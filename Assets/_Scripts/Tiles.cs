using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class Tiles : MonoBehaviour
{


    public MeshRenderer _meshRenderer;
    public Transform _transform;
    public GameObject[] _tilesList;
    public bool UnitHere;
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _tilesList = GameManager.Instance.board;
        UnitHere = false;

        _transform = GetComponent<Transform>();
    }


    private void Update()
    {

    }
    void colorear(string color)
    {

        _meshRenderer.material = (Resources.Load<Material>(color));

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == ("Unit"))
        {
            UnitHere = true;

            if (other.GetComponent<PlayerController>().PlayerTurn == GameManager.Instance.JugadorActual
                && other.GetComponent<NavMeshController>().isclicked == false)
            {
                CalculateColoration();
            }
            else
            {

            }
        }
    }

    private void CalculateColoration()
    {
        for (int i = 0; i < GameManager.Instance.board.Length; i++)
        {

            if (Vector3.Distance(_transform.position, _tilesList[i].transform.position) <= Dado.Instance.NumeroActual)
            {
                if (_tilesList[i].GetComponent<Tiles>().UnitHere && _tilesList[i].GetComponent<Tiles>() != this)
                    _tilesList[i].GetComponent<Tiles>().colorear("CasillaRojaSprite");
                else
                    _tilesList[i].GetComponent<Tiles>().colorear("CasillaGreenSprite");
            }
            else
            {
                _tilesList[i].GetComponent<Tiles>().colorear("CasillaBlueSprite");
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            colorear("Tile");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == ("Unit"))
        {
            UnitHere = false;
        }
    }
}



