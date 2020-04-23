using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMat : MonoBehaviour
{
    Material mat;
    bool change = false;

    [SerializeField]
    Material alphaMat;

    [SerializeField]
    Material defaultMat;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (change == true)
        {
            transform.GetComponent<Renderer>().material = alphaMat;
        }
        else
        {
            transform.GetComponent<Renderer>().material = defaultMat;
        }

        change = false;
    }

    public void CMat()
    {
        change = true;
        
    }

    public void CfMat()
    {
        change = false;

    }
}
