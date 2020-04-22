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
        foreach (RaycastHit hit in Physics.CapsuleCastAll(pos + new Vector3(1, 2,1), pos + new Vector3(1, 2,-1), 2, new Vector3(10, 0,0)))
            {
                if (hit.collider.tag == "Block")
                {
                    gameObj = hit.collider.gameObject;

                    gameObj.GetComponent<ChangeMat>().CMat();
                }
            }
        
        

    }

    
}