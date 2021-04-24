using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider))]

public class VulcanoTarget : MonoBehaviour
{
    #region Inspector properties


    #endregion

    #region Private Properties
    protected SpriteRenderer _spriteRenderer;
    protected Transform _transform;
    protected BoxCollider _boxCollider;
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
        _boxCollider.isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Activa el target un turno antes de que caiga la bola de fuego, y lo mantiene hasta el próximo turno.
        if ((GameManager.Instance.TurnoActual + 1) % 5 == 0 && !_spriteRenderer.enabled)
        {
            _transform.position = PrepareTarget();

        }


    }

    public Vector3 PrepareTarget()
    {
        int target = UnityEngine.Random.Range(0, GameManager.Instance.positions.Length);
        Transform targettrans;

        targettrans = GameManager.Instance.positions[target].transform;

        _spriteRenderer.enabled = true;

        //Coloca el target en una posición aleatoria del tablero.
        return new Vector3(targettrans.position.x, targettrans.position.y + 0.44f, targettrans.position.z);
    }

    //Desactiva el target después de que cayó la bola de fuego.
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Trowable")
        {
            _spriteRenderer.enabled = false;
        }
    }
}
