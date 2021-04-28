using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshRenderer))]

public class FlechaActivation : StunActivation
{

    // Update is called once per frame
    void Update()
    {
        if (!_navMeshController._is_selected 
            && _navMeshController.GetComponent<PlayerController>().PlayerTurn == GameManager.Instance.JugadorActual
            && Dado.Instance._numeroActual!=0)
        {
            _meshRenderer.enabled = true;
           
        }
        else
        {
            _meshRenderer.enabled = false;
        }
    }
}
