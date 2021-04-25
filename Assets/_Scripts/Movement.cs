using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    List<Tiles> selectablesTiles = new List<Tiles>();
    GameObject[] tiles;
    public Tiles t;
    public bool moving=false;
    public int move = 3;
    public float moveSpeed =2;
    public float jumpheigth=2;
    Vector3 velocity = new Vector3();
    Vector3 heading = new Vector3();
    Stack<Tiles> path = new Stack<Tiles>();
    Tiles currentTile;


    float halheight = 0;
    protected void init()
    {

        tiles = GameObject.FindGameObjectsWithTag("Tile");
        halheight = GetComponent<Collider>().bounds.extents.y;
    }



    public void GetCurrentTile()
    {
        currentTile = GetTargetTile(gameObject);
        currentTile.current = true;
    }

    public Tiles GetTargetTile(GameObject target)
    {
        RaycastHit hit;
        Tiles tile = null;
        if (Physics.Raycast(target.transform.position,-Vector3.down,out hit,1))
        {
            tile = hit.collider.GetComponent<Tiles>();
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
                    if (!tile.visited)
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
        tile.target=true;
        moving=true; 

        Tiles next=tile;

        while (next!=null)
        {
            path.Push(next);
            next= next.parent;
        }
    }

    public void Move(){
        if (path.Count > 0)
        {
            Tiles t = path.Peek();
            Vector3 target = t.transform.position;
            target.y += halheight + t.GetComponent<Collider>().bounds.extents.y;

            if (Vector3.Distance(transform.position, target) >= 0.05f)
            {
                CalculateHeading(target);
                SetHorizontalVelocity();
                transform.forward = heading;
                transform.position += velocity * Time.deltaTime;
            }
            else{
                transform.position = target;
                path.Pop();
            }
            
        }else
        {
            RemoveSelectableTiles();
            moving = false;
        }
    }

    protected void RemoveSelectableTiles(){
        if (currentTile != null)
        {
            currentTile.current = false;
            currentTile = null;
        }

        foreach (Tiles tile in selectablesTiles)
        {
            tile.Reset();
        }
        selectablesTiles.Clear();
    }

    void CalculateHeading(Vector3 target)
    {
        heading = target - transform.position;
        heading.Normalize();
    }

    void SetHorizontalVelocity()
    {
        velocity = heading * moveSpeed;
    }

}
