using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wipeCircle : MonoBehaviour
{
    [SerializeField]
    Material wipeMat;

    float value;

    public float point1, point2,speed1,speed2, speed3;

    bool one, two;

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

        one = false;
        two = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (gameObject.name == "Open")
        {
            if(value <= point1 && one == false)
            {
                value = Mathf.Lerp(value, point1, Time.deltaTime * speed1);
                wipeMat.SetFloat("_Radius", value);
                if(value >= (point1 - 0.1f))
                {
                    one = true;
                }
            }
            else if(one == true && two == false)
            {
                value = Mathf.Lerp(value, point2, Time.deltaTime * speed2);
                wipeMat.SetFloat("_Radius", value);
                if (value <= (point2 + 0.1f))
                {
                    two = true;
                }
            }
            else if (value <= 2 && two == true && one == true)
            {
                value = Mathf.Lerp(value, 2, Time.deltaTime * speed3);
                wipeMat.SetFloat("_Radius", value);
            }
        }
        else if (gameObject.name == "Close")
        {
            if(value >= point1 && one == false)
            {
                value = Mathf.Lerp(value, point1, Time.deltaTime * speed1);
                wipeMat.SetFloat("_Radius", value);
                if(value <= (point1 + 0.1f))
                {
                    one = true;
                }
            }
            else if(one == true && two == false)
            {
                value = Mathf.Lerp(value, point2, Time.deltaTime * speed2);
                wipeMat.SetFloat("_Radius", value);
                if (value >= (point2 - 0.1f))
                {
                    two = true;
                }
            }
            else if (value <= 2 && two == true && one == true)
            {
                value = Mathf.Lerp(value, 0, Time.deltaTime * speed3);
                wipeMat.SetFloat("_Radius", value);
            }
        }

    }
}
