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
    private Animator animator;
    private Vector3 posMouse;
    private Vector3 posActual;

    public bool isclicked=false;
    
    //public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        
    }

    // Update is called once per frame
    void colorear(){
        

    }
    void Update()
    {   

        
        posActual=agente.transform.position;
        if (this.GetComponent<PlayerController>().PlayerTurn == GameManager.Instance.JugadorActual) { 

        if (Input.GetMouseButtonDown(0)&& !isclicked){
        isclicked=true;
        posMouse= new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 206.257f, Camera.main.ScreenToWorldPoint(Input.mousePosition).z);

        //El +0.5 es para que pueda tomar las casillas diagonales al clickar en el centro ded ellas
        if (Vector3.Distance(posActual, posMouse)<Dado.Instance.NumeroActual + 0.5  )
        {
            
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
        }else{
                isclicked=false;
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
            if(provisional< Nearest  && provisional < Dado.Instance.NumeroActual)
            {
                Nearest = provisional;
                NearestObject = GameManager.Instance.board[i];
            }
        }
        Tiletarget.transform.position = new Vector3(NearestObject.transform.position.x, Tiletarget.transform.position.y, NearestObject.transform.position.z);
        return Tiletarget;
    }

private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Destino")
        {
            isclicked = false;
            Is_moving = false;
            animator.SetBool("Is_moving", Is_moving);
            GameManager.Instance.NextTurno();
        }
    }

    private void OnClickedUnit()
    {
        if (Input.GetMouseButtonDown(0)){

                RaycastHit hit;

                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                
                if ( Physics.Raycast ( ray, out hit, 100.0f))
                {
                    for ( int i =0; i < GameManager.Instance.positions.Length; i++)
                {
                    if(GameManager.Instance.positions[i].gameObject == hit.transform.gameObject)
                    {
                        Debug.Log("You selected the " + hit.transform.name);
                        GameObject test = hit.transform.gameObject;
                        /*
                        if (test.LayerMask.NameToLayer("Venusian"))
                        {
                            
                        }
                        */
                    }
                }
                }
                

                
        }
    }
}


