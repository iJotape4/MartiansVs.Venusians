using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nave : MonoBehaviour
{
    #region Inspector properties
    public float speed = 2f;
    public GameObject _target;
    public GameObject JetPackPrefab;
    public bool CanFly =true;
    public int JetPacksCount=2;
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

        if (GameManager.Instance.prepareBlueTargets && JetPacksCount>0)
            StartCoroutine(GenerateJetPackElement());

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
                JetPacksCount += 2;
            }
           
        }

        if (GameManager.Instance.EnableNextJetPack)
            CanFly = true;
            
    }

    public IEnumerator GenerateJetPackElement()
    {
        var jetPack = Instantiate(JetPackPrefab, transform.position, Quaternion.identity);
        jetPack.transform.SetParent(this.transform);
        jetPack.transform.localPosition = new Vector3 (0f, 0f, 0f);
        JetPacksCount -= 1;

       yield  return null;
    }

}
