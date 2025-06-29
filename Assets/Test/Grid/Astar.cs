using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar : MonoBehaviour
{

    Grid grid;  //grid object reference

    //List<Node> closedList = new List<Node>


    [SerializeField] Vector3Int startPosition;

    [SerializeField] Vector3Int goalPosition;


    //Node startNode;

    //Node goalNode;

    //Node currentNode;

    //List<>
    //List<>
    //List<>


    public Vector3Int GoalPosition => goalPosition;     //public access to goalPosition


    //
    void Start()
    {

        grid = FindObjectOfType<Grid>();  //find the Grid component on the scene



        //FindPath(startPosition, goalPosition);

    }

    public List<Node> FindPath(Vector3Int start, Vector3Int goal) //method of finding a path from a start point to a goal
    {

        Node startNode = grid.GetNode(start);

        Node goalNode = grid.GetNode(goal);

        List<Node> openList = new List<Node>();

        HashSet<Node> closedList = new HashSet<Node>();  //already checked

        openList.Add(startNode);



        startNode.GCost = 0;
        startNode.HCost = GetDistance(startNode, goalNode);
        startNode.Parent = null;


        while (openList.Count > 0)
        {
            Node currentNode = openList[0];

            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].FCost < currentNode.FCost || (openList[i].FCost == currentNode.FCost && openList[i].HCost < currentNode.HCost)) //Find the node with the smallest FCost or HCost if FCost is the same
                {

                    currentNode = openList[i];

                }




            }


            openList.Remove(currentNode);

            closedList.Add(currentNode);   //add current to closed


            if (currentNode == goalNode)
            {

                List<Node> finalPath = RetracePath(startNode, goalNode);

                foreach (Node node in finalPath)
                {

                    node.SetColor(Color.yellow);

                }

                startNode.SetColor(Color.green);

                goalNode.SetColor(Color.red);



                return finalPath;

            }



            //goes around the neighbors of the   current cell

            foreach (Node neighbour in GetNeighbours(currentNode))
            {

                if (!neighbour.IsWalkable || closedList.Contains(neighbour))
                {
                    continue;
                }

                int newCost = currentNode.GCost + GetDistance(currentNode, neighbour);

                if (newCost < neighbour.GCost || !openList.Contains(neighbour))
                {
                    neighbour.GCost = newCost;

                    neighbour.HCost = GetDistance(neighbour, goalNode);

                    neighbour.Parent = currentNode;


                    if (!openList.Contains(neighbour))
                    {
                        openList.Add(neighbour);

                    }


                }


            }


        }

        return null;


    }

    List<Node> RetracePath(Node startNode, Node endNode)  //restores the path from end to beginning using  parents
    {

        List<Node> path = new List<Node>();

        Node current = endNode;

        while (current != startNode)
        {

            path.Add(current);

            current = current.Parent;

        }

        path.Reverse();

        return path;


    }



    int GetDistance(Node a, Node b)   //grid distance
    {

        int dx = Mathf.Abs(a.GridPosition.x - b.GridPosition.x);  //how many cells to go horizontally

        int dz = Mathf.Abs(a.GridPosition.z - b.GridPosition.z);

        return dx + dz;

    }

    List<Node> GetNeighbours(Node node)
    {

        List<Node> neighbours = new List<Node>();

        Vector3Int[] dirs = { new Vector3Int(1, 0, 0), new Vector3Int(-1, 0, 0),  new Vector3Int(0, 0, 1),  new Vector3Int(0, 0, -1)};

        foreach (var dir in dirs)
        {

            Vector3Int checkPos = node.GridPosition + dir;

            if (checkPos.x >= 0 && checkPos.x < grid.Width && checkPos.z >= 0 && checkPos.z < grid.Height)
            {

                neighbours.Add(grid.GetNode(checkPos));

            }


        }

        return neighbours;


    }


    


}


//    startNode = grid.GetNode(startPosition);

//        startNode.NodeObject.GetComponent<Renderer>().material.color = Color.green;


//        goalNode = grid.GetNode(goalPosition);

//        goalNode.NodeObject.GetComponent<Renderer>().material.color = Color.blue;


//        currentNode = startNode;

//        openList.Add(currentNode);

//        //////
//        ///



//        neighbours.Clear();




//        Vector3Int leftNodePosition = currentNode.GridPosition * new Vector3Int(-1, 0, 0);

//        if (currentNode.GridPositionx >= 0)
//        {

//            Node leftNode = grid.GetNode(leftNodePosition);

//            neighbours.Add(leftNode);


//        }

//        Vector3Int rightNodePosition = currentNode.GridPosition * new Vector3Int(1, 0, 0);

//        if (currentNode.GridPositionx >= 0)
//        {

//            Node rightNode = grid.GetNode(rightNodePosition);

//            neighbours.Add(rightNode);


//        }

//        Vector3Int downNodePosition = currentNode.GridPosition * new Vector3Int(0, 0, -1);

//        if (currentNode.GridPositionx >= 0)
//        {

//            Node downNode = grid.GetNode(vNodePosition);

//            neighbours.Add(downNode);


//        }

//        Vector3Int upNodePosition = currentNode.GridPosition * new Vector3Int(0, 0, 1);

//        if (currentNode.GridPositionx >= 0)
//        {

//            Node upNode = grid.GetNode(upNodePosition);

//            neighbours.Add(upNode);


//        }

//        for (i = 0; i < neighbours.Count; i++)
//        {

//            neighbours[i].NodeObject.GetComponent<Renderer>().material.color = Color.magnetics;

//            // if (!neighbours[i].IsWalkable || closedList.Contains neighbours[i])
//            //continue;

//            //int newGCost = currentNodeGCost * CalculateDistance(currentNode.GridPosition, neighbours,[i].GridPosition);
//            // if (newGCost < neighbours[i].GCost ) 


//        }

//    }



//}