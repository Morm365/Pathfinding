using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;




public class ColResAction
{

    private ResourceType targetType;  //res type to search

    private WorldState worldState;  //State of the world

    private NPCMover mover;      //Movement component



    public ColResAction(ResourceType type, WorldState worldState, NPCMover mover)    //pass the resource type, state and movement component
    {
         
        this.targetType = type;
        this.worldState = worldState; 
        this.mover = mover;

    }


    public void DoAction()    //find the nearest resource you need and go to it
    {


        ColItems target = FindClosestResource();   //Find the nearest suitable resource

        if (target == null)
        {
            return;
        }



        mover.StartPathTo(target.GridPosition, () =>               //Build a path to the goal and perform an action upon completion
        {

            if (targetType == ResourceType.Food)   //Increase the counter of the required resource
            {

                worldState.food++;

            }
            else
            {

                worldState.water++;

            }

            ResourceSpawn.ActiveItems.Remove(target);   //Remove object from map and from list

            GameObject.Destroy(target.gameObject);


        });


    }

    private ColItems FindClosestResource()           //Find the nearest resource of the required type within a radius
    {
        ColItems closest = null;

        int shortest = int.MaxValue;

        foreach (var item in ResourceSpawn.ActiveItems)
        {
            if (item.type != targetType)
            {

                continue;   //Skip if type does not match

            }


            int dist = mover.GetPathDistanceTo(item.GridPosition);  //get the length of the path along A* from current NPC position  to position of the item

            if (dist <= 50 && dist < shortest)
            {


                shortest = dist;   //remember this distance as the new minimum

                closest = item;    //save a link to this item



            }
        }

        return closest;


    }





}
