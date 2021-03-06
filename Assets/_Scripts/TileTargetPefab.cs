using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TileTargetPefab : MonoBehaviour
{
    #region Private Region
    private BoxCollider _boxCollider;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
    _boxCollider=GetComponent<BoxCollider>();
    _boxCollider.isTrigger=true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Unit")
        {
            Destroy(this.gameObject);
            
        }
    }
    
}
