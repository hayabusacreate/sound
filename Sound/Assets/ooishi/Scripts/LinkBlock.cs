using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkBlock : MonoBehaviour
{
    public bool hitflag;
    public Block hitblock;
    private Block block,save;
    private Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        block = transform.root.gameObject.transform.gameObject. GetComponent<Block>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag=="Block")
        {
            save = other.transform.gameObject.GetComponent<Block>();
            if(block.block==save.block)
            {
                other.transform.gameObject.GetComponent<Renderer>().material.color = Color.black;
                hitflag = true;
                hitblock = other.transform.GetComponent<Block>();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Block")
        {
            hitflag = false;
        }
    }
}
