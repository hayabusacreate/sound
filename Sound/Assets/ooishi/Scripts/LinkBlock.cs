﻿using System.Collections;
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
    //public Block hitblock;
    private Block block,save;
    private Renderer renderer;
    private bool check;
    private Player player;
    public bool attackflag;
    private float time;
    public HitArea area;
    public bool playerhit;
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
            //hitblock = transform.root.gameObject.transform.GetComponent<Block>();
        }
    }
    // Update is called once per frame
    void Update()
    {
        //if(attackflag)
        //{
        //    time += Time.deltaTime;
        //    if(time>6)
        //    {
        //        attackflag = false;
        //        time = 0;
        //    }
        //}
        //else
        //{
        //    time = 0;
        //}
        if(playerhit)
        {
            if (player.attackflag)
            {
                attackflag = true;
            }else
            {
                attackflag = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(area==HitArea.Side)
        {
            //if (other.transform.tag == "Block")
            //{
            //    save = other.transform.gameObject.GetComponent<Block>();
            //    if (block.block == save.block)
            //    {
            //        other.transform.gameObject.GetComponent<Renderer>().material.color = Color.black;
            //        hitflag = true;
            //        hitblock = other.gameObject.transform.GetComponent<Block>();
            //    }
            //}else
            if (other.transform.tag == "Player")
            {
                playerhit = true;
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
            //if (other.transform.tag == "Block")
            //{
            //    hitflag = false;
            //    hitblock =transform.root.gameObject.transform.GetComponent<Block>();
            //}else
            if (other.transform.tag == "Player")
            {
                playerhit = false;
            }
        }

    }
}
