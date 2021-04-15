using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    #region Private Properties
   
    int LimitedeTurno = 2;
    #endregion

    #region Public properties
    PlayerController PlayerActual;
    public int JugadorActual = 0;
    public int TurnoActual = 1;
    public string Tag;
    #endregion

    
   

    private void Awake()
    {
        if (GameManager.Instance == null)
        {
            GameManager.Instance = this.GetComponent<GameManager>();

        }
        else if (GameManager.Instance != null && GameManager.Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i=1; i<= LimitedeTurno; i++)
        {
            InsertarUltimo(i);
        }

        NodoTurno = Instance.raiz;
        JugadorActual = NodoTurno.info; 

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
           
            NextTurno();
          
        }

       // Tag = ("Player" + JugadorActual);
        //PlayerActual = GameObject.FindGameObjectWithTag(Tag).GetComponent<PlayerController>();
    }

    //Éste método maneja el cambio de turnos
    public void NextTurno()
    {
        NodoTurno = NodoTurno.sig;
        JugadorActual = NodoTurno.info;
        TurnoActual += 1;
    }

    class Nodo
    {
        public int info;
        public Nodo ant, sig;
    }

    private Nodo raiz;
    private Nodo NodoTurno;

    public GameManager()
    {
        raiz = null;
    }

    public void InsertarUltimo(int x)
    {
        Nodo nuevo = new Nodo();
        nuevo.info = x;     
        if (raiz == null)
        {
            nuevo.sig = nuevo;
            nuevo.ant = nuevo;
            raiz = nuevo;
        }
        else
        {
            Nodo ultimo = raiz.ant;
            nuevo.sig = raiz;
            nuevo.ant = ultimo;
            raiz.ant = nuevo;
            ultimo.sig = nuevo;
        }
    }
}
