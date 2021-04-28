using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dado : MonoBehaviour
{   
    public static Dado Instance;

    #region Inspector Properties  
    public int _numeroActual;
    public bool _moviendo;
    #endregion

    #region Private Properties
    public cara[] _caras;
    protected Vector3 _posInicial;
    #endregion

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
        _caras = GetComponentsInChildren<cara>();
        
        _posInicial = GetComponent<Dado>().transform.position;
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
        for (int i =0; i<_caras.Length; i++)
        {
            if (_caras[i].TocaSuelo)
            {
                _numeroActual = 7 - _caras[i].Numero;
            }
        }
        Invoke("NumeroDado", 0.5f);

    }

    public void TirarDado()
    {
        UIManager.Instance.DesaactivateUiCon("UiconDices");
       
        transform.position = _posInicial;
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

    public void ResetPos()
    {
        transform.position = _posInicial;
        GetComponent<Rigidbody>().isKinematic = true;
        _numeroActual = 0;
    }
}
