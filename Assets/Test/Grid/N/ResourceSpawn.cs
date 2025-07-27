using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawn : MonoBehaviour
{

    [SerializeField] GameObject foodPrefab;

    [SerializeField] GameObject waterPrefab;

    Grid grid;      //Grid component reference



    public static List<ColItems> ActiveItems = new List<ColItems>();  //List of all active items on the stage

    IEnumerator Start()
    {

        yield return new WaitForSeconds(0.1f);  // waint until grid loaded

        grid = FindObjectOfType<Grid>(); //Find Grid on stage

        StartCoroutine(SpawnRoutine());


    }

    IEnumerator SpawnRoutine()
    {

        while (true)
        {
            SpawnRandomItem();

            yield return new WaitForSeconds(0.5f);



        }


    }


    void SpawnRandomItem()     //Creates random item
    {


      //  int foodMaxCount = 0;


        int foodCount = 0;

        int waterCount = 0;


        foreach (var item in ActiveItems)   //Count food and water
        {

            if (item == null)
            {
                continue;
            }
            if (item.type == ResourceType.Food)
            {
                foodCount++;
            }
            else if (item.type == ResourceType.Water)
            {
                waterCount++;
            }



        }



        if (foodCount >= 30 && waterCount >= 30)   //If both types have reached their limit
        {
            return;
        }






            Vector3Int pos;

        do         //Find a random passable cell
        {
            pos = new Vector3Int(Random.Range(0, grid.Width), 0, Random.Range(0, grid.Height));  //select a random passable cell



        }
        while (!grid.GetNode(pos).IsWalkable);


        ResourceType typeToSpawn;   //Determine what to spawn

        if (foodCount >= 30)
        {

            typeToSpawn = ResourceType.Water;

        }
        else if (waterCount >= 30)
        {

            typeToSpawn = ResourceType.Food;

        }
        else
        {

            typeToSpawn = Random.value < 0.5f ? ResourceType.Food : ResourceType.Water;


        }



        GameObject prefab = (typeToSpawn == ResourceType.Food) ? foodPrefab : waterPrefab;    // chose pref

        GameObject obj = Instantiate(prefab, grid.GetNode(pos).WorldPosition + Vector3.up * 0.5f, Quaternion.identity);   //Create an object in the world



        ColItems Finalitem = obj.GetComponent<ColItems>();       //Get ColItems component and save data

        Finalitem.type = typeToSpawn;

        Finalitem.GridPosition = pos;


        ActiveItems.Add(Finalitem);     //Add to active list
    }


}
