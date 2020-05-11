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
    //public Block hitblock;
    private Block block,save;
    private Renderer renderer;
    private bool check;
    private Player player;
    public bool attackflag;
    private float time;
    public HitArea area;
    public bool playerhit;
    public Block hitblock;
    private MapCreate map;
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
            if (hitblock != transform.root.gameObject.transform.GetComponent<Block>())
            {
                block = transform.root.gameObject.transform.gameObject.GetComponent<Block>();
                check = true;
                hitblock = transform.root.gameObject.transform.GetComponent<Block>();
                map = GameObject.Find("MapCreate").gameObject.GetComponent<MapCreate>();
            }

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
        if (playerhit)
        {
            if (player.attackflag)
            {
                attackflag = true;
            }
            else
            {
                attackflag = false;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Block")
        {

            //other.transform.gameObject.GetComponent<Renderer>().material.color = Color.black;
            hitflag = true;
            hitblock = other.gameObject.transform.GetComponent<Block>();

        }
        if (area == HitArea.Side)
        {
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
    private void OnTriggerEnter(Collider other)
    {
        if(area==HitArea.Side)
        {
            if (other.transform.tag == "Player")
            {
                playerhit = true;
                if (other.transform.gameObject.GetComponent<Player>().attackflag)
                {
                    attackflag = true;
                }
            }
        }
        if (other.transform.tag == "Block")
        {

            //other.transform.gameObject.GetComponent<Renderer>().material.color = Color.black;
            hitflag = true;
            hitblock = other.gameObject.transform.GetComponent<Block>();

        }if(area == HitArea.Up)
        {
            if (other.transform.tag == "Block")
            {

                other.transform.gameObject.GetComponent<Block>().rigidbody.isKinematic = true;
                //block.hight = other.transform.gameObject.GetComponent<Block>().hight - 1;

            }
        }
        if (other.transform.tag == "Ground")
        {

            //other.transform.gameObject.GetComponent<Block>().rigidbody.isKinematic = true;
            block.hight = map.maps.Length-1;

        }


    }
    private void OnTriggerExit(Collider other)
    {
        if(area==HitArea.Side)
        {

            if (other.transform.tag == "Player")
            {
                playerhit = false;
                attackflag = false;
            }
        }
        if (other.transform.tag == "Block")
        {
            hitflag = false;
            hitblock = transform.root.gameObject.transform.GetComponent<Block>();
        }

    }
}
