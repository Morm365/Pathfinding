using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WorldState
{


    public int food = 0;

    public int water = 0;



    public bool FullResources()
    {
        return food >= 5 && water >= 5;    //if there is 5 or more food and water
    }



  
}
