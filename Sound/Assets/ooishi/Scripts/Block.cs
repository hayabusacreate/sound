using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BlockType
{
    Nomal,
    Fire,
    Fish,
    Etc,
}
public enum InOut
{
    In,
    Half,
    Out,
}
public class Block : MonoBehaviour
{
    public BlockType block;
    public float hp;
    public float Maxhp;
    private float sethp;
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

    public bool bubbleflag;
    public GameObject kati;
    public ParticleSystem syuwa;

    private bool moveendflag;

    public bool fallflag, noneflag, rightflag, leftflag;
    private int savehight;
    public Rigidbody rigidbody;

    private Manager manager;
    //private Material mat;

    public AudioClip fall;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        Maxhp = hp;
        //mat.color = new Color(mat.color.r, mat.color.g, mat.color.b,0);
        manager = GameObject.Find("Manager").gameObject.GetComponent<Manager>();
        quaternion = this.transform.rotation;
        savequaternion = this.transform.rotation;
        blocks = new Dictionary<int, Block>();
        linkBlocks = new Dictionary<int, LinkBlock>();
        color = gameObject.transform.GetComponent<Renderer>();
        savecolor = color;
        if (block == BlockType.Nomal)
        {
            sethp = manager.nomalHp;
            if (hp <= sethp)
            {
                hp = sethp;
            }
        }
        if (block == BlockType.Fire)
        {
            sethp = manager.FireHp;
            if (hp <= sethp)
            {
                hp = sethp;
            }
        }
        if (block == BlockType.Fish)
        {
            sethp = manager.FishHp;
            if (hp <= sethp)
            {
                hp = sethp;
            }
        }
        if (block == BlockType.Etc)
        {
            sethp = manager.EtcHp;
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

        quaternion = this.transform.rotation;
        hight = transform.root.gameObject.GetComponent<Map>().hight;
        savehight = hight;
        rigidbody = gameObject.GetComponent<Rigidbody>();
        z = Mathf.Round(quaternion.eulerAngles.y);

        if (inout == InOut.In)
        {
            tyle = (int)(z / (360 / mapCreate.inblock));
            linkBlocks[0].attackflag = false;
            linkBlocks[1].attackflag = false;
            moveflag = false;
            player.attackflag = false;
            change = 0;
            count = 0;
            mapCreate.inmap[hight * 100 + tyle] = true;
        }
        else if(inout == InOut.Half)
        {
            tyle = (int)(z / (360 / mapCreate.halfblock));
            linkBlocks[0].attackflag = false;
            linkBlocks[1].attackflag = false;
            moveflag = false;
            player.attackflag = false;
            change = 0;
            count = 0;
            mapCreate.halfmap[hight * 100 + tyle] = true;
        }
        else if (inout == InOut.Out)
        {
            tyle = (int)(z / (360 / mapCreate.outblock));
            linkBlocks[0].attackflag = false;
            linkBlocks[1].attackflag = false;
            moveflag = false;
            player.attackflag = false;
            change = 0;
            count = 0;
            mapCreate.outmap[hight * 100 + tyle] = true;
        }
        if (inout == InOut.In)
        {
            mapCreate.intype.Add(hight * 100 + tyle, gameObject.GetComponent<Block>());
        }
        else if(inout == InOut.Out)
        {
            mapCreate.outtype.Add(hight * 100 + tyle, gameObject.GetComponent<Block>());
        }
        else if (inout == InOut.Half)
        {
            mapCreate.halftype.Add(hight * 100 + tyle, gameObject.GetComponent<Block>());
        }

        if (block == BlockType.Nomal)
        {
            color.material.color = Color.green;
        }
        else
        {
            color.material.color = Color.yellow;
        }
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Damage();

    }
    void Move()
    {

        if (inout==InOut.In)
        {
            mapCreate.intype[hight * 100 + (int)(z / (360 / mapCreate.inblock))] = gameObject.transform.GetComponent<Block>();
        }
        else if(inout == InOut.Out)
        {
            mapCreate.outtype[hight * 100 + (int)(z / (360 / mapCreate.outblock))] = gameObject.transform.GetComponent<Block>();
        }
        else if (inout == InOut.Half)
        {
            mapCreate.halftype[hight * 100 + (int)(z / (360 / mapCreate.halfblock))] = gameObject.transform.GetComponent<Block>();
        }
        if (inout == InOut.In)
        {
            if (mapCreate.maps.Length - 1 > hight)
            {
                if (!mapCreate.inmap[(hight + 1) * 100 + tyle])
                {
                    noneflag = true;
                }
                else if (mapCreate.inmap[(hight + 1) * 100 + tyle])
                {
                    noneflag = false;
                }
            }

            if (noneflag)
            {
                if (tyle == 0)
                {
                    if (mapCreate.inmap[hight * 100 + mapCreate.inblock - 1])
                    {
                        if (mapCreate.intype[hight * 100 + mapCreate.inblock - 1].block != block)
                        {
                            rightflag = true;
                        }
                        else
                        {
                            if (mapCreate.intype[hight * 100 + mapCreate.inblock - 1].rightflag)
                            {
                                rightflag = true;
                            }
                            else
                            {
                                rightflag = false;
                            }
                        }
                    }
                    else if (!mapCreate.inmap[hight * 100 + mapCreate.inblock - 1])
                    {
                        rightflag = true;
                    }

                }
                if (tyle != mapCreate.inblock - 1)
                {
                    if (mapCreate.inmap[hight * 100 + tyle + 1])
                    {
                        if (mapCreate.intype[hight * 100 + tyle + 1].block != block)
                        {
                            leftflag = true;
                        }
                        else
                        {
                            if (mapCreate.intype[hight * 100 + tyle + 1].leftflag)
                            {
                                leftflag = true;
                            }
                            else
                            {
                                leftflag = false;
                            }
                        }
                    }
                    else if (!mapCreate.inmap[hight * 100 + tyle + 1])
                    {
                        leftflag = true;
                    }

                }
                if (tyle == mapCreate.inblock - 1)
                {
                    if (mapCreate.inmap[hight * 100])
                    {
                        if (mapCreate.intype[hight * 100].block != block)
                        {
                            leftflag = true;
                        }
                        else
                        {
                            if (mapCreate.intype[hight * 100].leftflag)
                            {
                                leftflag = true;
                            }
                            else
                            {
                                leftflag = false;
                            }
                        }
                    }
                    else if (!mapCreate.inmap[hight * 100])
                    {
                        leftflag = true;
                    }
                }
                if (tyle != 0)
                {
                    if (mapCreate.inmap[hight * 100 + tyle - 1])
                    {
                        if (mapCreate.intype[hight * 100 + tyle - 1].block != block)
                        {
                            rightflag = true;
                        }
                        else
                        {
                            if (mapCreate.intype[hight * 100 + tyle - 1].rightflag)
                            {
                                rightflag = true;
                            }
                            else
                            {
                                rightflag = false;
                            }
                        }
                    }
                    else if (!mapCreate.inmap[hight * 100])
                    {
                       rightflag= true;
                    }
                }
            }
            if (rightflag && leftflag)
            {
                source.PlayOneShot(fall);
                fallflag = true;
                rigidbody.isKinematic = false;
            }
        }
        else
        if (inout == InOut.Out)
        {
            if (mapCreate.maps.Length - 1 > hight)
            {
                if (!mapCreate.outmap[(hight + 1) * 100 + tyle])
                {
                    noneflag = true;
                }
                else if (mapCreate.outmap[(hight + 1) * 100 + tyle])
                {
                    noneflag = false;
                }
            }

            if (noneflag)
            {
                if (tyle == 0)
                {
                    if (mapCreate.outmap[hight * 100 + mapCreate.outblock - 1])
                    {
                        if (mapCreate.outtype[hight * 100 + mapCreate.outblock - 1].block != block)
                        {
                            rightflag = true;
                        }
                        else
                        {
                            if (mapCreate.outtype[hight * 100 + mapCreate.outblock - 1].rightflag)
                            {
                                rightflag = true;
                            }
                            else
                            {
                                rightflag = false;
                            }
                        }
                    }
                    else if (!mapCreate.outmap[hight * 100 + mapCreate.outblock - 1])
                    {
                        rightflag = true;
                    }

                }
                if (tyle != mapCreate.outblock - 1)
                {
                    if (mapCreate.outmap[hight * 100 + tyle + 1])
                    {
                        if (mapCreate.outtype[hight * 100 + tyle + 1].block != block)
                        {
                            leftflag = true;
                        }
                        else
                        {
                            if (mapCreate.outtype[hight * 100 + tyle + 1].leftflag)
                            {
                                leftflag = true;
                            }
                            else
                            {
                                leftflag = false;
                            }
                        }
                    }
                    else if (!mapCreate.outmap[hight * 100 + tyle + 1])
                    {
                        leftflag = true;
                    }

                }
                if (tyle == mapCreate.outblock - 1)
                {
                    if (mapCreate.outmap[hight * 100])
                    {
                        if (mapCreate.outtype[hight * 100].block != block)
                        {
                            rightflag = true;
                        }
                        else
                        {
                            if (mapCreate.outtype[hight * 100].rightflag)
                            {
                                rightflag = true;
                            }
                            else
                            {
                                rightflag = false;
                            }
                        }
                    }
                    else if (!mapCreate.outmap[hight * 100])
                    {
                        rightflag = true;
                    }
                }
                if (tyle != 0)
                {
                    if (mapCreate.outmap[hight * 100 + tyle - 1])
                    {
                        if (mapCreate.outtype[hight * 100 + tyle - 1].block != block)
                        {
                            leftflag = true;
                        }
                        else
                        {
                            if (mapCreate.outtype[hight * 100 + tyle - 1].leftflag)
                            {
                                leftflag = true;
                            }
                            else
                            {
                                leftflag = false;
                            }
                        }
                    }
                    else if (!mapCreate.outmap[hight * 100])
                    {
                        leftflag = true;
                    }
                }
            }

            if (rightflag && leftflag)
            {
                source.PlayOneShot(fall);
                fallflag = true;
                rigidbody.isKinematic = false;
            }
        }
        else
        if (inout == InOut.Half)
        {
            if (mapCreate.maps.Length - 1 > hight)
            {
                if (!mapCreate.halfmap[(hight + 1) * 100 + tyle])
                {
                    noneflag = true;
                }
                else if (mapCreate.halfmap[(hight + 1) * 100 + tyle])
                {
                    noneflag = false;
                }
            }

            if (noneflag)
            {
                if (tyle == 0)
                {
                    if (mapCreate.halfmap[hight * 100 + mapCreate.halfblock - 1])
                    {
                        if (mapCreate.halftype[hight * 100 + mapCreate.halfblock - 1].block != block)
                        {
                            rightflag = true;
                        }
                        else
                        {
                            if (mapCreate.halftype[hight * 100 + mapCreate.halfblock - 1].rightflag)
                            {
                                rightflag = true;
                            }
                            else
                            {
                                rightflag = false;
                            }
                        }
                    }
                    else if (!mapCreate.halfmap[hight * 100 + mapCreate.halfblock - 1])
                    {
                        rightflag = true;
                    }

                }
                if (tyle != mapCreate.halfblock - 1)
                {
                    if (mapCreate.halfmap[hight * 100 + tyle + 1])
                    {
                        if (mapCreate.halftype[hight * 100 + tyle + 1].block != block)
                        {
                            leftflag = true;
                        }
                        else
                        {
                            if (mapCreate.halftype[hight * 100 + tyle + 1].leftflag)
                            {
                                leftflag = true;
                            }
                            else
                            {
                                leftflag = false;
                            }
                        }
                    }
                    else if (!mapCreate.halfmap[hight * 100 + tyle + 1])
                    {
                        leftflag = true;
                    }

                }
                if (tyle == mapCreate.halfblock - 1)
                {
                    if (mapCreate.halfmap[hight * 100])
                    {
                        if (mapCreate.halftype[hight * 100].block != block)
                        {
                            rightflag = true;
                        }
                        else
                        {
                            if (mapCreate.halftype[hight * 100].rightflag)
                            {
                                rightflag = true;
                            }
                            else
                            {
                                rightflag = false;
                            }
                        }
                    }
                    else if (!mapCreate.halfmap[hight * 100])
                    {
                        rightflag = true;
                    }
                }
                if (tyle != 0)
                {
                    if (mapCreate.halfmap[hight * 100 + tyle - 1])
                    {
                        if (mapCreate.halftype[hight * 100 + tyle - 1].block != block)
                        {
                            leftflag = true;
                        }
                        else
                        {
                            if (mapCreate.halftype[hight * 100 + tyle - 1].leftflag)
                            {
                                leftflag = true;
                            }
                            else
                            {
                                leftflag = false;
                            }
                        }
                    }
                    else if (!mapCreate.halfmap[hight * 100])
                    {
                        leftflag = true;
                    }
                }
            }

            if (rightflag && leftflag)
            {
                source.PlayOneShot(fall);
                fallflag = true;
                rigidbody.isKinematic = false;
            }
        }

        if (fallflag)
        {

            if (hight != savehight)
            {
                fallflag = false;
                leftflag = false;
                rightflag = false;
                savehight = hight;
                rigidbody.isKinematic = true;
            }
        }
        else
        {
        }
        //Debug.Log(inout + "" + tyle + "" + block);
        if (change == 0)
        {
            if (inout == InOut.In)
            {
                if (tyle + 1 <= mapCreate.inblock - 1)
                {
                    if (mapCreate.inmap[hight * 100 + tyle + 1]
                        && mapCreate.intype[hight * 100 + tyle + 1].moveflag)
                    {
                        if(mapCreate.intype[hight * 100 + tyle + 1].change==1)
                        {
                            change = 1;
                        }
                        else
                        {
                            change = 2;
                        }
                        moveflag = true;

                    }
                }
                if (tyle + 1 == mapCreate.inblock)
                {
                    if (mapCreate.inmap[hight * 100]
                        && mapCreate.intype[hight * 100 + 1].moveflag)
                    {
                        if (mapCreate.intype[hight * 100  + 1].change == 1)
                        {
                            change = 1;
                        }
                        else
                        {
                            change = 2;
                        }
                        moveflag = true;
                        //change = 1;
                    }
                }
                if (tyle - 1 >= 0)
                {
                    if (mapCreate.inmap[hight * 100 + tyle - 1]
                        && mapCreate.intype[hight * 100 + tyle - 1].moveflag)
                    {
                        if (mapCreate.intype[hight * 100 + tyle - 1].change == 1)
                        {
                            change = 1;
                        }
                        else
                        {
                            change = 2;
                        }
                        moveflag = true;
                        //change = 2;
                    }
                }
                if (tyle - 1 == -1)
                {
                    if (mapCreate.inmap[hight * 100 + mapCreate.inblock - 1]
                        && mapCreate.intype[hight * 100 + mapCreate.inblock - 1].moveflag)
                    {
                        if (mapCreate.intype[hight * 100 + tyle - 1].change == 1)
                        {
                            change = 1;
                        }
                        else
                        {
                            change = 2;
                        }
                        moveflag = true;
                        //change = 2;
                    }
                }
            }
            if (inout == InOut.Out)
            {
                if (tyle + 1 <= mapCreate.outblock - 1)
                {
                    if (mapCreate.outmap[hight * 100 + tyle + 1]
                        && mapCreate.outtype[hight * 100 + tyle + 1].moveflag)
                    {
                                            
                        if(mapCreate.outtype[hight * 100 + tyle + 1].change==1)
                        {
                            change = 1;
                        }
                        else
                        {
                            change = 2;
                        }
                        moveflag = true;
                        //change = 1;
                    }
                }
                if (tyle + 1 == mapCreate.outblock)
                {
                    if (mapCreate.outmap[hight * 100+1]
                        && mapCreate.outtype[hight * 100+1].moveflag)
                    {
                        if (mapCreate.outtype[hight * 100  + 1].change == 1)
                        {
                            change = 1;
                        }
                        else
                        {
                            change = 2;
                        }
                        moveflag = true;
                        //change = 1;
                    }
                }
                if (tyle - 1 >= 0)
                {
                    if (mapCreate.outmap[hight * 100 + tyle - 1]
                        && mapCreate.outtype[hight * 100 + tyle - 1].moveflag)
                    {
                        if (mapCreate.outtype[hight * 100 + tyle - 1].change == 1)
                        {
                            change = 1;
                        }
                        else
                        {
                            change = 2;
                        }
                        moveflag = true;
                        //change = 2;
                    }
                }
                if (tyle - 1 == -1)
                {
                    if (mapCreate.outmap[hight * 100 + mapCreate.outblock - 1]
                        && mapCreate.outtype[hight * 100 + mapCreate.outblock - 1].moveflag)
                    {
                        if (mapCreate.outtype[hight * 100 + mapCreate.outblock - 1].change == 1)
                        {
                            change = 1;
                        }
                        else
                        {
                            change = 2;
                        }
                        moveflag = true;
                        //change = 2;
                    }
                }
            }
            if (inout == InOut.Half)
            {
                if (tyle + 1 <= mapCreate.halfblock - 1)
                {
                    if (mapCreate.halfmap[hight * 100 + tyle + 1]
                        && mapCreate.halftype[hight * 100 + tyle + 1].moveflag)
                    {

                        if (mapCreate.halftype[hight * 100 + tyle + 1].change == 1)
                        {
                            change = 1;
                        }
                        else
                        {
                            change = 2;
                        }
                        moveflag = true;
                        //change = 1;
                    }
                }
                if (tyle + 1 == mapCreate.halfblock)
                {
                    if (mapCreate.halfmap[hight * 100 + 1]
                        && mapCreate.halftype[hight * 100 + 1].moveflag)
                    {
                        if (mapCreate.halftype[hight * 100 + 1].change == 1)
                        {
                            change = 1;
                        }
                        else
                        {
                            change = 2;
                        }
                        moveflag = true;
                        //change = 1;
                    }
                }
                if (tyle - 1 >= 0)
                {
                    if (mapCreate.halfmap[hight * 100 + tyle - 1]
                        && mapCreate.halftype[hight * 100 + tyle - 1].moveflag)
                    {
                        if (mapCreate.halftype[hight * 100 + tyle - 1].change == 1)
                        {
                            change = 1;
                        }
                        else
                        {
                            change = 2;
                        }
                        moveflag = true;
                        //change = 2;
                    }
                }
                if (tyle - 1 == -1)
                {
                    if (mapCreate.halfmap[hight * 100 + mapCreate.halfblock - 1]
                        && mapCreate.halftype[hight * 100 + mapCreate.halfblock - 1].moveflag)
                    {
                        if (mapCreate.halftype[hight * 100 + mapCreate.halfblock - 1].change == 1)
                        {
                            change = 1;
                        }
                        else
                        {
                            change = 2;
                        }
                        moveflag = true;
                        //change = 2;
                    }
                }
            }
        }
        quaternion = this.transform.rotation;
        z = Mathf.Round(quaternion.eulerAngles.y);

        if (inout == InOut.In)
        {
            if ((int)(z / (360 / mapCreate.inblock)) > tyle)
            {
                if (tyle == 0)
                {
                    //動作環境によって変わる可能性あり今後修正するべし
                    if ((int)(z / (360 / mapCreate.inblock)) == 1)
                    {
                        if (moveendflag)
                        {
                            mapCreate.inmap[hight * 100 + tyle] = false;
                            moveendflag = false;
                        }
                        tyle = (int)(z / (360 / mapCreate.inblock));
                        linkBlocks[0].attackflag = false;
                        linkBlocks[1].attackflag = false;
                        moveflag = false;
                        player.attackflag = false;
                        change = 0;
                        count = 0;
                        mapCreate.inmap[hight * 100 + tyle] = true;
                        mapCreate.intype[hight * 100 + tyle] = gameObject.transform.GetComponent<Block>();
                        transform.rotation = Quaternion.Euler(0, tyle * (360 / mapCreate.inblock), 0);
                    }
                    if ((int)((z- 5) / (360 / mapCreate.inblock)) == mapCreate.inblock - 2)
                    {
                        if (moveendflag)
                        {
                            mapCreate.inmap[hight * 100 + tyle] = false;
                            moveendflag = false;
                        }
                        tyle = 4;
                        linkBlocks[0].attackflag = false;
                        linkBlocks[1].attackflag = false;
                        moveflag = false;
                        player.attackflag = false;
                        change = 0;
                        count = 0;
                        mapCreate.inmap[hight * 100 + tyle] = true;
                        mapCreate.intype[hight * 100 + tyle] = gameObject.transform.GetComponent<Block>();
                        transform.rotation = Quaternion.Euler(0, tyle * (360 / mapCreate.inblock), 0);
                    }

                }
                else
                {
                    if (moveendflag)
                    {
                        mapCreate.inmap[hight * 100 + tyle] = false;
                        moveendflag = false;
                    }
                    tyle = (int)(z / (360 / mapCreate.inblock));
                    linkBlocks[0].attackflag = false;
                    linkBlocks[1].attackflag = false;
                    moveflag = false;
                    player.attackflag = false;
                    change = 0;
                    count = 0;
                    mapCreate.inmap[hight * 100 + tyle] = true;
                    mapCreate.intype[hight * 100 + tyle] = gameObject.transform.GetComponent<Block>();
                    transform.rotation = Quaternion.Euler(0, tyle * (360 / mapCreate.inblock), 0);
                }

            }else
            if ((int)(z / (360 / mapCreate.inblock)) < tyle)
            {
                if ((int)((z) / (360 / mapCreate.inblock)) == 0 && tyle == 4)
                {
                    if (moveendflag)
                    {
                        mapCreate.inmap[hight * 100 + tyle] = false;
                        moveendflag = false;
                    }
                    tyle = (int)(z / (360 / mapCreate.inblock));
                    linkBlocks[0].attackflag = false;
                    linkBlocks[1].attackflag = false;
                    moveflag = false;
                    player.attackflag = false;
                    change = 0;
                    count = 0;
                    mapCreate.inmap[hight * 100 + tyle] = true;
                    mapCreate.intype[hight * 100 + tyle] = gameObject.transform.GetComponent<Block>();
                    transform.rotation = Quaternion.Euler(0, tyle * (360 / mapCreate.inblock), 0);
                }
                else if ((int)((z-5) / (360 / mapCreate.inblock)) == tyle - 2)
                {
                    if (moveendflag)
                    {
                        mapCreate.inmap[hight * 100 + tyle] = false;
                        moveendflag = false;
                    }
                    tyle = (int)(z / (360 / mapCreate.inblock));
                    linkBlocks[0].attackflag = false;
                    linkBlocks[1].attackflag = false;
                    moveflag = false;
                    player.attackflag = false;
                    change = 0;
                    count = 0;
                    mapCreate.inmap[hight * 100 + tyle] = true;
                    mapCreate.intype[hight * 100 + tyle] = gameObject.transform.GetComponent<Block>();
                    transform.rotation = Quaternion.Euler(0, tyle * (360 / mapCreate.inblock), 0);
                }
                if(tyle==1&&z-speed<=0)
                {
                    if (moveendflag)
                    {
                        mapCreate.inmap[hight * 100 + tyle] = false;
                        moveendflag = false;
                    }
                    tyle = (int)(z / (360 / mapCreate.inblock));
                    linkBlocks[0].attackflag = false;
                    linkBlocks[1].attackflag = false;
                    moveflag = false;
                    player.attackflag = false;
                    change = 0;
                    count = 0;
                    mapCreate.inmap[hight * 100 + tyle] = true;
                    mapCreate.intype[hight * 100 + tyle] = gameObject.transform.GetComponent<Block>();
                    transform.rotation = Quaternion.Euler(0, tyle * (360 / mapCreate.inblock), 0);
                }
            }
        }
        else if(inout==InOut.Out)
        {
            if ((int)(z / (360 / mapCreate.outblock)) > tyle)
            {
                if (tyle == 0)
                {
                    //動作環境によって変わる可能性あり今後修正するべし
                    if ((int)(z / (360 / mapCreate.outblock)) == 1)
                    {
                        if (moveendflag)
                        {
                            mapCreate.outmap[hight * 100 + tyle] = false;
                            moveendflag = false;
                        }
                        tyle = (int)(z / (360 / mapCreate.outblock));
                        linkBlocks[0].attackflag = false;
                        linkBlocks[1].attackflag = false;
                        moveflag = false;
                        player.attackflag = false;
                        change = 0;
                        count = 0;
                        mapCreate.outmap[hight * 100 + tyle] = true;
                        mapCreate.outtype[hight * 100 + tyle] = gameObject.transform.GetComponent<Block>();
                    }
                    if ((int)((z - 5) / (360 / mapCreate.outblock)) == mapCreate.outblock - 2)
                    {
                        if (moveendflag)
                        {
                            mapCreate.outmap[hight * 100 + tyle] = false;
                            moveendflag = false;
                        }
                        tyle = (int)(z / (360 / mapCreate.outblock));
                        linkBlocks[0].attackflag = false;
                        linkBlocks[1].attackflag = false;
                        moveflag = false;
                        player.attackflag = false;
                        change = 0;
                        count = 0;
                        mapCreate.outmap[hight * 100 + tyle] = true;
                        mapCreate.outtype[hight * 100 + tyle] = gameObject.transform.GetComponent<Block>();
                    }

                }
                else
                {
                    if (moveendflag)
                    {
                        mapCreate.outmap[hight * 100 + tyle] = false;
                        moveendflag = false;
                    }
                    tyle = (int)(z / (360 / mapCreate.outblock));
                    linkBlocks[0].attackflag = false;
                    linkBlocks[1].attackflag = false;
                    moveflag = false;
                    player.attackflag = false;
                    change = 0;
                    count = 0;
                    mapCreate.outmap[hight * 100 + tyle] = true;
                    mapCreate.outtype[hight * 100 + tyle] = gameObject.transform.GetComponent<Block>();
                }

            }else
            if ((int)(z / (360 / mapCreate.outblock)) < tyle)
            {
                if ((int)((z) / (360 / mapCreate.outblock)) == 0 && tyle == 4)
                {
                    if (moveendflag)
                    {
                        mapCreate.outmap[hight * 100 + tyle] = false;
                        moveendflag = false;
                    }
                    tyle = (int)(z / (360 / mapCreate.outblock));
                    linkBlocks[0].attackflag = false;
                    linkBlocks[1].attackflag = false;
                    moveflag = false;
                    player.attackflag = false;
                    change = 0;
                    count = 0;
                    mapCreate.outmap[hight * 100 + tyle] = true;
                    mapCreate.outtype[hight * 100 + tyle] = gameObject.transform.GetComponent<Block>();
                }
                else if ((int)((z - 5) / (360 / mapCreate.outblock)) == tyle - 1 && tyle != 1)
                {
                    if (moveendflag)
                    {
                        mapCreate.outmap[hight * 100 + tyle] = false;
                        moveendflag = false;
                    }
                    tyle = (int)(z / (360 / mapCreate.outblock));
                    linkBlocks[0].attackflag = false;
                    linkBlocks[1].attackflag = false;
                    moveflag = false;
                    player.attackflag = false;
                    change = 0;
                    count = 0;
                    mapCreate.outmap[hight * 100 + tyle] = true;
                    mapCreate.outtype[hight * 100 + tyle] = gameObject.transform.GetComponent<Block>();
                }
                if (tyle == 1 && z - speed <= 0)
                {
                    if (moveendflag)
                    {
                        mapCreate.outmap[hight * 100 + tyle] = false;
                        moveendflag = false;
                    }
                    tyle = (int)(z / (360 / mapCreate.outblock));
                    linkBlocks[0].attackflag = false;
                    linkBlocks[1].attackflag = false;
                    moveflag = false;
                    player.attackflag = false;
                    change = 0;
                    count = 0;
                    mapCreate.outmap[hight * 100 + tyle] = true;
                    mapCreate.outtype[hight * 100 + tyle] = gameObject.transform.GetComponent<Block>();
                }
            }
        }
        else if (inout == InOut.Half)
        {
            if ((int)(z / (360 / mapCreate.halfblock)) > tyle)
            {
                if (tyle == 0)
                {
                    //動作環境によって変わる可能性あり今後修正するべし
                    if ((int)(z / (360 / mapCreate.halfblock)) == 1)
                    {
                        if (moveendflag)
                        {
                            mapCreate.halfmap[hight * 100 + tyle] = false;
                            moveendflag = false;
                        }
                        tyle = (int)(z / (360 / mapCreate.halfblock));
                        linkBlocks[0].attackflag = false;
                        linkBlocks[1].attackflag = false;
                        moveflag = false;
                        player.attackflag = false;
                        change = 0;
                        count = 0;
                        mapCreate.halfmap[hight * 100 + tyle] = true;
                        mapCreate.halftype[hight * 100 + tyle] = gameObject.transform.GetComponent<Block>();
                    }
                    if ((int)((z - 5) / (360 / mapCreate.halfblock)) == mapCreate.halfblock - 2)
                    {
                        if (moveendflag)
                        {
                            mapCreate.halfmap[hight * 100 + tyle] = false;
                            moveendflag = false;
                        }
                        tyle = (int)(z / (360 / mapCreate.halfblock));
                        linkBlocks[0].attackflag = false;
                        linkBlocks[1].attackflag = false;
                        moveflag = false;
                        player.attackflag = false;
                        change = 0;
                        count = 0;
                        mapCreate.halfmap[hight * 100 + tyle] = true;
                        mapCreate.halftype[hight * 100 + tyle] = gameObject.transform.GetComponent<Block>();
                    }

                }
                else
                {
                    if (moveendflag)
                    {
                        mapCreate.halfmap[hight * 100 + tyle] = false;
                        moveendflag = false;
                    }
                    tyle = (int)(z / (360 / mapCreate.halfblock));
                    linkBlocks[0].attackflag = false;
                    linkBlocks[1].attackflag = false;
                    moveflag = false;
                    player.attackflag = false;
                    change = 0;
                    count = 0;
                    mapCreate.halfmap[hight * 100 + tyle] = true;
                    mapCreate.halftype[hight * 100 + tyle] = gameObject.transform.GetComponent<Block>();
                }

            }
            else
            if ((int)(z / (360 / mapCreate.halfblock)) < tyle)
            {
                if ((int)((z) / (360 / mapCreate.halfblock)) == 0 && tyle == 4)
                {
                    if (moveendflag)
                    {
                        mapCreate.halfmap[hight * 100 + tyle] = false;
                        moveendflag = false;
                    }
                    tyle = (int)(z / (360 / mapCreate.halfblock));
                    linkBlocks[0].attackflag = false;
                    linkBlocks[1].attackflag = false;
                    moveflag = false;
                    player.attackflag = false;
                    change = 0;
                    count = 0;
                    mapCreate.halfmap[hight * 100 + tyle] = true;
                    mapCreate.halftype[hight * 100 + tyle] = gameObject.transform.GetComponent<Block>();
                }
                else if ((int)((z - 5) / (360 / mapCreate.halfblock)) == tyle - 1 && tyle != 1)
                {
                    if (moveendflag)
                    {
                        mapCreate.halfmap[hight * 100 + tyle] = false;
                        moveendflag = false;
                    }
                    tyle = (int)(z / (360 / mapCreate.halfblock));
                    linkBlocks[0].attackflag = false;
                    linkBlocks[1].attackflag = false;
                    moveflag = false;
                    player.attackflag = false;
                    change = 0;
                    count = 0;
                    mapCreate.halfmap[hight * 100 + tyle] = true;
                    mapCreate.halftype[hight * 100 + tyle] = gameObject.transform.GetComponent<Block>();
                }
                if (tyle == 1 && z - speed <= 0)
                {
                    if (moveendflag)
                    {
                        mapCreate.halfmap[hight * 100 + tyle] = false;
                        moveendflag = false;
                    }
                    tyle = (int)(z / (360 / mapCreate.halfblock));
                    linkBlocks[0].attackflag = false;
                    linkBlocks[1].attackflag = false;
                    moveflag = false;
                    player.attackflag = false;
                    change = 0;
                    count = 0;
                    mapCreate.halfmap[hight * 100 + tyle] = true;
                    mapCreate.halftype[hight * 100 + tyle] = gameObject.transform.GetComponent<Block>();
                }
            }
        }
        if (moveflag)
        {
            //mapCreate.inmap[hight * 100 + tyle] = false;

            count++;
            if (change == 1)
            {
                //RotateAround(円運動の中心,進行方向,速度)
                transform.RotateAround(center.transform.position,
                -transform.up, speed);
            }
            else if (change == 2)
            {
                //RotateAround(円運動の中心,進行方向,速度)
                transform.RotateAround(center.transform.position,
                transform.up, speed);

            }
        }
        else if (!moveflag)
        {
            if (linkBlocks[0].attackflag)
            {
                moveflag = true;
                change = 1;
                moveendflag = true;
            }
            else
if (linkBlocks[1].attackflag)
            {
                moveflag = true;
                change = 2;
                moveendflag = true;
            }
        }
        //if (linkBlocks[3].hitblock.hight != hight)
        //{
        //    if (inout == InOut.In)
        //    {
        //        mapCreate.inmap[hight * 100 + tyle] = false;
        //        mapCreate.inmap[linkBlocks[3].hitblock.hight * 100 + tyle] = true;
        //    }
        //    else
        //    {
        //        mapCreate.outmap[hight * 100 + tyle] = false;
        //        mapCreate.outmap[linkBlocks[3].hitblock.hight * 100 + tyle] = true;
        //    }
        //    hight = linkBlocks[3].hitblock.hight;
        //}
        //else
        //if (linkBlocks[2].hitblock.hight != hight)
        //{
        //    if (inout == InOut.In)
        //    {
        //        mapCreate.inmap[hight * 100 + tyle] = false;
        //        mapCreate.inmap[linkBlocks[2].hitblock.hight * 100 + tyle] = true;
        //    }
        //    else
        //    {
        //        mapCreate.outmap[hight * 100 + tyle] = false;
        //        mapCreate.outmap[linkBlocks[2].hitblock.hight * 100 + tyle] = true;
        //    }
        //    hight = linkBlocks[2].hitblock.hight;
        //}
        //Debug.Log(linkBlocks[6].hitblock.hight);
        //}else
        if (linkBlocks[6].hitblock.hight - 1 != hight && linkBlocks[6].hitblock.hight != hight)
        {
            //Debug.Log(1);
            if (inout == InOut.In)
            {
                mapCreate.inmap[hight * 100 + tyle] = false;
                mapCreate.inmap[(linkBlocks[6].hitblock.hight - 1) * 100 + tyle] = true;
                mapCreate.intype[hight * 100 + tyle] = gameObject.transform.GetComponent<Block>();
            }
            else if(inout==InOut.Out)
            {
                mapCreate.outmap[hight * 100 + tyle] = false;
                mapCreate.outmap[(linkBlocks[6].hitblock.hight - 1) * 100 + tyle] = true;
                mapCreate.outtype[hight * 100 + tyle] = gameObject.transform.GetComponent<Block>();
            }
            else if (inout == InOut.Half)
            {
                mapCreate.halfmap[hight * 100 + tyle] = false;
                mapCreate.halfmap[(linkBlocks[6].hitblock.hight - 1) * 100 + tyle] = true;
                mapCreate.halftype[hight * 100 + tyle] = gameObject.transform.GetComponent<Block>();
            }
            Instantiate(kati, transform.position, Quaternion.identity);
            hight = linkBlocks[6].hitblock.hight - 1;
        }
        //else
        //if (linkBlocks[7].hitblock.hight - 1 != hight && linkBlocks[7].hitblock.hight != hight)
        //{
        //    if (inout == InOut.In)
        //    {
        //        mapCreate.inmap[hight * 100 + tyle] = false;
        //        mapCreate.inmap[(linkBlocks[7].hitblock.hight - 1) * 100 + tyle] = true;
        //        mapCreate.intype[hight * 100 + tyle] = gameObject.transform.GetComponent<Block>();
        //    }
        //    else
        //    {
        //        mapCreate.outmap[hight * 100 + tyle] = false;
        //        mapCreate.outmap[(linkBlocks[7].hitblock.hight - 1) * 100 + tyle] = true;
        //        mapCreate.outtype[hight * 100 + tyle] = gameObject.transform.GetComponent<Block>();
        //    }
        //    Instantiate(kati, transform.position, Quaternion.identity);
        //    hight = linkBlocks[7].hitblock.hight - 1;
        //}
        //if(inout==InOut.In)
        //{
        //    mapCreate.intype[hight * 100 + tyle] = gameObject.transform.GetComponent<Block>();
        //}else if(inout == InOut.Out)
        //{
        //    mapCreate.outtype[hight * 100 + tyle] = gameObject.transform.GetComponent<Block>();
        //}


    }

    void Damage()
    {
        if (inout == InOut.In)
        {
            if (tyle + 1 <= mapCreate.inblock - 1)
            {
                if (mapCreate.inmap[hight * 100 + tyle + 1]
                    && mapCreate.intype[hight * 100 + tyle + 1].block == block
                    && mapCreate.intype[hight * 100 + tyle + 1].damageflag)
                {

                    damageflag = true;

                }
            }
            if (tyle + 1 == mapCreate.inblock)
            {
                if (mapCreate.inmap[hight * 100]
                    && mapCreate.intype[hight * 100].block == block
                    && mapCreate.intype[hight * 100].damageflag)
                {
                    damageflag = true;

                }
            }
            if (tyle - 1 >= 0)
            {
                if (mapCreate.inmap[hight * 100 + (tyle - 1)]
                    && mapCreate.intype[hight * 100 + (tyle - 1)].block == block
                    && mapCreate.intype[hight * 100 + (tyle - 1)].damageflag)
                {
                    damageflag = true;
                }
            }
            if (tyle - 1 == -1)
            {
                if (mapCreate.inmap[hight * 100 + (mapCreate.inblock - 1)]
                    && mapCreate.intype[hight * 100 + (mapCreate.inblock - 1)].block == block
                    && mapCreate.intype[hight * 100 + (mapCreate.inblock - 1)].damageflag)
                {
                    damageflag = true;
                }
            }
        }
        if (inout == InOut.Out)
        {
            if (tyle + 1 <= mapCreate.outblock - 1)
            {
                if (mapCreate.outmap[hight * 100 + tyle + 1]
                    && mapCreate.outtype[hight * 100 + tyle + 1].block == block
                    && mapCreate.outtype[hight * 100 + tyle + 1].damageflag)
                {
                    damageflag = true;
                }
            }
            if (tyle + 1 == mapCreate.outblock)
            {
                if (mapCreate.outmap[hight * 100]
                    && mapCreate.outtype[hight * 100].block == block
                    && mapCreate.outtype[hight * 100].damageflag)
                {
                    damageflag = true;
                }
            }
            if (tyle - 1 >= 0)
            {
                if (mapCreate.outmap[hight * 100 + (tyle - 1)]
                    && mapCreate.outtype[hight * 100 + (tyle - 1)].block == block
                    && mapCreate.outtype[hight * 100 + (tyle - 1)].damageflag)
                {
                    damageflag = true;
                }
            }
            if (tyle - 1 == -1)
            {
                if (mapCreate.outmap[hight * 100 + (mapCreate.outblock - 1)]
                    && mapCreate.outtype[hight * 100 + (mapCreate.outblock - 1)].block == block
                    && mapCreate.outtype[hight * 100 + (mapCreate.outblock - 1)].damageflag)
                {
                    damageflag = true;
                }
            }
        }
        if (inout == InOut.Half)
        {
            if (tyle + 1 <= mapCreate.halfblock - 1)
            {
                if (mapCreate.halfmap[hight * 100 + tyle + 1]
                    && mapCreate.halftype[hight * 100 + tyle + 1].block == block
                    && mapCreate.halftype[hight * 100 + tyle + 1].damageflag)
                {
                    damageflag = true;
                }
            }
            if (tyle + 1 == mapCreate.halfblock)
            {
                if (mapCreate.halfmap[hight * 100]
                    && mapCreate.halftype[hight * 100].block == block
                    && mapCreate.halftype[hight * 100].damageflag)
                {
                    damageflag = true;
                }
            }
            if (tyle - 1 >= 0)
            {
                if (mapCreate.halfmap[hight * 100 + (tyle - 1)]
                    && mapCreate.halftype[hight * 100 + (tyle - 1)].block == block
                    && mapCreate.halftype[hight * 100 + (tyle - 1)].damageflag)
                {
                    damageflag = true;
                }
            }
            if (tyle - 1 == -1)
            {
                if (mapCreate.halfmap[hight * 100 + (mapCreate.halfblock - 1)]
                    && mapCreate.halftype[hight * 100 + (mapCreate.halfblock - 1)].block == block
                    && mapCreate.halftype[hight * 100 + (mapCreate.halfblock - 1)].damageflag)
                {
                    damageflag = true;
                }
            }
        }
        if (damageflag)
        {
        }
        else
        {
            if (hight >= -5)
            {
                if (inout == InOut.In)
                {
                    if (mapCreate.halfmap[hight * 100 + (int)(z / (360 / mapCreate.halfblock))])
                    {
                        if (mapCreate.halftype[hight * 100 + (int)(z / (360 / mapCreate.halfblock))].damageflag)
                        {
                            damageflag = true;
                            syuwa.Play();
                        }
                    }
                }
                if (inout == InOut.Out)
                {
                    if (mapCreate.halfmap[hight * 100 + (int)(z / (360 / mapCreate.halfblock))])
                    {
                        if (mapCreate.halftype[hight * 100 + (int)(z / (360 / mapCreate.halfblock))].damageflag)
                        {
                            damageflag = true;
                            syuwa.Play();
                        }
                    }
                }
                if (inout == InOut.Half)
                {
                    if (mapCreate.inmap[hight * 100 + (int)(z / (360 / mapCreate.inblock))])
                    {
                        if (mapCreate.intype[hight * 100 + (int)(z / (360 / mapCreate.inblock))].damageflag)
                        {
                            damageflag = true;
                            syuwa.Play();
                        }
                    }
                    if (mapCreate.outmap[hight * 100 + (int)(z / (360 / mapCreate.outblock))])
                    {
                        if (mapCreate.outtype[hight * 100 + (int)(z / (360 / mapCreate.outblock))].damageflag)
                        {
                            damageflag = true;
                            syuwa.Play();
                        }
                    }
                }
            }
        }

        if ((block == BlockType.Nomal && player.type != PlayerType.Nomal)||
            (block == BlockType.Fire && player.type != PlayerType.Fire) ||
            (block == BlockType.Fish && player.type != PlayerType.Fish) ||
            (block == BlockType.Etc && player.type != PlayerType.Etc) || player.hight + 1 != hight)
        {
            if (damageflag)
            {
                damageflag = false;
                syuwa.Stop();
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
                GameObject gameObject = Instantiate(bubble, new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), Quaternion.identity);
                if (inout == InOut.In)
                {
                    gameObject.transform.rotation = Quaternion.Euler(0, (360 / mapCreate.inblock) * (tyle), 0);
                }
                else if(inout==InOut.Out)
                {
                    gameObject.transform.rotation = Quaternion.Euler(0, (360 / mapCreate.outblock) * (tyle), 0);
                }
                else if (inout == InOut.Half)
                {
                    gameObject.transform.rotation = Quaternion.Euler(0, (360 / mapCreate.halfblock) * (tyle), 0);
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
            else if(inout==InOut.Out)
            {
                mapCreate.outmap[hight * 100 + tyle] = false;
            }
            else if (inout == InOut.Half)
            {
                mapCreate.halfmap[hight * 100 + tyle] = false;
            }
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (bubbleflag)
        {
            if (collision.gameObject.tag == "Block")
            {
                bubbleflag = false;
            }
        }
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
            hp -= 0.05f;
        }
    }
}
