using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPackTarget : VulcanoTarget
{

    // Update is called once per frame
    void Update()
    {
        //Coloca el Target en el tablero y lo hace un objeto independiente
        if (GameManager.Instance.prepareBlueTargets  && !_spriteRenderer.enabled)
        {
            _transform.position = PrepareTarget();
            _transform.SetParent (null);
        }
    }

    //Destruye los targets azules cuando un JetPack colisiona con ellos. ESto para evitar que un mismo target se reasigne en otra posición del tablero
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Trowable")
        {
            Destroy(this.gameObject);
        }
    }
}
