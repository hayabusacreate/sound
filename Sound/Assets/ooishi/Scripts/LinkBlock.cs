using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum HitArea
{
    Up,
    Down,
    Side
}
public class LinkBlock : MonoBehaviour
{
    public bool hitflag;
    public Block hitblock;
    private Block block,save;
    private Renderer renderer;
    private bool check;
    private Player player;
    public bool attackflag;
    private float time;
    public HitArea area;
    // Start is called before the first frame update
    void Start()
    {
        check = false;
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    private void FixedUpdate()
    {
        if (!check)
        {
            block = transform.root.gameObject.transform.gameObject.GetComponent<Block>();
            check = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(attackflag)
        {
            time += Time.deltaTime;
            if(time>1)
            {
                attackflag = false;
                time = 0;
            }
        }
        else
        {
            time = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(area==HitArea.Side)
        {

            if (other.transform.tag == "Block")
            {
                save = other.transform.gameObject.GetComponent<Block>();
                if (block.block == save.block)
                {
                    other.transform.gameObject.GetComponent<Renderer>().material.color = Color.black;
                    hitflag = true;
                    hitblock = other.transform.GetComponent<Block>();
                }
                if (attackflag)
                {
                    attackflag = false;
                }
            }
            if (other.transform.tag == "Player")
            {
                if (other.transform.gameObject.GetComponent<Player>().attackflag)
                {
                    attackflag = true;
                }

            }
        }


    }
    private void OnTriggerExit(Collider other)
    {
        if(area==HitArea.Side)
        {
            if (other.transform.tag == "Block")
            {
                hitflag = false;
            }
            if (other.transform.tag == "Player")
            {
            }
        }

    }
}
