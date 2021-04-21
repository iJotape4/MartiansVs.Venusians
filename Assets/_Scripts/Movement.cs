using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    List<Tiles> selectablesTiles = new List<Tiles>();
    GameObject[] tiles;
    public int move = 3;
    public float jumpheigth;
    Vector3 velocity = new Vector3();
    Vector3 heading = new Vector3();
    Stack<Tiles> path = new Stack<Tiles>();
    Tiles currentTile;


    float halheight = 0;
    protected void init()
    {

        tiles = GameObject.FindGameObjectsWithTag("Tile");
        //halheight = GetComponent<Collider>().bounds.extends.y;
    }



    public void GetCurrentTile()
    {

        currentTile = GetCurrentTile(GameObject);
        currentTile.current = true;
    }

    public Tiles GetTargetTile(GameObject target)
    {
        RaycastHit hit;
        Tiles tile = null;
        if (Physics.Raycast(target.transform.position,Vector3.up,out hit,1))
        {
            tile = hit.Collider.GetComponent<Tiles>();
        }
        return tile;
    }



    public void computeAdjacencyList()
    {

        //tiles=GameObject.FindGameObjectsWithTag("Tile");

        foreach (GameObject tile in tiles)
        {
            Tiles t = tile.GetComponent<Tiles>();
            t.FindNeighbors(jumpheigth);

        }
    }


    public void FindSelectableTiles()
    {
        computeAdjacencyList();
        GetCurrentTile();

        Queue<Tiles> process = new Queue<Tiles>();

        process.Enqueue(currentTile);
        currentTile.visited = true;
        //currentTile.parent=?? leave as null
        while (process.Count > 0)
        {
            Tiles t = process.Dequeue();
            selectablesTiles.Add(t);
            t.selectable = true;

            if (t.distance < move)
            {
                foreach (Tiles tile in t.adjacencyList)
                {
                    if (tile.visited)
                    {
                        tile.parent = t;
                        tile.visited = true;
                        tile.distance = 1 + t.distance;
                        process.Enqueue(tile);
                    }
                }
            }
        }

    }


public void MoveToTile(Tiles tile){
    path.Clear();
    t.target=true;
    moving=false; 

    Tiles next=tile;

    while (next!=null)
    {
        path.Push(next);
        next= next.parent;
    }
}

}
