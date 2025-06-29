using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Node : IComparable
{

    public int GCost;  //cost from starting point to current

    public int HCost;  //cost from current    point to the goal

    public Node Parent;

    public int FCost => GCost + HCost;  //the total cost of the path through this node

    public Vector3 WorldPosition { get; private set; }
    public Vector3Int GridPosition { get; private set; }
    public bool IsWalkable { get; set; }
    public GameObject NodeObject { get; private set; }


    public Node(Vector3 worldPosition, Vector3Int gridPosition, bool isWalkable, GameObject nodeObject)
    {

        WorldPosition = worldPosition;

        GridPosition = gridPosition;

        IsWalkable = isWalkable;

        NodeObject = nodeObject;


    }

    public int CompareTo(object obj)    //Comparison of nodes
    {

        Node other = obj as Node;

        int compare = FCost.CompareTo(other.FCost);   //compare by FCost

        if (compare == 0)
        {

            compare = HCost.CompareTo(other.HCost);

        }

        return -compare;   //invert so that lower values ​​have higher priority 

    }


    public void SetColor(Color color)
    {
        if (NodeObject != null)
        {

            NodeObject.GetComponent<Renderer>().material.color = color;

        }


    }


} 