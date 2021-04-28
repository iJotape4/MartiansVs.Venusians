using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]

public class NavMeshController : MonoBehaviour
{
    #region Inspector Properties
    [SerializeField] private Camera mainCamera;
    public GameObject objetivo;
    public GameObject TileTargetPrefab;
    public NavMeshAgent agente;

    public bool _is_moving = false;
    public bool _is_selected = false;
    public bool _is_stuned;
    public bool _isclicked = false;

    public GameObject TileActual;
    #endregion

    #region Private Properties
    private float speed = 20;

    private Animator animator;
    private Vector3 posMouse;
    private Vector3 posActual;
    List<GameObject> posicionesPosibles;

    #endregion

    #region DiagonalsLists 
    List<int[]> Diagonals = new List<int[]> 
    {
       new int[]  {63,  73},
       new  int[]  { 54, 64, 74 },
       new  int[]  { 45, 55, 65, 75 },
       new  int[]  { 36, 46, 56, 66, 76 },
       new  int[]  { 27, 37, 47, 57, 67, 77 },
       new  int[]  { 18, 28, 38, 48, 58, 68 , 78 },
       new  int[]  { 9,19,29,39,49,59,69 ,79 },
       new  int[]  { 0,10,20,30,40,50,60,70,80 },
       new  int[]  { 1, 11,21,31,41,51,61,71 },
       new  int[]   { 2,12,22,32,42,52,62},
       new  int[]   { 3,13 ,23,43,53 },
       new  int[]   { 4,14,24,34,44 },
       new  int[]   { 5,15,25,35},
       new  int[]   { 6,16,26 },
       new  int[]   { 7,17}
    }
    ;

     List<int[]> Diagonals2 = new List<int[]>
    {
       new int[]  {79,  71},
       new  int[]  { 78, 70, 62 },
       new  int[]  { 77, 69, 61, 53 },
       new  int[]  { 76, 68, 60, 52, 44 },
       new  int[]  { 75, 67, 59, 51, 43, 35 },
       new  int[]  { 74, 66, 58, 50, 42, 34 , 26 },
       new  int[]  { 73,65,57,49,41,33,25 ,17 },
       new  int[]  { 72,64,56,48,40,32,24,16,8 },
       new  int[]  { 63, 55,47,39,31,23,15,7 },
       new  int[]   { 54,46,38,30,22,14,6},
       new  int[]   { 45,37 ,29,21,13,5 },
       new  int[]   { 36,28,20,12,4 },
       new  int[]   { 27,19,11,3},
       new  int[]   { 18,10,2 },
       new  int[]   { 9,1}
    }
     ;

    #endregion



    //public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame

    public List<GameObject> CaclularCasillasPosibles()
    {

        List<GameObject> PosiblePositions = new List<GameObject>();
        int position;
        for (int i = 0; i < GameManager.Instance.positions.Length; i++)
        {
            if (GameManager.Instance.positions[i].gameObject == TileActual)
            {
                position = i;
                int DadoNumber = Dado.Instance._numeroActual;
                for (int j = 1; j <= DadoNumber; j++)
                    
                {
                    try
                    {
                        //Casillas a la izquierda
                        if (((position - (1 * j)) >= 0 && position >= 0 && position < 9) ||
                            ((position - (1 * j)) >= 9 && position >= 9 && position < 18) ||
                            ((position - (1 * j)) >= 18 && position >= 18 && position < 27) ||
                            ((position - (1 * j)) >= 27 && position >= 27 && position < 36) ||
                            ((position - (1 * j)) >= 36 && position >= 36 && position < 45) ||
                            ((position - (1 * j)) >= 45 && position >= 45 && position < 54) ||
                            ((position - (1 * j)) >= 54 && position >= 54 && position < 63) ||
                            ((position - (1 * j)) >= 63 && position >= 63 && position < 72) ||
                            ((position - (1 * j)) >= 72 && position >= 72 && position < 80)
                            &&(!GameManager.Instance.positions[position - (1 * j)].gameObject.GetComponent<Tiles>()._vulcanized) )
                        {
                            PosiblePositions.Add(GameManager.Instance.positions[position - (1 * j)].gameObject);

                        }


                        //Casillas a la Derecha
                        if (((position + (1 * j)) < 9 && position >= 0 && position < 9) ||
                          ((position + (1 * j)) < 18 && position >= 9 && position < 18) ||
                          ((position + (1 * j)) < 27 && position >= 18 && position < 27) ||
                          ((position + (1 * j)) < 36 && position >= 27 && position < 36) ||
                          ((position + (1 * j)) < 45 && position >= 36 && position < 45) ||
                          ((position + (1 * j)) < 54 && position >= 45 && position < 54) ||
                          ((position + (1 * j)) < 63 && position >= 54 && position < 63) ||
                          ((position + (1 * j)) < 72 && position >= 63 && +position < 72) ||
                          ((position + (1 * j)) < 80 && position >= 72 && position < 80)
                           && (!GameManager.Instance.positions[position + (1 * j)].gameObject.GetComponent<Tiles>()._vulcanized) )
                        {
                            PosiblePositions.Add(GameManager.Instance.positions[position + (1 * j)].gameObject);

                        }


                        //Casillas hacia arriba
                        if ((position + (1 * j)) <= 80
                    && (!GameManager.Instance.positions[position + (9 * j)].gameObject.GetComponent<Tiles>()._vulcanized))
                        {
                            PosiblePositions.Add(GameManager.Instance.positions[position + (9 * j)].gameObject);

                        }


                        //Casillas hacia abajo
                        if ((position - (9 * j)) >= 0
                            && (!GameManager.Instance.positions[position - (9 * j)].gameObject.GetComponent<Tiles>()._vulcanized))

                        {
                            PosiblePositions.Add(GameManager.Instance.positions[position - (9 * j)].gameObject);

                        }

                        //Diagonales Der/Izq
                        List<GameObject> PosibleDiags1 = CalculateDiagonals(Diagonals, position);
                        foreach (GameObject d1 in PosibleDiags1)
                        {
                            PosiblePositions.Add(d1);
                        }

                        List<GameObject> PosibleDiags2 = CalculateDiagonals(Diagonals2, position);
                        foreach (GameObject d2 in PosibleDiags2)
                        {
                            PosiblePositions.Add(d2);
                        }

                    }
                    catch { }
                }
            }
        }


        for (int i = 0; i < GameManager.Instance.positions.Length; i++)
        {
            if (PosiblePositions.Contains(GameManager.Instance.positions[i].gameObject))
            {
                GameManager.Instance.positions[i].GetComponent<Tiles>()._posibleMovement = true;

            }
            else
            {
                GameManager.Instance.positions[i].GetComponent<Tiles>()._posibleMovement = false;
            }
        }



        return PosiblePositions;
    }

    public List<GameObject> CalculateDiagonals(List<int[]> Diagonals, int position)
    {
        List<GameObject> PosibleDiagonals = new List<GameObject>();
        int posInarray = 0;
        for (int l = 0; l < Diagonals.Count; l++)
        {
            for (int h = 0; h < Diagonals[l].Length; h++)
            {
                if (Diagonals[l][h] == position)
                {
                    posInarray = Array.IndexOf( Diagonals[l], position)+1;
                    

                    for (int g = 1; g <= Diagonals[l].Length; g++  )
                    {

                        if (Diagonals[l][g-1] != position &&  (posInarray-g <= Dado.Instance._numeroActual)
                            && (!GameManager.Instance.positions[Diagonals[l][g - 1]].gameObject.GetComponent<Tiles>()._vulcanized)) {

                            if (g > posInarray && g-posInarray <=Dado.Instance._numeroActual )
                            {
                                PosibleDiagonals.Add(GameManager.Instance.positions[Diagonals[l][g - 1]].gameObject);
                            }
                            else if ( g < posInarray)
                            {
                                PosibleDiagonals.Add(GameManager.Instance.positions[Diagonals[l][g - 1]].gameObject);
                            }                          
                        }
                    }


                }
            }
        }

        return PosibleDiagonals;
    }

    void Update()
    {
        if (!_is_selected)
        {
            OnClickedUnit();
        }
      
       



        posActual = agente.transform.position;
        if (this.GetComponent<PlayerController>().PlayerTurn == GameManager.Instance.JugadorActual && !_is_moving && this._is_selected )
        {

            if (Input.GetMouseButtonDown(0) && _is_selected)
            {

                posMouse = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 206.257f, Camera.main.ScreenToWorldPoint(Input.mousePosition).z);



                RaycastHit hit;
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                        if (posicionesPosibles.IndexOf(hit.transform.gameObject)>-1)
                        {
                            float step = speed * Time.deltaTime;
                            _is_moving = true;
                            animator.SetBool("Is_moving", _is_moving);
                            

                            Vector3 position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 206.257f, Camera.main.ScreenToWorldPoint(Input.mousePosition).z);


                            var TileTarget = Instantiate(TileTargetPrefab, position, Quaternion.identity);

                            objetivo = corregirPosition(TileTarget.gameObject);
                            Camera.main.GetComponent<CameraControl>().GoFinal();

                            agente.destination = objetivo.transform.position;
                            transform.position = Vector3.MoveTowards(transform.position, TileTarget.transform.position, step);
                        }
                }
            }

        }
    }

    //Método que elije la casilla mas
    private GameObject corregirPosition(GameObject Tiletarget)
    {
        float Nearest = 20;
        GameObject NearestObject = Tiletarget.gameObject;
        for (int i = 0; i < GameManager.Instance.board.Length; i++)
        {
            float provisional = Vector3.Distance(Tiletarget.transform.position, GameManager.Instance.board[i].transform.position);
            if (provisional < Nearest && provisional < Dado.Instance._numeroActual && Dado.Instance._numeroActual > 0)
            {
                Nearest = provisional;
                NearestObject = GameManager.Instance.board[i];
            }
        }
        Tiletarget.transform.position = new Vector3(NearestObject.transform.position.x, Tiletarget.transform.position.y, NearestObject.transform.position.z);
        return Tiletarget;
    }

    //Collision whit a target 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Destino" && _is_moving)
        {

            _isclicked = false;
            _is_moving = false;
            _is_selected = false;

            animator.SetBool("Is_moving", _is_moving);


            Dado.Instance.ResetPos();
            posicionesPosibles = CaclularCasillasPosibles();
            GameManager.Instance.NextTurno();

            UIManager.Instance.DesaactivateUiCon("UiconAllien");
            UIManager.Instance.ActivateUiCon("UiconDices");
            UIManager.Instance.DesaactivateUiCon("UiconCasillas");

            Camera.main.GetComponent<CameraControl>().GoInicial();
       
        }

        if (other.tag == "Tile")
        {
            TileActual = other.gameObject;
        }

        if (other.gameObject.layer == 10)
        {
            _is_stuned = true;
        }


        
    }

    //Select a Unit
    private void OnClickedUnit()
    {
        RaycastHit hit;


        if (Input.GetMouseButtonDown(1)  && Dado.Instance._numeroActual!=0  && !_is_stuned)
        {
            
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {

                if (hit.transform.gameObject == this.gameObject && (gameObject.layer == (8) && GameManager.Instance.JugadorActual == 1))
                {
                    _is_selected = true;
                    Debug.Log("You selected the " + hit.transform.name);
                    posicionesPosibles = CaclularCasillasPosibles();

                    UIManager.Instance.DesaactivateUiCon("UiconAllien");
                    UIManager.Instance.DesaactivateUiCon("UiconDices");
                    UIManager.Instance.ActivateUiCon("UiconCasillas");

                }
                 else if (hit.transform.gameObject == this.gameObject && (gameObject.layer == (9) && GameManager.Instance.JugadorActual == 2))
                {
                    _is_selected = true;
                    Debug.Log("You selected the " + hit.transform.name);
                    posicionesPosibles = CaclularCasillasPosibles();

                    UIManager.Instance.DesaactivateUiCon("UiconAllien");
                    UIManager.Instance.DesaactivateUiCon("UiconDices");
                    UIManager.Instance.ActivateUiCon("UiconCasillas");

                }              
            }
        }
    }

    void OnCollisionEnter(Collision collision){

        if (this.gameObject.layer==(8)&&  collision.gameObject.layer==(9))
        {
        
            Destroy(collision.gameObject);
           GameManager.Instance.livesP2-=1;
            
        }if (this.gameObject.layer==(9)&&  collision.gameObject.layer==(8))
        {
             Destroy(collision.gameObject);
             GameManager.Instance.livesP1-=1;
        }



    }
    
    
}


