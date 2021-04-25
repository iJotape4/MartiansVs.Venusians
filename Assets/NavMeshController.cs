using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{

    public GameObject objetivo;
    public GameObject TileTargetPrefab;
    public NavMeshAgent agente;
    //public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        agente=GetComponent<NavMeshAgent>();


       //agente.destination=objetivo.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {


            /*  RaycastHit hit;
              if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit))
              {
                  agente.destination=hit.point;
              }*/

            //var objectPos = Camera.current.ScreenToWorldPoint(Input.mousePosition);

            Vector3 position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,206.257f, Camera.main.ScreenToWorldPoint(Input.mousePosition).z);
          var TileTarget = Instantiate(TileTargetPrefab, position, Quaternion.identity);
               
                objetivo=TileTarget.gameObject;
                 agente.destination=objetivo.transform.position;
            
        }
         Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
  
}
