using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class Tiles : MonoBehaviour
{
    public static Tiles instance;
    public MeshRenderer _meshRenderer;
   void Start(){
        _meshRenderer=  GetComponent<MeshRenderer>();

   }


    private void Update()
    {
        
    }
    void colorear(string color){
    
        _meshRenderer.material = (Resources.Load<Material>(color));

}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer== 10)
        {
            colorear("Tile");
        }
    }

}
