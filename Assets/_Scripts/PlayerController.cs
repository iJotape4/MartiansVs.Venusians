using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Movement
{
    Rigidbody rigidbody;
    public float velocidad;

    public void awake(){
        //rigidbody=GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
          rigidbody=GetComponent<Rigidbody>(); 
          init();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float movH=Input.GetAxis("Horizontal");
        float movV=Input.GetAxis("Vertical");
        Vector3 movimiento= new Vector3 (movH,0,movV);
        rigidbody.AddForce(movimiento*velocidad);
    }
    void Update (){

    }
}
