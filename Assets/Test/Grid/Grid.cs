using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Grid : MonoBehaviour             //Places a grid of cells on the map
{

    [SerializeField] int cellCountX;   

    [SerializeField] int cellCountZ;

    [SerializeField] float cellSizeX;

    [SerializeField] float cellSizeZ;

    [SerializeField] GameObject nodePrefab;

    [SerializeField] int obstaclesNumber = 5;

    Node[] grid;



    void Start()
    {

        grid = new Node[cellCountX * cellCountZ];

        for (int z = 0; z < cellCountZ; z++)
        {

            for (int x = 0; x < cellCountX; x++)
            {

                int i = x + z * cellCountX;

                Vector3 worldPosition = new Vector3(x * cellSizeX, 0, z * cellSizeZ);

                Vector3Int gridPosition = new Vector3Int(x, 0, z);

                bool isWalkable = true;   //by default the cell is passable


                GameObject nodeObject = Instantiate(nodePrefab, worldPosition, Quaternion.identity);

                grid[i] = new Node(worldPosition, gridPosition, isWalkable, nodeObject);  //create and save a Node object with its coordinates and  availability 


            }


        }

        GenerateObstacles(obstaclesNumber);   //create random obstacles after grid generation




    }








    public Node GetNode(Vector3Int gridPosition)   //get a node by coordinates in the grid
    {

        int i = gridPosition.x + gridPosition.z * cellCountX;

        return grid[i];



    }


    public Vector3Int WorldToGridPosition(Vector3 position)   //translating the    world position into    grid position
    {

        int x = Mathf.RoundToInt(position.x / cellSizeX);

        int z = Mathf.RoundToInt(position.z / cellSizeZ);

        return new Vector3Int(x, 0, z);

    }

    

    public int Width => cellCountX;

    public int Height => cellCountZ;

    public void GenerateObstacles(int count)  //Creates random uncrossable cells
    {

        int attempts = 0;

        int maxAttempts = count;

        while (count > 0 && attempts < maxAttempts)
        {
            int x = Random.Range(0, cellCountX);

            int z = Random.Range(0, cellCountZ);

            Vector3Int pos = new Vector3Int(x, 0, z);

            Node node = GetNode(pos);

            if (node != null && node.IsWalkable)
            {
                node.IsWalkable = false;

                node.SetColor(Color.black);

                count--;


            }

            attempts++;

        }


    }


}
