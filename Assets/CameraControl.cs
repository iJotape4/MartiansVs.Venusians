using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] Vector3 posInicial =  new Vector3(5.36f,212.46f,0.5f);
     [SerializeField] Vector3 posFinal = new Vector3(0.57f,210.43f,-0.41f);
     [SerializeField] Vector3 rotInicial= new Vector3(90,0,0) ;
     [SerializeField] float sizeInicial =4.98f;
     [SerializeField] float sizeFinal =3.57f;
     [SerializeField] Vector3 rotFinal =new Vector3 (23.9f,90,0) ;


    // Start is called before the first frame update
    void Start()
    {
       GoInicial();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


public void GoInicial(){
   transform.position =posInicial;
   Camera.main.orthographicSize=sizeInicial;
   transform.rotation =Quaternion.Euler(rotInicial);
}


 public void  GoFinal(){
     transform.position =posInicial;
    transform.rotation =Quaternion.Euler(rotFinal);
    Camera.main.orthographicSize=sizeFinal;

}

}
