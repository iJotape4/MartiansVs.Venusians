using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPackTarget : VulcanoTarget
{

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.prepareBlueTargets  && !_spriteRenderer.enabled)
        {
            _transform.position = PrepareTarget();
            _transform.SetParent (null);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Trowable")
        {
            Destroy(this.gameObject);
        }
    }
}
