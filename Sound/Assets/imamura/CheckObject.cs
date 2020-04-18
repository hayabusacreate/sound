using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckObject : MonoBehaviour
{
    

    
    Camera mainCamera;
    
    GameObject gameObj;


    bool a = false;

    void Update()
    {
     
        Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2 -5);
        Ray ray = Camera.main.ScreenPointToRay(center);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            if(hit.collider.tag == "Block")
            {
                gameObj = hit.collider.gameObject;

                gameObj.GetComponent<ChangeMat>().CMat();
            }
        }
        else
        {
            
        }



        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 5);

    }

    
}