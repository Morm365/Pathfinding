using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class NPCMover : MonoBehaviour
{

    Astar pathfinder;  //reference to the component responsible for pathfinding

    Grid grid;

    [SerializeField] float speed = 2f;



    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f); //  delay to ensure that Grid    and Astar have had time to initialize

        pathfinder = FindObjectOfType<Astar>();


        grid = FindObjectOfType<Grid>();

        if (pathfinder == null || grid == null)
        {
            Debug.LogError("Astar or Grid not found");

            yield break;
        }


        Vector3Int start;

        //Select a random passable cell to place the NPC in
        do         //the NPC   will be placed on a free cell 
        {


            start = new Vector3Int(Random.Range(0, grid.Width), 0, Random.Range(0, grid.Height));


        }
        while (!grid.GetNode(start).IsWalkable);    //first it executes the loop then it checks the condition



        transform.position = grid.GetNode(start).WorldPosition + Vector3.up * 0.5f;

        // Vector3Int start = grid.WorldToGridPosition(transform.position);

        //Vector3Int start = new Vector3Int(Random.Range(0, grid.Width), 0, Random.Range(0, grid.Height));     // find path from random place on the grid

        //transform.position = grid.GetNode(start).WorldPosition + Vector3.up * 0.5f; 




       // //old
       // //


       // Vector3Int goal = pathfinder.GoalPosition;

       // List<Node> path = pathfinder.FindPath(start, goal); //build a path from the current position to the goal

       // if (path == null || path.Count == 0)
       // {

       //     Debug.LogWarning("Path not found");

       //     yield break;
       // }

       // StartCoroutine(FollowPath(path));

       ////.... StartCoroutine(MoveToNextCollectible());




       // //
       // //old

    }

    //IEnumerator MoveToNextCollectible()
    //{

    //    yield return new WaitForSeconds(0.1f);


    //    pathfinder = FindObjectOfType<Astar>();

    //    grid = FindObjectOfType<Grid>();

    //    Vector3Int start;

    //    do
    //    {

    //        GameObject target = FindClosestobject();



    //    }
    //    while(true)
    //    {



    //    }




    //   // GameObject FindClosestobject()

    //}


    IEnumerator FollowPath(List<Node> path)
    {
        foreach (Node node in path)                      //Navigating through a list of nodes
        {
            Vector3 target = node.WorldPosition + Vector3.up * 0.5f;

            while ((transform.position - target).sqrMagnitude > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);


                yield return null;
            }

            yield return new WaitForSeconds(0.05f);


        }
         
         
           
    }

   //FoodFinding


    public void StartPathTo(Vector3Int target, System.Action onComplete)
    {


        StopAllCoroutines();

        Vector3Int start = grid.WorldToGridPosition(transform.position);

        List<Node> path = pathfinder.FindPath(start, target);


        if (path != null && path.Count > 0)
        {

            StartCoroutine(FollowPath(path, onComplete));

        }
        else
        {

            onComplete?.Invoke();  //if the path is not found then terminate

        }

    }

    public int GetPathDistanceTo(Vector3Int target)
    {

        Vector3Int start = grid.WorldToGridPosition(transform.position);

        List<Node> path = pathfinder.FindPath(start, target);

        return path != null ? path.Count : int.MaxValue;

    }



    IEnumerator FollowPath(List<Node> path, System.Action onComplete)
    {

        foreach (Node node in path)
        {
            Vector3 target = node.WorldPosition + Vector3.up * 0.5f;

            while ((transform.position - target).sqrMagnitude > 0.01f)
            {

                transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);

                yield return null;

            }

            yield return new WaitForSeconds(0.05f);

        }

        onComplete?.Invoke();

    }


}
