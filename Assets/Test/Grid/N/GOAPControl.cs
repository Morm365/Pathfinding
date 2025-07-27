using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class GOAPControl : MonoBehaviour
{


    private WorldState worldState;   //Current status of food and water quantity

    private ColResAction foodAction;   //Food collection action

    private ColResAction waterAction; //water action

    private NPCMover mover;

    private bool isBusy = false;

    private int growCount = 0;


    void Start()
    {

        mover = GetComponent<NPCMover>();      //component of movement

        worldState = new WorldState();     //Create a new state

        foodAction = new ColResAction(ResourceType.Food, worldState, mover);       //Initialize food/water action

        waterAction = new ColResAction(ResourceType.Water, worldState, mover);



        StartCoroutine(PlannerLoop());   //Start the decision-making cycle



    }

    IEnumerator PlannerLoop()
    {

        while (growCount < 6)
        {
            if (!isBusy)            //If free, select a new action
            {

                if (worldState.food < 5)   //first collects food
                {
                    isBusy = true;

                    foodAction.DoAction();

                }
                else if (worldState.water < 5)
                {

                    isBusy = true;

                    waterAction.DoAction();


                }
                else
                {

                    transform.localScale += Vector3.one;   //grow  When food and water more 5 

                    growCount++;

                    Debug.Log("NPC grown by" + transform.localScale);


                    worldState.food = 0;    //Reset state

                    worldState.water = 0;


                }
            }


            yield return new WaitForSeconds(0.5f);   //pause between checks

            isBusy = false;

        }

        Debug.Log("Resources collected");


    }










}
