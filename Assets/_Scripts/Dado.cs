﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dado : MonoBehaviour
{   
    public static Dado Instance;

    public cara[] caras;
    public int NumeroActual;
    public Vector3 PosInicial;
 
    public bool moviendo;


     private void Awake()
    {
        if (Dado.Instance == null)
        {
            Dado.Instance = this.GetComponent<Dado>();

        }
        else if (Dado.Instance != null && Dado.Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
        NumeroDado();
        caras = GetComponentsInChildren<cara>();
        
        PosInicial = GetComponent<Dado>().transform.position;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        IsMoving();
        if (Input.GetButtonDown("Submit"))
        {
            TirarDado();
            
        }
    }

    void NumeroDado()
    {
        for (int i =0; i<caras.Length; i++)
        {
            if (caras[i].TocaSuelo)
            {
                NumeroActual = 7 - caras[i].Numero;
            }
        }
        Invoke("NumeroDado", 0.5f);

    }

    public void TirarDado()
    {
        UIManager.Instance.DesaactivateUiCon("UiconDices");
       
        transform.position = PosInicial;
        float FuerzaInicial = Random.Range(-10, 10 );
        float FuerzaInicial2 = Random.Range(10, 10);
        float multplier = Random.Range(20, 20);
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(new Vector3(FuerzaInicial * multplier, 0, FuerzaInicial2* multplier));
        GetComponent<Rigidbody>().rotation = Random.rotation;
    }

    public bool IsMoving()
    {
        return  !GetComponent<Rigidbody>().IsSleeping();
    }
}
