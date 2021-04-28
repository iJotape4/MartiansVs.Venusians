using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{

    #region Private Properties
    Rigidbody _rigidbody;
    Camera _mainCamera;
    #endregion

    #region Inspector Properties
    public Dado dado1;
    public int PlayerTurn;
    public Camera DadosCamera;
    public float velocidad;
    #endregion



    void Start()
    {
          _rigidbody=GetComponent<Rigidbody>();


        _mainCamera= Camera.main.GetComponent<Camera>();

        DadosCamera = GameObject.FindGameObjectWithTag("DadosCamara").GetComponent<Camera>();
    }
   
    void FixedUpdate()
    {
        float movH=Input.GetAxis("Horizontal");
        float movV=Input.GetAxis("Vertical");
        Vector3 movimiento= new Vector3 (movH,0,movV);
        _rigidbody.AddForce(movimiento*velocidad);
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
            _mainCamera.enabled = false;
            dado1.TirarDado();
        }

        while (dado1.IsMoving() )
        {
          
          DadosCamera.enabled = true;
         
            yield return new WaitForSeconds(0.001f);
        }


        yield return new WaitForSeconds(0.1f); ;


        DadosCamera.enabled = false;
        _mainCamera.enabled = true;

        UIManager.Instance.ActivateUiCon("UiconAllien");
        UIManager.Instance.DesaactivateUiCon("UiconDices");
        UIManager.Instance.DesaactivateUiCon("UiconCasillas");

    }

    }

