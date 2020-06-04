using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titleEffect : MonoBehaviour
{
    [SerializeField]
    GameObject shell;

    Color EColor;

    bool cColor;

    public bool exEnd;

    float cl;

    // Start is called before the first frame update
    void Start()
    {
        //shell.GetComponent<Renderer>().material
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            cColor = true;
        }

        if(cColor == true)
        {
            if (cl <= 1)
            {
                cl += Time.deltaTime;
                EColor = new Color(cl, cl - 0.4f, cl - 0.7f);
            }
            shell.GetComponent<Renderer>().material.SetColor("_EmissionColor", EColor);
        }

        if(cl >= 1)
        {
            exEnd = true;
        }
    }
}
