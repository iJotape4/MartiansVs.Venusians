using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticCamera : MonoBehaviour
{
    // Start is called before the first frame update
   public void rotateLeft(){
transform.Rotate(Vector3.up,90,Space.Self);
    }
    public void rotateRight(){
        transform.Rotate(Vector3.up,-90,Space.Self);
    }

}
