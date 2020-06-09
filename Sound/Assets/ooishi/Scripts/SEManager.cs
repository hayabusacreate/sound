using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    public GameObject se;
    public GameObject iceobj;
    public bool pon;
    public bool ice;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pon)
        {
            Instantiate(se, transform.position, Quaternion.identity);
            pon = false;
        }
        if(ice)
        {
            Instantiate(iceobj, transform.position, Quaternion.identity);
            ice = false;
        }
    }
}
