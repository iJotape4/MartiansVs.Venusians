using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    #region Private Properties

    int LimitedeTurno = 2;
    GameObject[] board;
    [HideInInspector]
    public Transform[] positions;
    #endregion

    #region Inspector properties
    PlayerController PlayerActual;
    public int JugadorActual = 0;
    public int TurnoActual = 1;
    public string Tag;

    public bool prepareRedTargets, prepareBlueTargets, ThrowFire, ThrowJetPacks, EnableNextFire=false, EnableNextJetPack=false;
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

        board = GameObject.FindGameObjectsWithTag("Tile");
    }

    // Start is called before the first frame update
    void Start()
    {
      
        positions = new Transform[board.Length];

        for (int i=1; i<= LimitedeTurno; i++)
        {
            InsertarUltimo(i);
        }

        NodoTurno = Instance.raiz;
        JugadorActual = NodoTurno.info;

        for (int i = 0; i < board.Length; i++)
        {
            positions[i] = board[i].GetComponent<Transform>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        CalculateEvents();
        if (Input.GetButtonDown("Jump"))
        {
           
            NextTurno();
          
        }
        // Tag = ("Player" + JugadorActual);
        //PlayerActual = GameObject.FindGameObjectWithTag(Tag).GetComponent<PlayerController>();
    }

    void CalculateEvents()
    {
        if (TurnoActual != 1) { 

            if ((TurnoActual + 1) % 5 == 0 )
                prepareRedTargets = true;
            else
                prepareRedTargets = false;

            if ((TurnoActual + 1) % 10 == 0)
                prepareBlueTargets = true;
            else
                prepareBlueTargets = false;

            if (TurnoActual % 5 == 0)
                ThrowFire = true;
            else
                ThrowFire = false;

            if (TurnoActual % 10 == 0)
                ThrowJetPacks = true;
            else
                ThrowJetPacks = false;

            if ((TurnoActual - 1) % 5 == 0)
                EnableNextFire = true;
            else
                EnableNextFire = false;

            if ((TurnoActual - 1) % 10 == 0)
                EnableNextJetPack = true;
            else
                EnableNextJetPack = false;
        }
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
