using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPos : MonoBehaviour
{
    GameObject camera;
    
    [SerializeField]
    Material mat;

    bool turned;

    SceneChange sc;

    float value;

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("CM vcam2");
        sc = GameObject.Find("SceneChange").GetComponent<SceneChange>();
        value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = camera.transform.position + new Vector3(0,0,-10);
        
        if(mat.GetFloat("_Radius") >= 1 && turned == false)
        {
            transform.Rotate(0, 180, 0);
            turned = true;
        }

        if(sc.creaflag == true)
        {
            value += Time.deltaTime;
            //gameObject.GetComponent<Renderer>().material.SetFloat("_Threshold", value);
            foreach(Transform child in transform)
            {
                child.GetComponent<Renderer>().material.SetFloat("_Threshold", value);
            }
        }
    }
}
