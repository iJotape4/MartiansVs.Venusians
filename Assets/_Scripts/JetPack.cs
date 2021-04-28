using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
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

        if (_meshRenderer == null)
            Debug.LogError("Missing component !");
    }

  
    void Update()
    {
        //Lanza los JetPacks en el turno correspondiente
        if (GameManager.Instance.ThrowJetPacks)
        {
            MoveToTarget();
        }
    }


     void MoveToTarget()
    {
        //Evita ejecutar el método si el target se destruyó. Los targets se destruyen para evitar que calcule una nueva posición después de colisionar con el paquete.
        if (_target == null)
        {
            return;
        }

        //En esta parte, se destruye el objeto residual padre de "JetPackPrefab". Así mismo, se asigna como nuevo padre a la nave espacial.
        Transform JetPackPrefab;
        if (transform.parent != null && transform.parent != Nave.Instance.transform)
        {
             JetPackPrefab = this.transform.parent;
            transform.SetParent(Nave.Instance.transform);
            Destroy(JetPackPrefab.gameObject);
      
        }

        //Los paquetes se dirigen hacia tu target desde que incia el método, pero siguen moviendose a la par con la nave y son invisibles hasta que la nave llega a la mitad del trayecto.
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, step);
        _rigidbody.AddRelativeTorque(new Vector3(0f, 1f, 0f), ForceMode.Force);


        //Calcula que la Nave esté en la mitad del recorrido, para así lanzar los paquetes.(En realidad en este punto los hace visibles, pues se sueltan un poco antes)
        if (Vector3.Distance(Nave.Instance.transform.position, Nave.Instance._target.transform.position) <= Nave.Instance.naveDistance)
        {
            transform.SetParent(null);
            _meshRenderer.enabled = true;                   
        }
    }
}
