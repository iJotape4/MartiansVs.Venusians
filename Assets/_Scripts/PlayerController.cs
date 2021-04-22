using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Movement
{

    #region Private Properties
    Rigidbody rigidbody;
    #endregion

    #region Inspector Properties
    public Dado dado1;
    public int PlayerTurn;
    public Camera DadosCamera;
    #endregion
    public float velocidad;

    public void awake(){
        //rigidbody=GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
          rigidbody=GetComponent<Rigidbody>(); 
          init();



        DadosCamera = GameObject.FindGameObjectWithTag("DadosCamara").GetComponent<Camera>();
        DadosCamera.enabled = false;
    }
    void update(){
    
        if (!moving)
        {
            FindSelectableTiles();
            CheckMouse();
        }else{
            Move();
        }
    
    }

    void CheckMouse(){

        if (Input.GetMouseButtonUp(0))
        {
                Ray ray= Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag=="Tile")
                    {
                        Tiles tile = hit.collider.GetComponent<Tiles>();
                        if (tile.selectable)
                        {
                            //todo move target
                            MoveToTile(tile);
                        }
                    }
                }
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float movH=Input.GetAxis("Horizontal");
        float movV=Input.GetAxis("Vertical");
        Vector3 movimiento= new Vector3 (movH,0,movV);
        rigidbody.AddForce(movimiento*velocidad);
    }
    void Update (){

        if(PlayerTurn != GameManager.Instance.JugadorActual)
        {
            return;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.X)){
                StartCoroutine(LanzarDado());
            }
        }
    }

    public IEnumerator LanzarDado()
    {

        dado1 = GameObject.Find("Dice").GetComponent<Dado>();

        if (!dado1.IsMoving())
        {
            dado1.TirarDado();
        }

        while (dado1.IsMoving() )
        {
            DadosCamera.enabled = true;
         
            yield return new WaitForSeconds(0.001f);
        }

     
        DadosCamera.enabled = false;
        

        yield return new WaitForSeconds(0.1f); ;


        //TODO Aqui van los eventos  que se pueden ejecutar antes de terminar el turno

        //Termina el turno
        GameManager.Instance.NextTurno();
    }

    }

