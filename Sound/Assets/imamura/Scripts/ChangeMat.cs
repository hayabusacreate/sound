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
    Material HedroMat;
    [SerializeField]
    Material BejitablMat;
    [SerializeField]
    Material TomatMat;
    [SerializeField]
    Material FishMat;
    private Block block;
    // Start is called before the first frame update
    void Start()
    {
        block = gameObject.GetComponent<Block>();
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
            if(block.block==BlockType.Fire)
            {
                transform.GetComponent<Renderer>().material = HedroMat;
            }
            else if(block.block==BlockType.Nomal)
            {
                transform.GetComponent<Renderer>().material = BejitablMat;
            }
            else if (block.block == BlockType.Fish)
            {
                transform.GetComponent<Renderer>().material = TomatMat;
            }
            else if (block.block == BlockType.Etc)
            {
                transform.GetComponent<Renderer>().material = FishMat;
            }

        }

        transform.GetComponent<Renderer>().material.SetFloat("_Threshold",1.0f - (block.hp / block.Maxhp));
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
