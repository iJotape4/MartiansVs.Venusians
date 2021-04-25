using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    public GameObject objetivo;
    public Transform target;
    public GameObject TileTargetPrefab;
    public NavMeshAgent agente;
    private float speed = 20;
    //public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        agente = GetComponent<NavMeshAgent>();


        agente.destination=target.position;
    }

    // Update is called once per frame
    void Update()
    {



        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit RaycastHit))
            {
                float step = speed * Time.deltaTime;

                ///var objectPos = Camera.current.ScreenToWorldPoint(Input.mousePosition);
                Vector3 position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 206.257f, Camera.main.ScreenToWorldPoint(Input.mousePosition).z);

                var TileTarget = Instantiate(TileTargetPrefab, position, Quaternion.identity);

                objetivo = TileTarget.gameObject;

                agente.destination = objetivo.transform.position;
                transform.position = Vector3.MoveTowards(transform.position, TileTarget.transform.position, step);
            }
            /*
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                agente.destination = RaycastHit.point;
                Debug.Log(Input.mousePosition);
            }
*/


        }



        Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

}
