using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NavMeshController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    public GameObject objetivo;

    public GameObject TileTargetPrefab;
    public NavMeshAgent agente;
    private float speed = 20;
    public bool Is_moving = false;
    public bool is_selected = false;
    private Animator animator;
    private Vector3 posMouse;
    private Vector3 posActual;
    public GameObject TileActual;
    List<GameObject> posicionesPosibles;

    public List<int[]> Diagonals = new List<int[]> 
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

    }
     ;



    public bool isclicked = false;

    //public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();


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
                int DadoNumber = Dado.Instance.NumeroActual;
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
                            ((position - (1 * j)) >= 72 && position >= 72 && position < 80))
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
                          ((position + (1 * j)) < 80 && position >= 72 && position < 80))
                        {
                            PosiblePositions.Add(GameManager.Instance.positions[position + (1 * j)].gameObject);

                        }


                        //Casillas hacia arriba
                        if ((position + (1 * j)) <= 80)
                        {
                            PosiblePositions.Add(GameManager.Instance.positions[position + (9 * j)].gameObject);

                        }


                        //Casillas hacia abajo
                        if ((position - (9 * j)) >= 0)
                        {
                            PosiblePositions.Add(GameManager.Instance.positions[position - (9 * j)].gameObject);

                        }

                        //Diagonales Der/Izq
                        List<GameObject> PosibleDiags1 = CalculateDiagonals(Diagonals, position);
                        foreach (GameObject e in PosibleDiags1)
                        {
                            PosiblePositions.Add(e);
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
                GameManager.Instance.positions[i].GetComponent<Tiles>().PosibleMovement = true;

            }
            else
            {
                GameManager.Instance.positions[i].GetComponent<Tiles>().PosibleMovement = false;
            }
        }



        return PosiblePositions;
    }

    public List<GameObject> CalculateDiagonals(List<int[]> Diagonals, int position)
    {
        List<GameObject> PosibleDiagonals = new List<GameObject>();
        for (int l = 0; l < Diagonals.Count; l++)
        {
            for (int h = 0; h < Diagonals[l].Length; h++)
            {
                if (Diagonals[l][h] == position)
                {
                    for (int g = 0; g < Diagonals[l].Length; g++)
                    {
                        if (Diagonals[l][g] != position)
                            PosibleDiagonals.Add(GameManager.Instance.positions[Diagonals[l][g]].gameObject);
                    }

                }
            }
        }

        return PosibleDiagonals;
    }

    void Update()
    {
        if (!is_selected)
        {
            OnClickedUnit();
        }
      
       



        posActual = agente.transform.position;
        if (this.GetComponent<PlayerController>().PlayerTurn == GameManager.Instance.JugadorActual && !Is_moving && this.is_selected)
        {

            //TODO: Toca hacer que no aparezcan las casillas apenas le pasa el turno, sino cuando se selecciona la unidad
            posicionesPosibles = CaclularCasillasPosibles();

            if (Input.GetMouseButtonDown(0) && is_selected)
            {

                posMouse = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 206.257f, Camera.main.ScreenToWorldPoint(Input.mousePosition).z);



                RaycastHit hit;
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                   //for (int i = 0; i <= posicionesPosibles.Count; i++)
                    //{
                        if (posicionesPosibles.IndexOf(hit.transform.gameObject)>-1)
                        {
                            float step = speed * Time.deltaTime;
                            Is_moving = true;
                            animator.SetBool("Is_moving", Is_moving);
                            

                            Vector3 position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 206.257f, Camera.main.ScreenToWorldPoint(Input.mousePosition).z);


                            var TileTarget = Instantiate(TileTargetPrefab, position, Quaternion.identity);

                            objetivo = corregirPosition(TileTarget.gameObject);
                            Camera.main.GetComponent<CameraControl>().GoFinal();

                            agente.destination = objetivo.transform.position;
                            transform.position = Vector3.MoveTowards(transform.position, TileTarget.transform.position, step);
                        }

                    //}
                    

                }


                //El +0.5 es para que pueda tomar las casillas diagonales al clickar en el centro ded ellas
                /*  if (Vector3.Distance(posActual, posMouse) < Dado.Instance.NumeroActual + 0.5)

                  {
                      isclicked = true;
                      Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                      if (Physics.Raycast(ray, out RaycastHit RaycastHit))
                      {
                          float step = speed * Time.deltaTime;
                          Is_moving = true;
                          animator.SetBool("Is_moving", Is_moving);

                          Vector3 position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 206.257f, Camera.main.ScreenToWorldPoint(Input.mousePosition).z);

                          var TileTarget = Instantiate(TileTargetPrefab, position, Quaternion.identity);

                          objetivo = corregirPosition(TileTarget.gameObject);
                          // objetivo = TileTarget.gameObject;

                          agente.destination = objetivo.transform.position;
                          transform.position = Vector3.MoveTowards(transform.position, TileTarget.transform.position, step);
                      }
                  }
                  else
                  {
                      isclicked = false;

                  } */

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
            if (provisional < Nearest && provisional < Dado.Instance.NumeroActual && Dado.Instance.NumeroActual > 0)
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
        if (other.tag == "Destino" && Is_moving)
        {

            isclicked = false;
            Is_moving = false;
            is_selected = false;

            animator.SetBool("Is_moving", Is_moving);


            Dado.Instance.ResetPos();
            posicionesPosibles = CaclularCasillasPosibles();
            GameManager.Instance.NextTurno();
            
             Camera.main.GetComponent<CameraControl>().GoInicial();
            
        }

        if (other.tag == "Tile")
        {
            TileActual = other.gameObject;
        }
    }

    //Select a Unit
    private void OnClickedUnit()
    {
        RaycastHit hit;


        if (Input.GetMouseButtonDown(1)  && Dado.Instance.NumeroActual!=0)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {

                if (hit.transform.gameObject == this.gameObject && (gameObject.layer == (8) && GameManager.Instance.JugadorActual == 1))
                {
                    is_selected = true;
                    Debug.Log("You selected the " + hit.transform.name);

                }
                 else if (hit.transform.gameObject == this.gameObject && (gameObject.layer == (9) && GameManager.Instance.JugadorActual == 2))
                {
                    is_selected = true;
                    Debug.Log("You selected the " + hit.transform.name);

                }
            }
        }
    }
    
}


