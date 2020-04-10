using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BlockType
{
    Nomal,
    Fire
}
public class Block : MonoBehaviour
{
    public BlockType block;
    public int hp;
    private int sethp;
    public float speed;
    private bool hitflag;
    private Renderer color,savecolor;
    private Dictionary<int,Block> blocks;
    private Dictionary<int,LinkBlock> linkBlocks;
    private int count;
    public bool damageflag;
    private Player player;
    public bool moveflag;
    private GameObject center;
    private float radius;
    // Start is called before the first frame update
    void Start()
    {
        blocks = new Dictionary<int, Block>();
        linkBlocks = new Dictionary<int, LinkBlock>();
        color =gameObject.transform.GetComponent<Renderer>();
        savecolor = color;
        if(block==BlockType.Nomal)
        {
            sethp = 1;
            if(hp<=sethp)
            {
                hp = sethp;
            }
        }else
        {
            sethp = 2;
            if (hp <= sethp)
            {
                hp = sethp;
            }
        }
        count = 0;
        foreach(Transform child in transform)
        {
            linkBlocks.Add(count,child.gameObject.GetComponent<LinkBlock>()) ;
            blocks.Add(count, gameObject.GetComponent<Block>());
            count++;
        }
        center = GameObject.Find("Center");
        player = GameObject.Find("Player").gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i=0;i<linkBlocks.Count-5;i++)
        {
            if(linkBlocks[i].hitflag)
            {
                blocks[i]=linkBlocks[i].hitblock;
            }
        }
        Damage();
        Move();
    }
    void Move()
    {
        radius = Vector3.Distance(transform.position, center.transform.position);
        if (linkBlocks[0].attackflag)
        {
            if(!linkBlocks[1].hitflag)
            {
                //RotateAround(円運動の中心,進行方向,速度)
                transform.RotateAround(center.transform.position,
                -transform.forward, speed / radius);

            }
            moveflag = true;


        }
        else if(linkBlocks[1].attackflag)
        {
            if (!linkBlocks[0].hitflag)
            {
                //RotateAround(円運動の中心,進行方向,速度)
                transform.RotateAround(center.transform.position,
                transform.forward, speed / radius);
                moveflag = true;
            }
            moveflag = true;

        }
    }

    void Damage()
    {
        for(int i=0;i<3;i++)
        {
            if(blocks[i].damageflag&&!damageflag)
            {
                damageflag = true;
                color.material.color = Color.red;
            }
        }
        if ((block == BlockType.Nomal &&player.type == PlayerType.Fire)
            || (block == BlockType.Fire && player.type == PlayerType.Nomal))
        {
            if (damageflag)
            {
                hp--;
                damageflag = false;
                color.material.color = Color.white;
            }
        }
        if (hp < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {

            //color.material.color = Color.red;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.transform.tag=="Player")
        {

                damageflag = true;
            hitflag = true;
            // color.material.color = Color.white;
        }
    }
}
