using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    public static Tiles instance;
    public MeshRenderer _meshRenderer;
   void Start(){
       GetComponent<MeshRenderer>();

   }
void coloear(){
    
_meshRenderer.material = (Resources.Load<Material>("CasillaGreenSprite"));

}
    
    
}
