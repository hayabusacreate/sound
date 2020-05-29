using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wipeCircle : MonoBehaviour
{
    [SerializeField]
    Material wipeMat;

    float value;

    void Start()
    {
        if(gameObject.name == "Open")
        {
            value = 0;
            wipeMat.SetFloat("_Radius", 0);
        }
        else if (gameObject.name == "Close")
        {
            value = 2;
            wipeMat.SetFloat("_Radius", 2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.Space))
        //{
        //    value = value + Time.deltaTime;
        //    wipeMat.SetFloat("_Radius", value);
        //    Debug.Log(wipeMat);
        //}
        if (gameObject.name == "Open" && value <= 2)
        {
            value = value + Time.deltaTime;
            wipeMat.SetFloat("_Radius", value);
            Debug.Log(wipeMat);
        }
        else if (gameObject.name == "Close" && value >= 0)
        {
            value = value - Time.deltaTime;
            wipeMat.SetFloat("_Radius", value);
            Debug.Log(wipeMat);
        }

    }
}
