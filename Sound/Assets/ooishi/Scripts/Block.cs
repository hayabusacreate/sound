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
    public float hp;
    private int sethp;
    public int speed;
    private bool hitflag;
    private Renderer color, savecolor;
    private Dictionary<int, Block> blocks;
    private Dictionary<int, LinkBlock> linkBlocks;
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
    private Quaternion quaternion, savequaternion;
    private float z;
    private bool savetyle;
    private int change;

    public GameObject bubble;
    private float bubbletime;
    // Start is called before the first frame update
    void Start()
    {
        quaternion = this.transform.rotation;
        savequaternion = this.transform.rotation;
        blocks = new Dictionary<int, Block>();
        linkBlocks = new Dictionary<int, LinkBlock>();
        color = gameObject.transform.GetComponent<Renderer>();
        savecolor = color;
        if (block == BlockType.Nomal)
        {
            sethp = 1;
            if (hp <= sethp)
            {
                hp = sethp;
            }
        }
        else
        {
            sethp = 2;
            if (hp <= sethp)
            {
                hp = sethp;
            }
        }
        count = 0;
        foreach (Transform child in transform)
        {
            linkBlocks.Add(count, child.gameObject.GetComponent<LinkBlock>());
            blocks.Add(count, gameObject.GetComponent<Block>());
            count++;
        }
        center = transform.root.gameObject;
        player = GameObject.Find("Player").gameObject.GetComponent<Player>();
        count = 0;
        mapCreate = GameObject.Find("MapCreate").GetComponent<MapCreate>();
        if (inout == InOut.In)
        {
            mapCreate.intype.Add(hight * 100 + tyle, gameObject.GetComponent<Block>());
        }
        else
        {
            mapCreate.outtype.Add(hight * 100 + tyle, gameObject.GetComponent<Block>());
        }

        if(block==BlockType.Nomal)
        {
            color.material.color = Color.green;
        }else
        {
            color.material.color = Color.yellow;
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
        if(change==0)
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
                            change = 2;
                        }
                    }
                }
                else
                {
                    if (mapCreate.inmap[hight * 100])
                    {
                        if (mapCreate.intype[hight * 100 + 1].moveflag)
                        {
                            moveflag = true;
                            change = 2;
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
                            change = 1;
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
                            change = 1;
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
                            change = 2;
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
                            change = 2;
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
                            change = 1;
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
                            change = 1;
                        }
                    }
                }
            }
        }
 
        //pos = new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(center.transform.position.x, 0, center.transform.position.z);
        //rad = Mathf.Atan2(pos.x, pos.z);
        //degree = transform.rotation.z * Mathf.Rad2Deg;
        //if (degree < 0)
        //{
        //    degree += 360;
        //}
        //radius = Vector3.Distance(transform.position, center.transform.position);
        quaternion = this.transform.rotation;

        if (change == 1)
        {
            z = quaternion.eulerAngles.y % 360 - ((int)((360 / mapCreate.inblock)) / 2);
        }
        if (change == 2)
        {
            z = quaternion.eulerAngles.y % 360 + ((int)((360 / mapCreate.inblock)) / 2);
        }
        if (moveflag)
        {
            mapCreate.inmap[hight * 100 + tyle] = false;
            if (inout == InOut.In)
            {
                if ((int)(z / (360 / mapCreate.inblock)) != tyle && count > 15)
                {

                    tyle = (int)(z / (360 / (mapCreate.inblock)))-1;
                    linkBlocks[0].attackflag = false;
                    linkBlocks[1].attackflag = false;
                    moveflag = false;
                    player.attackflag = false;
                    change = 0;
                    count = 0;
                    mapCreate.inmap[hight * 100 + tyle] = true;
                }
                //if(tyle+1<mapCreate.inblock)
                //{
                //    if (mapCreate.inmap[hight * 100 + tyle + 1] && linkBlocks[0].attackflag)
                //    {
                //        linkBlocks[0].attackflag = false;
                //        linkBlocks[1].attackflag = false;
                //        moveflag = false;
                //        player.attackflag = false;
                //        change = 0;
                //        count = 0;
                //        mapCreate.inmap[hight * 100 + tyle] = true;
                //    }
                //}else
                //{
                //    if (mapCreate.inmap[hight * 100] && linkBlocks[0].attackflag)
                //    {
                //        linkBlocks[0].attackflag = false;
                //        linkBlocks[1].attackflag = false;
                //        moveflag = false;
                //        player.attackflag = false;
                //        change = 0;
                //        count = 0;
                //        mapCreate.inmap[hight * 100 + tyle] = true;
                //    }
                //}
                //if(tyle-1>0)
                //{
                //    if (mapCreate.inmap[hight * 100 + tyle - 1] && linkBlocks[1].attackflag)
                //    {
                //        linkBlocks[0].attackflag = false;
                //        linkBlocks[1].attackflag = false;
                //        moveflag = false;
                //        player.attackflag = false;
                //        change = 0;
                //        count = 0;
                //        mapCreate.inmap[hight * 100 + tyle] = true;
                //    }
                //}else
                //{
                //    if (mapCreate.inmap[hight * 100 + mapCreate.inblock-1] && linkBlocks[1].attackflag)
                //    {
                //        linkBlocks[0].attackflag = false;
                //        linkBlocks[1].attackflag = false;
                //        moveflag = false;
                //        player.attackflag = false;
                //        change = 0;
                //        count = 0;
                //        mapCreate.inmap[hight * 100 + tyle] = true;
                //    }
                //}

            }
            else
            {
                if ((int)(z / (360 / (mapCreate.outblock))) != tyle
                    && count > 15
                    )
                {
                    tyle = (int)(z / (360 / mapCreate.outblock))-1;
                    linkBlocks[0].attackflag = false;
                    linkBlocks[1].attackflag = false;
                    moveflag = false;
                    player.attackflag = false;
                    change = 0;
                    count = 0;
                    mapCreate.inmap[hight * 100 + tyle] = true;
                }
                //if (tyle + 1 < mapCreate.outblock)
                //{
                //    if (mapCreate.outmap[hight * 100 + tyle + 1] && linkBlocks[0].attackflag)
                //    {
                //        linkBlocks[0].attackflag = false;
                //        linkBlocks[1].attackflag = false;
                //        moveflag = false;
                //        player.attackflag = false;
                //        change = 0;
                //        count = 0;
                //        mapCreate.outmap[hight * 100 + tyle] = true;
                //    }
                //}
                //else
                //{
                //    if (mapCreate.outmap[hight * 100] && linkBlocks[0].attackflag)
                //    {
                //        linkBlocks[0].attackflag = false;
                //        linkBlocks[1].attackflag = false;
                //        moveflag = false;
                //        player.attackflag = false;
                //        change = 0;
                //        count = 0;
                //        mapCreate.outmap[hight * 100 + tyle] = true;
                //    }
                //}
                //if (tyle - 1 > 0)
                //{
                //    if (mapCreate.outmap[hight * 100 + tyle - 1] && linkBlocks[1].attackflag)
                //    {
                //        linkBlocks[0].attackflag = false;
                //        linkBlocks[1].attackflag = false;
                //        moveflag = false;
                //        player.attackflag = false;
                //        change = 0;
                //        count = 0;
                //        mapCreate.outmap[hight * 100 + tyle] = true;
                //    }
                //}
                //else
                //{
                //    if (mapCreate.outmap[hight * 100 + mapCreate.outblock - 1] && linkBlocks[1].attackflag)
                //    {
                //        linkBlocks[0].attackflag = false;
                //        linkBlocks[1].attackflag = false;
                //        moveflag = false;
                //        player.attackflag = false;
                //        change = 0;
                //        count = 0;
                //        mapCreate.outmap[hight * 100 + tyle] = true;
                //    }
                //}
            }
            count++;
            if (change == 1)
            {
                //RotateAround(円運動の中心,進行方向,速度)
                transform.RotateAround(center.transform.position,
                transform.forward, speed);
            }
            else if (change == 2)
            {
                //RotateAround(円運動の中心,進行方向,速度)
                transform.RotateAround(center.transform.position,
                -transform.forward, speed);

            }
        }
        else
        {
            if (linkBlocks[0].attackflag)
            {
                moveflag = true;
                change = 1;
            }
            else
if (linkBlocks[1].attackflag)
            {
                moveflag = true;
                change = 2;
            }
        }
        if(linkBlocks[3].hitblock.hight!=hight)
        {
            hight = linkBlocks[3].hitblock.hight;
        }
        if (linkBlocks[2].hitblock.hight != hight)
        {
            hight = linkBlocks[2].hitblock.hight;
        }


    }

    void Damage()
    {
        if (damageflag)
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
                    if (mapCreate.inmap[hight * 100 + mapCreate.inblock - 1])
                    {
                        if (mapCreate.intype[hight * 100 + mapCreate.inblock - 1].block == block)
                        {
                            mapCreate.intype[hight * 100 + mapCreate.inblock - 1].damageflag = true;
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
            //color.material.color = Color.red;
        }else
        {
            if (inout == InOut.In)
            {
                if(mapCreate.outtype[hight*100+ (int)(z / (360 / mapCreate.outblock))].damageflag)
                {
                    damageflag = true;
                }
            }
            if (inout == InOut.Out)
            {
                if (mapCreate.intype[hight * 100 + (int)(z / (360 / mapCreate.inblock))].damageflag)
                {
                    damageflag = true;
                }
            }
        }

        if ((block == BlockType.Nomal && player.type == PlayerType.Fire)
            || (block == BlockType.Fire && player.type == PlayerType.Nomal))
        {
            if (damageflag)
            {
                damageflag = false;
            }
        }
        else
        {
            if (damageflag)
            {
                hp -= Time.deltaTime;
                bubbletime += Time.deltaTime;

            }
            if (bubbletime > 1)
            {
                GameObject gameObject = Instantiate(bubble,new Vector3(transform.position.x,transform.position.y+5,transform.position.z), Quaternion.identity);
                if(inout==InOut.In)
                {
                    gameObject.transform.rotation = Quaternion.Euler(-90, 0, (360 / mapCreate.inblock) * (tyle+3));
                }else
                {
                    gameObject.transform.rotation = Quaternion.Euler(-90, 0, (360 / mapCreate.outblock) * (tyle+3));
                }

                bubbletime = 0;
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
        if (collision.transform.tag == "Player")
        {

            hitflag = true;
            // color.material.color = Color.white;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Bubble")
        {
            hp--;
        }
    }
}
