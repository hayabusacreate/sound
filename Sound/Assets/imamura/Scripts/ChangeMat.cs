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
            else
            {
                transform.GetComponent<Renderer>().material = BejitablMat;
            }

        }

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
