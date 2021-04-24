using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class FireBall : MonoBehaviour
{

    #region Inspector properties
    public VulcanoTarget _target;
    public float speed = 2f;
    public bool CanLaunch = true;
    #endregion

    #region Private Properties
    protected Rigidbody _rigidbody;
    protected CapsuleCollider _capsuleCollider;
    protected Transform _transform;

    public Vector3 _initPos;
    #endregion

    private void Awake()
    {
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
        

        //_target = GetComponentInParent<VulcanoTarget>();

        if (_transform == null)
            Debug.LogError("Missing component !");
        if (_capsuleCollider == null)
            Debug.LogError("Missing component !");
        if (_rigidbody == null)
            Debug.LogError("Missing component !");
        if (_target == null)
            Debug.LogError("Missing component !");
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody.useGravity = false;
        _initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {       
      
        // Dispara la bola
        if (GameManager.Instance.TurnoActual % 5 == 0 && CanLaunch)
        {
            ShootFireball();
        }

        //Envía la bola al target cuando llega a su altura máxima
        if (_rigidbody.velocity.y <= -0.1f)
        {
            MoveToTarget();
        }

        if ((GameManager.Instance.TurnoActual-1) % 5 == 0 )
        {
            //Habilita la condición de lanzamiento cuando empieza el próximo turno.
            CanLaunch = true;
        }
    }
    

    void ShootFireball()
    {
        CanLaunch =false;
        _rigidbody.useGravity = true;
        _rigidbody.AddForce(new Vector3(0f, 10f, 0f), ForceMode.Impulse);

    }

     void MoveToTarget()
    {
        _rigidbody.useGravity = false;
        //Apunta la bola hacia el target
        _transform.LookAt(_target.transform.position);
        //Mueve la bola hacia el target
        float step = speed * Time.deltaTime; 
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, step);
       
    }

    //Resetea la bola y la deja lista para el próximo lanzamiento.
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Target")
        {
            //Retorno a la posición inicial
            _transform.position = _initPos;

            //Coloca la rotación inicial
            _transform.rotation = Quaternion.Euler(270f, 0f, 0f);
            // Elimina las velocidades.
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
     
        }
    }

    public void ActivateLaunch()
    {
        }

    }

