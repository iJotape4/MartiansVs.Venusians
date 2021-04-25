using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Movement
{

    #region Private Properties
    Rigidbody rigidbody;
    Camera MainCamera;
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


        MainCamera= Camera.main.GetComponent<Camera>();

        DadosCamera = GameObject.FindGameObjectWithTag("DadosCamara").GetComponent<Camera>();
      //  DadosCamera.enabled = false;
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
            MainCamera.enabled = false;
            dado1.TirarDado();
        }

        while (dado1.IsMoving() )
        {
          
          DadosCamera.enabled = true;
         
            yield return new WaitForSeconds(0.001f);
        }


        yield return new WaitForSeconds(0.1f); ;


        DadosCamera.enabled = false;
        MainCamera.enabled = true;

        //TODO Aqui van los eventos  que se pueden ejecutar antes de terminar el turno

        //Termina el turno
        GameManager.Instance.NextTurno();
    }

    }

