using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MenuScript
{
   [MenuItem("Tools/Assign Tile Material")]
   public static void AssignTileMaterial(){
       GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
       Material material = Resources.Load<Material>("testmaterial");

       foreach (GameObject item in tiles)
       {
           item.GetComponent<Renderer>().material = material;
       }
   }
}
