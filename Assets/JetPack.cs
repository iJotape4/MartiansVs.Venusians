﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JetPack : FireBall
{

    #region Private Properties
    protected MeshRenderer _meshRenderer;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        _meshRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.ThrowJetPacks)
        {
            MoveToTarget();
        }

        if (GameManager.Instance.EnableNextJetPack)
        {
            
        }
    }

     void MoveToTarget()
    {
        if(_target == null)
        {
            return;
        }

        transform.SetParent(null);
        _meshRenderer.enabled = true;
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, step);
        _rigidbody.AddRelativeTorque(new Vector3(0f, 1f, 0f), ForceMode.Force );
    }
}
