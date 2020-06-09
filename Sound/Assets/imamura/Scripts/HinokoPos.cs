using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HinokoPos : MonoBehaviour
{
    GameObject camera;

    bool turned;
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("CM vcam2");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = camera.transform.position + new Vector3(0, 3.8f, -10);
    }  
}
