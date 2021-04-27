using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NavMeshController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    public GameObject objetivo;

    public GameObject TileTargetPrefab;
    public static NavMeshAgent agente;
    private float speed = 20;
    public bool Is_moving = false;
    public bool is_selected = false;
    private Animator animator;
    private Vector3 posMouse;
    private Vector3 posActual;
    public GameObject TileActual;



    public bool isclicked = false;

    //public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        // agente = GetComponent<NavMeshAgent>();
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
            List<GameObject> posicionesPosibles = CaclularCasillasPosibles();

            if (Input.GetMouseButtonDown(0) && is_selected)
            {

                posMouse = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 206.257f, Camera.main.ScreenToWorldPoint(Input.mousePosition).z);



                RaycastHit hit;
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    for (int i = 0; i <= posicionesPosibles.Count; i++)
                    {
                        if (hit.transform.gameObject == posicionesPosibles[i])
                        {
                            float step = speed * Time.deltaTime;
                            Is_moving = true;
                            animator.SetBool("Is_moving", Is_moving);

                            Vector3 position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 206.257f, Camera.main.ScreenToWorldPoint(Input.mousePosition).z);

                            var TileTarget = Instantiate(TileTargetPrefab, position, Quaternion.identity);

                            objetivo = corregirPosition(TileTarget.gameObject);

                            agente.destination = objetivo.transform.position;
                            transform.position = Vector3.MoveTowards(transform.position, TileTarget.transform.position, step);
                        }
                    }
                    
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
        if (other.tag == "Destino" && Is_moving )
        {
            
            isclicked = false;
            Is_moving = false;
            is_selected = false;

            animator.SetBool("Is_moving", Is_moving);


            GameManager.Instance.NextTurno();

        }

        if (other.tag == "Tile")
        {
            TileActual = other.gameObject;
        }
    }

    //Select a Unit
    private void OnClickedUnit()
    {

        if (GameManager.Instance.JugadorActual == 1)
        {
            if (Input.GetMouseButtonDown(1))
            {

                RaycastHit hit;

                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);


                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    for (int i = 0; i < GameManager.Instance.positions.Length; i++)
                    {

                        Debug.Log("You selected the " + hit.transform.name);
                        GameObject test = hit.transform.gameObject;

                        if (test.layer == (8))
                        {
                            is_selected = true;
                            agente = test.GetComponent<NavMeshAgent>();

                        }

                    }
                }
            }
        }
        else if (GameManager.Instance.JugadorActual == 2)
        {

            if (Input.GetMouseButtonDown(1))
            {

                RaycastHit hit;

                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);


                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    for (int i = 0; i < GameManager.Instance.positions.Length; i++)
                    {

                        Debug.Log("You selected the " + hit.transform.name);
                        GameObject test = hit.transform.gameObject;
                        Debug.Log(test.name);
                        if (test.layer == (9))
                        {
                            is_selected = true;

                            agente = test.GetComponent<NavMeshAgent>();

                        }

                    }
                }

            }
        }
    }
}


