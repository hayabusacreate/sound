using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wipeTex : MonoBehaviour
{
    [SerializeField]
    Material wipeMat;

    public float value,point1, point2,speed1,speed2, speed3, max, min;

    bool one, two;
    public bool opened,closed;

    void Start()
    {
        if(gameObject.name == "Close")
        {
            value = min;
            wipeMat.SetFloat("_Radius", min);
        }
        else if (gameObject.name == "Open")
        {
            value = max;
            wipeMat.SetFloat("_Radius", max);
        }

        one = false;
        two = false;
        opened = false;
        closed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (gameObject.name == "Close")
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
            else if (value <= max && two == true && one == true)
            {
                value = Mathf.Lerp(value, max, Time.deltaTime * speed3);
                wipeMat.SetFloat("_Radius", value);
            }
        }
        else if (gameObject.name == "Open")
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
            else if (value <= max && two == true && one == true)
            {
                value = Mathf.Lerp(value, min, Time.deltaTime * speed3);
                wipeMat.SetFloat("_Radius", value);
            }
        }

        if (value >= max && one == true && two == true)
        {
            closed = true;
        }
        else if(value <= max && one == true && two == true)
        {
            opened = true;
        }


    }
}
