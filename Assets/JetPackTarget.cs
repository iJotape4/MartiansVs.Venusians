using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPackTarget : VulcanoTarget
{

    // Update is called once per frame
    void Update()
    {
        if ((GameManager.Instance.TurnoActual + 1) % 2 == 0 && !_spriteRenderer.enabled)
        {
            _transform.position = PrepareTarget();

        }
    }
}
