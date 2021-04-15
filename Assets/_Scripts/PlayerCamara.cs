using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamara : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject jugador;
    private Vector3  posicionR;

    void Start()
    {
        posicionR =  transform.position-jugador.transform.position ;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position=jugador.transform.position+posicionR;
    }
}
