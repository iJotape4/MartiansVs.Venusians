using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nave : MonoBehaviour
{
    #region Inspector properties
    public float speed = 2f;
    public GameObject _target;
    public bool CanFly =true;
    #endregion

    #region Private Properties
   
    protected Transform _transform;
    protected MeshRenderer _meshRenderer;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.ThrowJetPacks && CanFly)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, step);

            //Cambiar posicion
            if (Vector3.Distance(transform.position, _target.transform.position) < 0.001f)
            {  
                _target.transform.localPosition *= -1.0f;
                _transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
                CanFly = false;
            }
           
        }

        if (GameManager.Instance.EnableNextJetPack)
            CanFly = true;
    }
}
