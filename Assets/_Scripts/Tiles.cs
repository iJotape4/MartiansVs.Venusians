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
    public bool Vulcanized;
    public int  VulcanoCounter;
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _tilesList = GameManager.Instance.board;
        UnitHere = false;

        _transform = GetComponent<Transform>();
    }


    private void Update()
    {
        if(VulcanoCounter != 0)
        {
            if(VulcanoCounter+3 == GameManager.Instance.TurnoActual)
            {
                Vulcanized = false;
                VulcanoCounter = 0;
            }
        }
       

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
            Tiles Tile = _tilesList[i].GetComponent<Tiles>();

            if (!Tile.Vulcanized)
            {
                if (Vector3.Distance(_transform.position, _tilesList[i].transform.position) <= Dado.Instance.NumeroActual)
                {
                if (Tile.UnitHere && Tile != this)
                    Tile.colorear("CasillaRojaSprite");
                else
                    Tile.colorear("CasillaGreenSprite");
                 }
            else
            {
                Tile.colorear("CasillaBlueSprite");
            }
            }   
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            colorear("Tile");
            Vulcanized = true;
            VulcanoCounter = GameManager.Instance.TurnoActual;
            //StartCoroutine(Unvulcanization());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == ("Unit"))
        {
            UnitHere = false;
        }
    }

    public IEnumerator Unvulcanization()
    {
        int Counter = GameManager.Instance.TurnoActual;
        if(GameManager.Instance.TurnoActual == Counter + 3)
        {
            Vulcanized = false;
        }
        yield return null;
    }
}



