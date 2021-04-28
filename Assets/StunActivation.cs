using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class StunActivation : MonoBehaviour
{

    #region Private Properties
    protected int _turnStun =0;
    protected MeshRenderer _meshRenderer;
    protected NavMeshController _navMeshController;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.enabled = false;

        _navMeshController = GetComponentInParent<NavMeshController>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( _navMeshController._is_stuned)
        {
            _meshRenderer.enabled = true;
            CountFinishStun();
        }
        else
        {
            _meshRenderer.enabled = false;
        }
    }

    void CountFinishStun()
    {
        if (_turnStun == 0)
            _turnStun = GameManager.Instance.TurnoActual;

        if (GameManager.Instance.TurnoActual == _turnStun + 2) 
        { 
            _navMeshController._is_stuned = false;
            _turnStun = 0;
        }
    }
}
