using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class Tiles : MonoBehaviour
{
    #region Public Properties  
    public bool _unitHere;
    public bool _posibleMovement;
    public bool _vulcanized;
    public int _vulcanoCounter;
    #endregion

    #region Private Properties
    public MeshRenderer _meshRenderer;
    public Transform _transform;
    public GameObject[] _tilesList;
    #endregion

    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _tilesList = GameManager.Instance.board;
        _unitHere = false;

        _transform = GetComponent<Transform>();
    }


    private void Update()
    {
        if(_vulcanoCounter != 0)
        {
            if(_vulcanoCounter+3 == GameManager.Instance.TurnoActual)
            {
                _vulcanized = false;
                _vulcanoCounter = 0;
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
            _unitHere = true;

            if (other.GetComponent<PlayerController>().PlayerTurn == GameManager.Instance.JugadorActual
                && other.GetComponent<NavMeshController>()._isclicked == false)
            {
                CalculateColoration();
            }
        }
    }

    public void CalculateColoration()
    {
        for (int i = 0; i < GameManager.Instance.board.Length; i++)
        {
            Tiles Tile = _tilesList[i].GetComponent<Tiles>();

            if (!Tile._vulcanized)
            {
                //El +0.5 es para que pueda tomar las casillas diagonales al clickar en el centro ded ellas
                if (Tile._posibleMovement)
                {

                if (Tile._unitHere && Tile != this)
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
            _vulcanized = true;
            _vulcanoCounter = GameManager.Instance.TurnoActual;
            //StartCoroutine(Unvulcanization());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == ("Unit"))
        {
            _unitHere = false;
        }
    }

    public IEnumerator Unvulcanization()
    {
        int Counter = GameManager.Instance.TurnoActual;
        if(GameManager.Instance.TurnoActual == Counter + 3)
        {
            _vulcanized = false;
        }
        yield return null;
    }
}



