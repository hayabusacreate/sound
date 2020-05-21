using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMat : MonoBehaviour
{
    Material mat;
    
    private Block block;
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
    }

    
}
