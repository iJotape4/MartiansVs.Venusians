using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(NavMeshController))]
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
            StartCoroutine(CountFinishStun());
        }
        else
        {
            _meshRenderer.enabled = false;
        }
    }

    public IEnumerator CountFinishStun()
    {
        int Counter = GameManager.Instance.TurnoActual;
        if (GameManager.Instance.TurnoActual == Counter + 2)
        {
            _navMeshController._is_stuned = false;
        }
        yield return null;
    }
}
