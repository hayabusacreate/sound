using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waipeTest : MonoBehaviour
{
    float value;
    // Start is called before the first frame update
    void Start()
    {
        value =  10;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            value += Time.deltaTime;
            transform.GetComponent<Renderer>().material.mainTextureScale = new Vector2(value, value);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            value -= Time.deltaTime;
            transform.GetComponent<Renderer>().material.mainTextureScale = new Vector2(value, value);
        }
    }
}
