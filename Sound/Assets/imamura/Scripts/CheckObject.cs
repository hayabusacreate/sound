using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckObject : MonoBehaviour
{

    Vector3 pos;
    
    Camera mainCamera;
    
    GameObject gameObj;


    bool a = false;

    void Start()
    {
        
    }

    void Update()
    {
        pos = transform.GetComponent<Transform>().position;
        Ray ray = new Ray(pos,pos +new Vector3(100,10));
        // RaycastHit hit;
        //foreach (RaycastHit hit in Physics.CapsuleCastAll(pos + new Vector3(1, 5,-3.5f), pos + new Vector3(-1, 5,-3.5f), 3, new Vector3(0, 5,-10)))
        foreach (RaycastHit hit in Physics.BoxCastAll(pos + new Vector3(0, 11, 0), Vector3.one * 10f, new Vector3(0, 10, 0), Quaternion.identity, 10f))
        {
                if (hit.collider.tag == "Block")
                {
                    gameObj = hit.collider.gameObject;

                    gameObj.GetComponent<ChangeMat>().CMat();
                }
            }
        
        

    }

    
}