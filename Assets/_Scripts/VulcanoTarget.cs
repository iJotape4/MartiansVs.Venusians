using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class VulcanoTarget : MonoBehaviour
{
    #region Inspector properties

    public int target1;
    public Transform targettrans;

    #endregion

    #region Private Properties
    protected SpriteRenderer _spriteRenderer;
    protected Transform _transform;
    #endregion

     void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _transform = GetComponent<Transform>();


        if (_transform == null)
            Debug.LogError("Missing component !");

        if (_spriteRenderer == null)
            Debug.LogError("Missing component !");
    }

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Activa el target un turno antes de que caiga la bola de fuego, y lo mantiene hasta el próximo turno.
        if ((GameManager.Instance.TurnoActual+1) % 5 == 0 && !_spriteRenderer.enabled)
        {
            PrepareTarget();
            
        }

        //Desactiva el target después de que cayó la bola de fuego.
        if ((GameManager.Instance.TurnoActual-1) % 5 == 0 )
        {
            _spriteRenderer.enabled = false;
            
        }

      
    }

    void PrepareTarget()
    {
        Debug.Log(_transform.position);
        target1 = UnityEngine.Random.Range(0, GameManager.Instance.positions.Length);
        
        targettrans = GameManager.Instance.positions[target1].transform;
        if (targettrans == null)
        {
            return;
        }
        _spriteRenderer.enabled = true;

        //Coloca el target en una posición aleatoria del tablero.
        _transform.position = new Vector3(targettrans.position.x, targettrans.position.y+0.44f , targettrans.position.z);
    }
}
