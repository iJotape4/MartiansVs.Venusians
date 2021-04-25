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
    private Vector3 posMouse;
    private Vector3 posActual;

    public bool isclicked=false;
    
    //public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        

        
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

        if (Vector3.Distance(posActual, posMouse)<Dado.Instance.NumeroActual)
        {
            
             Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit RaycastHit))
            {
                float step = speed * Time.deltaTime;

                Vector3 position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 206.257f, Camera.main.ScreenToWorldPoint(Input.mousePosition).z);

                var TileTarget = Instantiate(TileTargetPrefab, position, Quaternion.identity);

                objetivo = TileTarget.gameObject;

                agente.destination = objetivo.transform.position;
                transform.position = Vector3.MoveTowards(transform.position, TileTarget.transform.position, step);

                
                
            }
        }else{
                isclicked=false;
            }

        }

        }


    }

private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Destino")
        {
          isclicked=false;
            GameManager.Instance.NextTurno();
        }
    }
}
