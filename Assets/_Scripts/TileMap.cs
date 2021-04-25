using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TileMap : MonoBehaviour
{
    #region 

    public Unit[] units;
    public GameObject selectedUnit;

    public TileType[] tileTypes;
    List<Nodes> CurrentPaht;
    List<Nodes> unvisited = new List<Nodes>();
    int[,] tiles;
    Nodes[,] graph;
    float tileY = 0.8f;
    int mapSizeX = 9;
    int mapSizeY = 9;
    #endregion
    void Start()
    {

        mapdata();
        //GeneratePathFindingGraph();

    }

    void mapdata()
    {


        tiles = new int[mapSizeX, mapSizeY];

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeX; y++)
            {
                tiles[x, y] = 0;
            }
        }
        //volcano tiles
        tiles[5, 4] = 1;
        tiles[6, 4] = 1;
        tiles[5, 5] = 1;
        tiles[6, 5] = 1;

        //generate map


        Generatemap();
    }
    void Generatemap()
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeX; y++)
            {
                TileType tt = tileTypes[tiles[x, y]];
                GameObject go = (GameObject)Instantiate(tt.tileVisualPrefab, new Vector3(x, 0, y), Quaternion.identity);
                Clickeable ct = go.GetComponent<Clickeable>();
                ct.tileX = x;
                ct.tileY = y;
                ct.map = this;
            }
        }

    }
    public Vector3 TileCoordToWorldCoord(int x, float y, int z)
    {
        return new Vector3(x, 0.8f, z);


    }
    class Nodes
    {
        public List<Nodes> neighbours;
        public int x;
        public int y;
        public Nodes()
        {
            neighbours = new List<Nodes>();
        return;
        }
        public float DistanceTo(Nodes n)
        {
            return Vector2.Distance(
                new Vector2(x, y),
                new Vector2(n.x, n.y)
            );
        }

    }
    public void MoveUnitTo(int x, int z)
    {
        CurrentPaht = null;
        
            selectedUnit.GetComponent<Unit>().tileX = x;
            selectedUnit.GetComponent<Unit>().tileZ = z;
            selectedUnit.transform.position = TileCoordToWorldCoord(x, tileY, z);


        Dictionary<Nodes, float> dist = new Dictionary<Nodes, float>();
        Dictionary<Nodes, Nodes> Prev = new Dictionary<Nodes, Nodes>();
       

        Nodes source = graph[
           selectedUnit.GetComponent<Unit>().tileX=x,
            selectedUnit.GetComponent<Unit>().tileZ=z
            
        ];

        Nodes target = graph[
         x, z
       ];


        dist[source] = 0;
        Prev[source] = null;


        foreach (Nodes v in graph)
        {

            if (v != source)
            {
                dist[v] = Mathf.Infinity;
                Prev[v] = null;

            }
            unvisited.Add(v);
        }



        while (unvisited.Count > 0)
        {
            //ruta mas corta
            Nodes u = null;
            foreach (Nodes possible in unvisited)
            {
                if (u == null || dist[possible] < dist[u])
                {
                    u = possible;
                }
            }

            if (u == target)
            {
                break;
            }

            unvisited.Remove(u);

            foreach (Nodes v in u.neighbours)
            {
                float alt = dist[u] + u.DistanceTo(v);
                if (alt < dist[v])
                {
                    dist[v] = alt;
                    Prev[v] = u;

                }
            }
        }

        if (Prev[target] == null)
        {
            return;
        }
        CurrentPaht = new List<Nodes>();

        Nodes curr = target;
        while (curr != null)
        {
            CurrentPaht.Add(curr);
            curr = Prev[curr];
        }
        CurrentPaht.Reverse();
    
    }
    //found the target



    void GeneratePathFindingGraph()
    {
        graph = new Nodes[mapSizeX, mapSizeY];

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeX; y++)
            {
                graph[x, y] = new Nodes();
                graph[x, y].x = x;
                graph[x, y].y = y;
            }
        }

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeX; y++)
            {

                if (x > 0)
                    graph[x, y].neighbours.Add(graph[x - 1, y]);
                if (x < mapSizeX - 1)
                    graph[x, y].neighbours.Add(graph[x - 1, y]);
                if (y > 0)
                    graph[x, y].neighbours.Add(graph[x, y - 1]);
                if (y < mapSizeY - 1)
                    graph[x, y].neighbours.Add(graph[x, y - 1]);
            }
        }


    }

}

