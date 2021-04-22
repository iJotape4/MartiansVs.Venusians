using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class FireBall : MonoBehaviour
{

    #region Inspector properties
    public VulcanoTarget _target;
    public float speed = 10f;
    public bool CanLaunch = true;
    #endregion

    #region Private Properties
    protected Rigidbody _rigidbody;
    protected CapsuleCollider _capsuleCollider;
    protected Transform _transform;
    public GameObject _fireball;
    public Transform _initPos;
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
       // _fireball.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {       
      
        // Check if the position of the cube and sphere are approximately equal.

        if (GameManager.Instance.TurnoActual % 5 == 0 && CanLaunch)
        {
            _fireball.SetActive(true);
            ShootFireball();
        }

        if (_rigidbody.velocity.y <= -0.1f)
        {
            MoveToTarget();
        }
    }

    void ShootFireball()
    {
        CanLaunch =false;
        _rigidbody.useGravity = true;
        _rigidbody.AddForce(new Vector3(0, 10f, 0), ForceMode.Impulse);
        _rigidbody.mass *= 0.5f;
    }

    void MoveToTarget()
    {

        _transform.rotation = Quaternion.Euler(45f, 90f, 90f);
        float step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, step);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Target")
        {
            _rigidbody.useGravity = false;
            _fireball.SetActive(false);
            _transform = _initPos;
        }
    }
}
