using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    public GameObject se;
    public bool pon;
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
    }
}
