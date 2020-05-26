using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMat : MonoBehaviour
{
    Material mat;
    
    private Block block;

    float th;

    public bool end = false;

    float cl;

    Color color;

    // Start is called before the first frame update
    void Start()
    {
        block = gameObject.GetComponent<Block>();
        mat = GetComponent<Material>();
    }

    // Update is called once per frame
    void Update()
    {
        

        //transform.GetComponent<Renderer>().material.SetFloat("_Threshold",1.0f - (block.hp / block.Maxhp) - 0.7f);
        transform.GetComponent<Renderer>().material.SetColor("_Color", new Color(1.0f, (block.hp / block.Maxhp), (block.hp / block.Maxhp), 1));
        if(block.hp < 0)
        {
            th += Time.deltaTime * 1f;
            cl += Time.deltaTime * 5f;
            color = new Color(cl*2, cl, cl/2, cl);
            transform.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
            transform.GetComponent<Renderer>().material.SetFloat("_Threshold", th);
        }

        if(th >= 1)
        {
            end = true;
        }

    }

    
}
