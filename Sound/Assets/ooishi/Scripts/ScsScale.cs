using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScsScale : MonoBehaviour
{
    public bool inout;
    public bool parfect;
    private float scale;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(inout)
        {
            if (scale <= 1)
            {
                scale += 10 * Time.deltaTime;
                if (scale > 1)
                {
                    scale = 1;
                    parfect = true;
                }
            }
        }else
        {
            if (scale >= 0)
            {
                scale -= 10 * Time.deltaTime;
                if (scale < 0)
                {
                    scale = 0;
                    parfect = true;
                }
            }
        }

        transform.localScale = new Vector3(scale, scale, scale);
    }
}
