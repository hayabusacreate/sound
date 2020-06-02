using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMat : MonoBehaviour
{
    Material mat;
    
    private Block block;

    //溶岩解け
    float th;
    
    //氷解け
    float th2;

    public bool end = false;

    float cl;

    Color color;

    [SerializeField]
    Material yMat;

    public int num = 0;

    // Start is called before the first frame update
    void Start()
    {
        block = gameObject.GetComponent<Block>();
        mat = GetComponent<Material>();
        transform.GetChild(num).gameObject.GetComponent<Renderer>().material.SetFloat("_Threshold", 1);
        //transform.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.2f, 0.2f, 0.2f, 1));
        //transform.GetComponent<Renderer>().material.SetColor("_Color", new Color(0.2f, 0.2f, 0.2f, 1));
    }

    // Update is called once per frame
    void Update()
    {
        if (th2 >= 1)
        {
            transform.GetChild(num).gameObject.GetComponent<Renderer>().material.SetFloat("_Threshold", 1);
        }

        //transform.GetComponent<Renderer>().material.SetFloat("_Threshold",1.0f - (block.hp / block.Maxhp) - 0.7f);
        //transform.GetComponent<Renderer>().material.SetColor("_Color", new Color(1.0f, (block.hp / block.Maxhp), (block.hp / block.Maxhp), 1));

        if (block.type == "4")
        {
            transform.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.2f, 0.2f, 0.2f, 1));
            transform.GetComponent<Renderer>().material.SetColor("_Color", new Color(0.2f, 0.2f, 0.2f, 1));
        }

        if (block.type != "3")
        {
            if(block.type == "5" && th2 <= 1)
            {
                transform.GetChild(num).gameObject.GetComponent<Renderer>().material.SetFloat("_Threshold",0);
            }
            if (block.hp <= 3)
            {
                if (th2 <= 1)
                {
                    transform.GetChild(num).gameObject.GetComponent<Renderer>().material.SetFloat("_Threshold", th2 += Time.deltaTime);
                }
            }
                if (block.hp == 3)
            {
                if(th2 <= 1)
                {
                    transform.GetChild(num).gameObject.GetComponent<Renderer>().material.SetFloat("_Threshold", th2+=Time.deltaTime);
                }
                
                transform.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.0f, 0.4f, 0.8f, 1));
                transform.GetComponent<Renderer>().material.SetColor("_Color", new Color(0.0f, 0.4f, 0.8f, 1));
            }
            else if (block.hp == 2)
            {
                transform.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.4f, 0.1f, 0.1f, 1));
                transform.GetComponent<Renderer>().material.SetColor("_Color", new Color(0.3f, 0.3f, 0.3f, 1));
            }
            else if (block.hp == 1)
            {
                transform.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(1f, 0.3f, 0.2f, 1));
                transform.GetComponent<Renderer>().material.SetColor("_Color", new Color(1f, 0.6f, 0.5f, 1));
            }
            else if (block.hp == 0)
            {
                transform.GetComponent<Renderer>().material = yMat;
            }
        }


        if (block.hp < 0)
        {
            th += Time.deltaTime * 4f;
            cl += Time.deltaTime * 5f;
            color = new Color(cl * 2, cl, cl / 2, cl);
            transform.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
            transform.GetComponent<Renderer>().material.SetFloat("_Threshold", th);
        }

        if (th >= 1)
        {
            end = true;
        }

    }

    
}
