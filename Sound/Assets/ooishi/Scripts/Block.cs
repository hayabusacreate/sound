using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BlockType
{
    Nomal,
    Fire
}
public enum InOut
{
    In,
    Out,
}
public class Block : MonoBehaviour
{
    public BlockType block;
    public int hp;
    private int sethp;
    public int speed;
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
    private bool changeflag;
    private MapCreate mapCreate;
    public InOut inout;
    private bool link;
    public int hight, tyle;
    private Vector3 pos;
    private float rad, degree;

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
        center = transform.root.gameObject;
        player = GameObject.Find("Player").gameObject.GetComponent<Player>();
        count = 0;
        mapCreate = GameObject.Find("MapCreate").GetComponent<MapCreate>();
        if(inout==InOut.In)
        {
            mapCreate.intype.Add(hight * 100 + tyle, gameObject.GetComponent<Block>());
        }else
        {
            mapCreate.outtype.Add(hight * 100 + tyle, gameObject.GetComponent<Block>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Damage();

    }
    void Move()
    {
        if (inout == InOut.In)
        {
            if (tyle + 1 < mapCreate.inblock)
            {
                if (mapCreate.inmap[hight * 100 + tyle + 1])
                {
                    if (mapCreate.intype[hight * 100 + tyle + 1].moveflag)
                    {
                        moveflag = true;
                    }
                }
            }
            else
            {
                if (mapCreate.inmap[hight * 100])
                {
                    if (mapCreate.intype[hight * 100].moveflag)
                    {
                        moveflag = true;
                    }
                }
            }
            if (tyle - 1 >= 0)
            {
                if (mapCreate.inmap[hight * 100 + tyle - 1])
                {
                    if (mapCreate.intype[hight * 100 + tyle - 1].moveflag)
                    {
                        moveflag = true;
                    }
                }
            }
            else
            {
                if (mapCreate.inmap[hight * 100 + mapCreate.inblock - 1])
                {
                    if (mapCreate.intype[hight * 100 + mapCreate.inblock - 1].moveflag)
                    {
                        moveflag = true;
                    }
                }
            }
        }
        if (inout == InOut.Out)
        {
            if (tyle + 1 < mapCreate.outblock)
            {
                if (mapCreate.outmap[hight * 100 + tyle + 1])
                {
                    if (mapCreate.outtype[hight * 100 + tyle + 1].moveflag)
                    {
                        moveflag = true;
                    }
                }
            }
            else
            {
                if (mapCreate.outmap[hight * 100])
                {
                    if (mapCreate.outtype[hight * 100].moveflag)
                    {
                        moveflag = true;
                    }
                }
            }
            if (tyle - 1 > 0)
            {
                if (mapCreate.outmap[hight * 100 + tyle - 1])
                {
                    if (mapCreate.outtype[hight * 100 + tyle - 1].moveflag)
                    {
                        moveflag = true;
                    }
                }
            }
            else
            {
                if (mapCreate.outmap[hight * 100 + mapCreate.inblock])
                {
                    if (mapCreate.outtype[hight * 100 + mapCreate.inblock].moveflag)
                    {
                        moveflag = true;
                    }
                }
            }
        }
        pos = new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(center.transform.position.x, 0, center.transform.position.z);
        rad = Mathf.Atan2(pos.x, pos.z);
        degree = transform.rotation.z * Mathf.Rad2Deg;
        if (degree < 0)
        {
            degree += 360;
        }
        radius = Vector3.Distance(transform.position, center.transform.position);
        if (linkBlocks[0].attackflag)
        {
           moveflag = true;
        }else
        if(linkBlocks[1].attackflag)
        {
            moveflag = true;
        }

        if(player.changeflag==0)
        {
            if(tyle==1&&hight==1)
            Debug.Log((int)degree);
            if (moveflag)
            {
                //RotateAround(円運動の中心,進行方向,速度)
                transform.RotateAround(center.transform.position,
                transform.forward, speed);
            }
            if (inout == InOut.In)
            {
                if ((int)(degree / (360 / mapCreate.inblock)) != tyle)
                {
                    tyle = (int)(degree / (360 / mapCreate.inblock));
                    moveflag = false;
                    linkBlocks[0].attackflag = false;
                    player.attackflag = false;
                }
            }
            else
            {
                if ((int)(degree / (360 / mapCreate.outblock)) != tyle)
                {
                    tyle = (int)(degree/ (360 / mapCreate.outblock));
                    moveflag = false;
                    linkBlocks[0].attackflag = false;
                    player.attackflag = false;
                }
            }
        }
        else if (player.changeflag == 1)
        {
            if (moveflag)
            {
                //RotateAround(円運動の中心,進行方向,速度)
                transform.RotateAround(center.transform.position,
                -transform.forward, speed);
            }
            if (inout == InOut.In)
            {
                if ((int)(degree / (360 / mapCreate.inblock)) != tyle)
                {
                    tyle = (int)(degree / (360 / mapCreate.inblock));
                    moveflag = false;
                    linkBlocks[1].attackflag = false;
                    player.attackflag = false;
                }
            }
            else
            {
                if ((int)(degree / (360 / mapCreate.outblock)) != tyle)
                {
                    tyle = (int)(degree / (360 / mapCreate.outblock));
                    moveflag = false;
                    linkBlocks[1].attackflag = false;
                    player.attackflag = false;
                }
            }
        }
    }

    void Damage()
    {
        if(damageflag)
        {
            if (inout == InOut.In)
            {
                if (tyle + 1 < mapCreate.inblock)
                {
                    if (mapCreate.inmap[hight * 100 + tyle + 1])
                    {
                        if (mapCreate.intype[hight * 100 + tyle + 1].block == block)
                        {
                            mapCreate.intype[hight * 100 + tyle + 1].damageflag = true;
                        }
                    }
                }
                else
                {
                    if (mapCreate.inmap[hight * 100])
                    {
                        if (mapCreate.intype[hight * 100].block == block)
                        {
                            mapCreate.intype[hight * 100].damageflag = true;
                        }
                    }
                }
                if (tyle - 1 >= 0)
                {
                    if (mapCreate.inmap[hight * 100 + tyle - 1])
                    {
                        if (mapCreate.intype[hight * 100 + tyle - 1].block == block)
                        {
                            mapCreate.intype[hight * 100 + tyle - 1].damageflag = true;
                        }
                    }
                }
                else
                {
                    if (mapCreate.inmap[hight * 100 + mapCreate.inblock-1])
                    {
                        if (mapCreate.intype[hight * 100 + mapCreate.inblock-1].block == block)
                        {
                            mapCreate.intype[hight * 100 + mapCreate.inblock-1].damageflag = true;
                        }
                    }
                }
            }
            if (inout == InOut.Out)
            {
                if (tyle + 1 < mapCreate.outblock)
                {
                    if (mapCreate.outmap[hight * 100 + tyle + 1])
                    {
                        if (mapCreate.outtype[hight * 100 + tyle + 1].block == block)
                        {
                            mapCreate.outtype[hight * 100 + tyle + 1].damageflag = true;
                        }
                    }
                }
                else
                {
                    if (mapCreate.outmap[hight * 100])
                    {
                        if (mapCreate.outtype[hight * 100].block == block)
                        {
                            mapCreate.outtype[hight * 100].damageflag = true;
                        }
                    }
                }
                if (tyle - 1 > 0)
                {
                    if (mapCreate.outmap[hight * 100 + tyle - 1])
                    {
                        if (mapCreate.outtype[hight * 100 + tyle - 1].block == block)
                        {
                            mapCreate.outtype[hight * 100 + tyle - 1].damageflag = true;
                        }
                    }
                }
                else
                {
                    if (mapCreate.outmap[hight * 100 + mapCreate.inblock])
                    {
                        if (mapCreate.outtype[hight * 100 + mapCreate.inblock].block == block)
                        {
                            mapCreate.outtype[hight * 100 + mapCreate.inblock].damageflag = true;
                        }
                    }
                }
            }
            color.material.color = Color.red;
        }

        if ((block == BlockType.Nomal &&player.type == PlayerType.Fire)
            || (block == BlockType.Fire && player.type == PlayerType.Nomal))
        {
            if (damageflag)
            {
                hp--;
                damageflag = false;
            }
        }
        if (hp < 0)
        {
            if (inout == InOut.In)
            {
                mapCreate.inmap[hight * 100 + tyle] = false;
            }
            else
            {
                mapCreate.outmap[hight * 100 + tyle] = false;
            }
                Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            damageflag = true;
            //color.material.color = Color.red;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.transform.tag=="Player")
        {

            hitflag = true;
            // color.material.color = Color.white;
        }
    }
}
