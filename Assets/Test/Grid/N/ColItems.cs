using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ResourceType { Food, Water }

public class ColItems : MonoBehaviour
{

    public ResourceType type;  //Item type

    public Vector3Int GridPosition { get; set; }   //The position of the item in grid coordinates



}