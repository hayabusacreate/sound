using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{
    Nomal,
    Fire
}
public class Player : MonoBehaviour
{
    public float speed;
    private float radius;
    public float inradius, outradius;
    private float x;
    private float z;
    public GameObject center, inobj, outobj;
    private Vector3 invec, outvec;
    public int changeflag, saveflag, ren;
    private Rigidbody rigidbody;
    public float jumppower;
    private bool jumpflag;
    public PlayerType type;
    private Block block;
    public bool attackflag;
    private float attacktime;
    public float tackeletime;
    public bool endflag;
    public bool moveflag;
    public bool startflag;
    public int hight, tyle;
    private Vector3 pos;
    private float rad, degree;
    private MapCreate map;
    private Renderer renderer;
    public GameObject takle, jump;
    // Start is called before the first frame update
    void Start()
    {
        hight = 0;
        radius = inradius;
        changeflag = 0;
        saveflag = 0;
        rigidbody = gameObject.transform.GetComponent<Rigidbody>();
        ren = 0;
        type = PlayerType.Nomal;
        moveflag = true;
        map = GameObject.Find("MapCreate").GetComponent<MapCreate>();
        renderer = gameObject.transform.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Attack();
        //hight = -(int)transform.position.y;
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.E) && !attackflag)
        {
            startflag = true;
            attackflag = true;
            Instantiate(takle, transform.position, Quaternion.identity);
        }
        if (attackflag)
        {
            moveflag = true;
            if (changeflag == 0)
            {
                //RotateAround(円運動の中心,進行方向,速度)
                transform.RotateAround(center.transform.position,
                transform.up, (speed * 4) / radius);
            }
            else if (changeflag == 1)
            {
                //RotateAround(円運動の中心,進行方向,速度)
                transform.RotateAround(center.transform.position,
                -transform.up, (speed * 4) / radius);
            }

            attacktime += Time.deltaTime;
            if (tackeletime < attacktime)
            {
                attacktime = 0;
                attackflag = false;
            }
        }
    }

    void Move()
    {
        //Debug.Log("Player" + hight * 100 + (int)(((degree+15) % 360) / (360 / (map.inblock))));
        pos = new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(center.transform.position.x, 0, center.transform.position.z);
        rad = Mathf.Atan2(pos.x, pos.z);
        degree = rad * Mathf.Rad2Deg;
        if (ren == 0)
        {
            degree += (360 / (map.inblock))*2.35f;
        }
        else if (ren == 1)
        {
            degree += (360 / (map.outblock))*2.5f;
        }
        if (degree < 0)
        {
            degree += 360;
        }
        //degree -= (360 / map.inblock);

        radius = Vector3.Distance(transform.position, center.transform.position);
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            //changeflag = 0;
            moveflag = false;
        }
        if (Input.GetKey(KeyCode.W) )
        {
            changeflag = 0;
            moveflag = true;
        }
        else
        if (Input.GetKey(KeyCode.S) )
        {
            changeflag = 1;
            moveflag = true;

        }
        if (Input.GetKeyDown(KeyCode.A) && ren != 0&&!map.inmap[hight * 100 + (int)(((degree) % 360) / (360 / (map.inblock)))])
        {
            changeflag = 2;
        }
        else
        if (Input.GetKeyDown(KeyCode.D) && ren != 1
            &&! map.outmap[hight * 100 + (int)(((degree) % 360) / (360 / (map.outblock)))]
            )
        {
            changeflag = 3;
        }
        if (ren == 0)
        {
            if(changeflag==0)
            {
                if (map.inmap[hight * 100 + (int)((degree+(360/map.inblock)/4 )%360  / (360 / (map.inblock)))] && changeflag == 0)
                {
                    moveflag = false;
                }
            }else if(changeflag==1)
            {
                if((degree - (360 / (map.inblock) / 2)) % 360>0)
                {
                    if (map.inmap[hight * 100 + (int)((degree-(360 / map.inblock) / 4) % 360) / (360 / (map.inblock))] && changeflag == 1)
                    {
                        moveflag = false;
                    }
                }else
                {
                    if (map.inmap[hight * 100 + (int)((360+degree- (360 / map.inblock) / 4) % 360) / (360 / (map.inblock))] && changeflag == 1)
                    {
                        moveflag = false;
                    }
                }


            }


        }
        else
        {
            if (changeflag == 0)
            {
                if (map.outmap[hight * 100 + (int)(((degree + (360 / (map.outblock) / 8)) % 360) / (360 / (map.outblock)))] && changeflag == 0)
                {
                    moveflag = false;
                }
            }
            else if (changeflag == 1)
            {
                if ((degree - (360 / (map.outblock) / 2)) % 360 > 0)
                {
                    if (map.outmap[hight * 100 + (int)((degree - (360 / (map.outblock) / 8) % 360) / (360 / (map.outblock)))] && changeflag == 1)
                    {
                        moveflag = false;
                    }
                }
                else
                {
                    if (map.outmap[hight * 100 + (int)(((360 + degree) - (360 / (map.outblock) / 8) % 360) / (360 / (map.outblock)))] && changeflag == 1)
                    {
                        moveflag = false;
                    }
                }


            }

        }
        if (changeflag == 0 && !attackflag && moveflag)
        {
            //RotateAround(円運動の中心,進行方向,速度)
            transform.RotateAround(center.transform.position,
            transform.up, speed);
        }
        else if (changeflag == 1 && !attackflag && moveflag)
        {
            //RotateAround(円運動の中心,進行方向,速度)
            transform.RotateAround(center.transform.position,
            -transform.up, speed);
        }
        else if (changeflag == 2)
        {
            radius = inradius;
        }
        else if (changeflag == 3)
        {
            radius = outradius;
        }

        if (changeflag == 0 || changeflag == 1)
        {
            invec = inobj.transform.position;
            outvec = outobj.transform.position;
            saveflag = changeflag;
        }
        else
        {
            if (saveflag == 0)
            {
                if (changeflag == 2)
                {
                    transform.position = Vector3.Lerp(transform.position, invec, 0.1f);
                    if ((transform.position.x > invec.x && transform.position.x <= invec.x + 0.1f)
                        || (transform.position.x < invec.x && transform.position.x >= invec.x - 0.1f))
                    {
                        transform.position = invec;
                        changeflag = 0;
                        ren = 0;
                    }
                }
                else
                {
                    transform.position = Vector3.Lerp(transform.position, outvec, 0.1f);
                    if ((transform.position.x > outvec.x && transform.position.x <= outvec.x + 0.1f)
                        || (transform.position.x < outvec.x && transform.position.x >= outvec.x - 0.1f))
                    {
                        transform.position = outvec;
                        changeflag = 0;
                        ren = 1;
                    }
                }
            }
            else
            {
                if (changeflag == 2)
                {
                    transform.position = Vector3.Lerp(transform.position, invec, 0.1f);
                    if ((transform.position.x > invec.x && transform.position.x <= invec.x + 0.1f)
                        || (transform.position.x < invec.x && transform.position.x >= invec.x - 0.1f))
                    {
                        transform.position = invec;
                        changeflag = 1;
                        ren = 0;
                    }
                }
                else
                {
                    transform.position = Vector3.Lerp(transform.position, outvec, 0.1f);
                    if ((transform.position.x > outvec.x && transform.position.x <= outvec.x + 0.1f)
                        || (transform.position.x < outvec.x && transform.position.x >= outvec.x - 0.1f))
                    {
                        transform.position = outvec;
                        changeflag = 1;
                        ren = 1;
                    }
                }
            }
        }

        if (moveflag)
        {
            speed = 3;
            renderer.material.color = Color.white;
        }
        else
        {
            speed = 0;
            renderer.material.color = Color.white;
        }
        if(moveflag&&attackflag)
        {
            renderer.material.color = Color.red;
            speed = 3;
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !jumpflag)
        {
            rigidbody.velocity += new Vector3(0, jumppower, 0);
            Instantiate(jump, transform.position, Quaternion.identity);
            jumpflag = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (jumpflag)
        {
            jumpflag = false;
        }
        if (collision.gameObject.tag == "Block")
        {
            if (type == PlayerType.Nomal && collision.gameObject.GetComponent<Block>().block == BlockType.Fire)
            {
                type = PlayerType.Fire;
                //block.hp++;
            }
            if (type == PlayerType.Fire && collision.gameObject.GetComponent<Block>().block == BlockType.Nomal)
            {
                type = PlayerType.Nomal;
                //block.hp++;
            }
            block = collision.gameObject.GetComponent<Block>();
            hight = collision.gameObject.GetComponent<Block>().hight-1;
            tyle = collision.gameObject.GetComponent<Block>().tyle;
        }
        if(collision.gameObject.tag=="Ground")
        {
            hight = map.maps.Length - 1;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Side")
        {
           // moveflag = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Side")
        {
           // moveflag = true;
        }
    }
}
