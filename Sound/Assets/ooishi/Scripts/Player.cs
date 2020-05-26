using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerMove
{
    Down,
    Up,
    Right,
    Left
}


public class Player : MonoBehaviour
{
    public float speed;
    private int x;
    private int y;
    public int changeflag, saveflag, ren;
    private Rigidbody rigidbody;
    public float jumppower;
    public bool jumpflag;
    private Block block;
    public bool endflag;
    public bool moveflag;
    public int hight, tyle;
    private Vector3 pos;
    private float rad, degree;
    private MapCreate map;
    //private Renderer renderer;

    public AudioClip jumpse;
    private AudioSource source;

    public Animator anim;

    public bool flontflag, backflag, fbflag, bbflag;

    public int maphight;

    public bool damageflag;

    public float damagetime;
    private float savedamege;

    public int hp;
    public GameObject[] hps;
    private int hpcount;

    public PlayerMove playerMove;

    private float angle;

    public GameObject jetBallet;
    private float jetcount;
    public float range;

    private Vector3 save;

    public bool inrightroll, inleftroll,outrightroll,outleftroll;

    public Vector3 rollObj;

    public ChangeCam cam;

    public int movecount;
    public Map GetMap;
    // Start is called before the first frame update
    void Start()
    {
        jumpflag = true;
        playerMove = PlayerMove.Down;
        hpcount = 0;
        savedamege = damagetime;
        source = gameObject.GetComponent<AudioSource>();
        hight = 0;
        changeflag = 0;
        saveflag = 0;
        rigidbody = gameObject.transform.GetComponent<Rigidbody>();
        ren = 0;
        moveflag = false;
        map = GameObject.Find("MapCreate").GetComponent<MapCreate>();
        //renderer = gameObject.transform.GetComponent<Renderer>();
        flontflag = true;
        backflag = true;
        bbflag = false;
        fbflag = false;
        save = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        //Jump();
        //Damage();
        //hight = -(int)transform.position.y;
    }



    void Move()
    {
        if (!backflag || !flontflag)
        {
            //moveflag = false;
        }

        if (transform.position == save && (!inrightroll || !inleftroll))
        {
            moveflag = false;
        }
        if ((Input.GetKeyDown(KeyCode.D)) && !moveflag&&!outleftroll&&!inrightroll&&!outrightroll&&!inleftroll && !jumpflag)
        {

            if (backflag)
            {
                if(bbflag)
                {
                    outleftroll = true;
                    movecount++;
                }
                else
                {
                    if (!moveflag&&!outleftroll&&!inrightroll)
                    {
                        changeflag = 0;
                        moveflag = true;
                        if (playerMove == PlayerMove.Down)
                        {
                            save = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                        }
                        if (playerMove == PlayerMove.Up)
                        {
                            save = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                        }
                        if (playerMove == PlayerMove.Right)
                        {
                            save = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                        }
                        if (playerMove == PlayerMove.Left)
                        {
                            save = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
                        }
                        movecount++;
                        anim.SetBool("Dash", true);
                    }
                }

            }
            else
            {
                inrightroll = true;
                movecount++;
            }
        }
        else
        if ((Input.GetKeyDown(KeyCode.A)) && !moveflag && !outleftroll && !inrightroll && !outrightroll && !inleftroll&&!jumpflag)
        {

            if (flontflag)
            {
                if(fbflag)
                {
                    outrightroll = true;
                    movecount++;
                }
                else
                {
                    if (!moveflag&&!outrightroll&&!inleftroll)
                    {
                        changeflag = 1;
                        moveflag = true;
                        if(playerMove==PlayerMove.Down)
                        {
                            save = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                        }
                        if (playerMove == PlayerMove.Up)
                        {
                            save = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                        }
                        if (playerMove == PlayerMove.Right)
                        {
                            save = new Vector3(transform.position.x , transform.position.y-1, transform.position.z);
                        }
                        if (playerMove == PlayerMove.Left)
                        {
                            save = new Vector3(transform.position.x, transform.position.y+1, transform.position.z);
                        }
                        movecount++;
                        anim.SetBool("Dash", true);
                    }
                }
            }
            else
            {
                inleftroll = true;
                movecount++;
            }
        }
        //}else if(Input.GetKey(KeyCode.Q)&&jumpflag)
        //{
        //    transform.Rotate(0, 0, -5);

        //}
        //else if (Input.GetKey(KeyCode.E) && jumpflag)
        //{
        //    transform.Rotate(0, 0, 5);
        //}
        Quaternion woldangle = transform.localRotation;
        Vector3 vector3 = woldangle.eulerAngles;


        //woldangle = Quaternion.Euler(vector3);

        angle = transform.rotation.eulerAngles.z;
        if (inrightroll)
        {
            transform.Rotate(0, 0, -5);
        }
        if (inleftroll)
        {
            transform.Rotate(0, 0, +5);
        }
        if (outrightroll)
        {
            transform.RotateAround(rollObj, -transform.forward, 100*Time.deltaTime);
        }
        if (outleftroll)
        {
            transform.RotateAround(rollObj, transform.forward, 100*Time.deltaTime);
        }
        if (playerMove == PlayerMove.Down)
        {
            if (angle >= 90 && angle < 180)
            {
                playerMove = PlayerMove.Left;
                angle = 90;
                transform.localEulerAngles = new Vector3(0,0,angle);
                outleftroll = false;
                inleftroll = false;
                moveflag = false;
            }
            if (angle < 270 && angle > 180)
            {
                angle = 270;
                transform.localEulerAngles = new Vector3(0, 0, angle);
                outrightroll = false;
                inrightroll = false;
                playerMove = PlayerMove.Right;
                moveflag = false;
            }
        }
        if (playerMove == PlayerMove.Up)
        {
            if (angle <= 360 && angle > 270)
            {
                playerMove = PlayerMove.Right;
                angle = 270;
                transform.localEulerAngles = new Vector3(0, 0, angle);
                outleftroll = false;
                inleftroll = false;

                moveflag = false;
            }
            if (angle < 90 && angle > 0)
            {
                angle = 90;
                transform.localEulerAngles = new Vector3(0, 0, angle);
                outrightroll = false;
                inrightroll = false;
                playerMove = PlayerMove.Left;
                moveflag = false;
            }
        }
        if (playerMove == PlayerMove.Right)
        {
            if (angle >= 180 && angle <= 185)
            {
                playerMove = PlayerMove.Up;
                angle = -179;
                transform.localEulerAngles = new Vector3(0, 0, angle);
                outrightroll = false;
                inrightroll = false;
                moveflag = false;
            }
            if (angle < 90 && angle > 0)
            {
                angle = 0;
                transform.localEulerAngles = new Vector3(0, 0, angle);
                outleftroll = false;
                inleftroll = false;
                playerMove = PlayerMove.Down;
                moveflag = false;
            }
        }
        if (playerMove == PlayerMove.Left)
        {
            if (angle >= 0 && angle > 270)
            {

                playerMove = PlayerMove.Down;
                angle = 0;
                transform.localEulerAngles = new Vector3(0, 0, angle);
                outrightroll = false;
                inrightroll = false;
                moveflag = false;
            }
            if (angle <= 180 && angle >= 175)
            {
                angle = 181;
                transform.localEulerAngles = new Vector3(0, 0, angle);
                outleftroll = false;
                inleftroll = false;
                playerMove = PlayerMove.Up;
                moveflag = false;
            }
        }
        switch (playerMove)
        {
            case PlayerMove.Down:
                if (moveflag)
                {
                    if (changeflag == 0)
                    {
                        transform.position -= new Vector3(speed, 0, 0);
                        if (transform.position.x <= save.x)
                        {
                            transform.position = new Vector3(save.x, transform.position.y, transform.position.z);
                            moveflag = false;
                        }
                    }
                    else
                    {
                        transform.position += new Vector3(speed, 0, 0);
                        if (transform.position.x >= save.x)
                        {
                            transform.position = new Vector3(save.x, transform.position.y, transform.position.z);
                            moveflag = false;
                        }

                    }
                }
                if(!inrightroll&&!inleftroll&&!outrightroll&&!outleftroll)
                rigidbody.AddForce(0, -10, 0);
                break;
            case PlayerMove.Up:
                if (moveflag)
                {
                    if (changeflag == 0)
                    {
                        transform.position += new Vector3(speed, 0, 0);
                        if (transform.position.x >= save.x)
                        {
                            transform.position = new Vector3(save.x, transform.position.y, transform.position.z);
                            moveflag = false;
                        }
                    }
                    else
                    {
                        transform.position -= new Vector3(speed, 0, 0);
                        if (transform.position.x <= save.x)
                        {
                            transform.position = new Vector3(save.x, transform.position.y, transform.position.z);
                            moveflag = false;
                        }
                    }
                }
                if (!inrightroll && !inleftroll && !outrightroll && !outleftroll)
                    rigidbody.AddForce(0, 10, 0);
                break;
            case PlayerMove.Right:
                if (moveflag)
                {
                    if (changeflag == 0)
                    {
                        transform.position += new Vector3(0, speed, 0);
                        if (transform.position.y >= save.y)
                        {
                            transform.position = new Vector3(transform.position.x, save.y, transform.position.z);
                            moveflag = false;
                        }
                    }
                    else
                    {
                        transform.position -= new Vector3(0, speed, 0);
                        if (transform.position.y <= save.y)
                        {
                            transform.position = new Vector3(transform.position.x, save.y, transform.position.z);
                            moveflag = false;
                        }

                    }
                }
                if (!inrightroll && !inleftroll && !outrightroll && !outleftroll)
                    rigidbody.AddForce(-10, 0, 0);
                break;
            case PlayerMove.Left:
                if (moveflag)
                {
                    if (changeflag == 0)
                    {
                        transform.position -= new Vector3(0, speed, 0);
                        if (transform.position.y <= save.y)
                        {
                            transform.position = new Vector3(transform.position.x, save.y, transform.position.z);
                            moveflag = false;
                        }
                    }
                    else
                    {

                        transform.position += new Vector3(0, speed, 0);
                        if (transform.position.y >= save.y)
                        {
                            transform.position = new Vector3(transform.position.x, save.y, transform.position.z);
                            moveflag = false;
                        }
                    }
                }
                if (!inrightroll && !inleftroll && !outrightroll && !outleftroll)
                    rigidbody.AddForce(10, 0, 0);
                break;
        }
        if (cam.changeflag)
        {
            if (!GetMap.maps[(y * 1000) + x])
            {
                jumpflag = true;
            }
        }

    }


    //void Damage()
    //{
    //    if (damageflag)
    //    {
    //        gameObject.layer = 13;
    //        damagetime -= Time.deltaTime;
    //    }
    //    if (damagetime < 0)
    //    {
    //        gameObject.layer = 14;

    //        damagetime = savedamege;
    //        damageflag = false;
    //    }

    //    if (hp <= 0)
    //    {
    //        endflag = true;
    //    }

    //}

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Block")
        {
            block = collision.gameObject.GetComponent<Block>();
            hight = collision.gameObject.GetComponent<Block>().maphight;
            tyle = collision.gameObject.GetComponent<Block>().tyle;
            //rollObj = collision.gameObject.transform.position;

            if(playerMove==PlayerMove.Down)
            {
                if (GetMap.width > collision.gameObject.GetComponent<Block>().xx + 1)
                {
                    if (!GetMap.maps[(collision.gameObject.GetComponent<Block>().yy * 1000) + collision.gameObject.GetComponent<Block>().xx+1])
                    {
                        bbflag = true;
                    }
                    if (0 <= collision.gameObject.GetComponent<Block>().yy - 1)
                    {
                        if (!GetMap.maps[((collision.gameObject.GetComponent<Block>().yy - 1) * 1000) + collision.gameObject.GetComponent<Block>().xx + 1])
                        {
                            backflag = true;
                        }
                    }
                    else
                    {
                        backflag = true;
                    }

                }
                else
                {
                    backflag = true;
                    bbflag = true;
                }
                if (0 <= collision.gameObject.GetComponent<Block>().xx - 1)
                {
                    if (!GetMap.maps[(collision.gameObject.GetComponent<Block>().yy * 1000) + collision.gameObject.GetComponent<Block>().xx - 1])
                    {
                        fbflag = true;
                    }

                    if (0 <= collision.gameObject.GetComponent<Block>().yy - 1)
                    {
                        if (!GetMap.maps[((collision.gameObject.GetComponent<Block>().yy - 1) * 1000) + collision.gameObject.GetComponent<Block>().xx - 1])
                        {
                            flontflag = true;
                        }
                    }
                    else
                    {
                        flontflag = true;
                    }
                }
                else
                {
                    flontflag = true;
                    fbflag = true;
                }

            }
            if (playerMove == PlayerMove.Up)
            {
                if (GetMap.width > collision.gameObject.GetComponent<Block>().xx + 1)
                {
                    if (!GetMap.maps[(collision.gameObject.GetComponent<Block>().yy * 1000) + collision.gameObject.GetComponent<Block>().xx+1])
                    {
                        fbflag = true;
                    }
                    if (GetMap.hight > collision.gameObject.GetComponent<Block>().yy + 1)
                    {
                        if (!GetMap.maps[((collision.gameObject.GetComponent<Block>().yy + 1) * 1000) + collision.gameObject.GetComponent<Block>().xx + 1])
                        {
                            flontflag = true;
                        }
                    }
                    else
                    {
                        flontflag = true;
                    }
                }
                else
                {
                    flontflag = true;
                    fbflag = true;
                }
                if (0 <= collision.gameObject.GetComponent<Block>().xx - 1)
                {
                    if (!GetMap.maps[(collision.gameObject.GetComponent<Block>().yy * 1000) + collision.gameObject.GetComponent<Block>().xx - 1])
                    {
                        bbflag = true;
                    }
                    if (GetMap.hight > collision.gameObject.GetComponent<Block>().yy + 1)
                    {
                        if (!GetMap.maps[((collision.gameObject.GetComponent<Block>().yy + 1) * 1000) + collision.gameObject.GetComponent<Block>().xx - 1])
                        {
                            backflag = true;
                        }
                    }
                    else
                    {
                        backflag = true;
                    }
                }
                else
                {
                    backflag = true;
                    bbflag = true;
                }
            }
             
            if (playerMove == PlayerMove.Right)
            {
                if (GetMap.hight > collision.gameObject.GetComponent<Block>().yy + 1)
                {
                    if (!GetMap.maps[((collision.gameObject.GetComponent<Block>().yy+1) * 1000) + collision.gameObject.GetComponent<Block>().xx])
                    {
                        fbflag = true;
                    }
                    if (0 <= collision.gameObject.GetComponent<Block>().xx - 1)
                    {
                        if (!GetMap.maps[((collision.gameObject.GetComponent<Block>().yy + 1) * 1000) + collision.gameObject.GetComponent<Block>().xx - 1])
                        {
                            flontflag = true;
                        }
                    }
                    else
                    {
                        flontflag = true;
                    }
                }
                else
                {
                    flontflag = true;
                    fbflag = true;
                }
                if (0 <= collision.gameObject.GetComponent<Block>().yy - 1)
                {
                    if (!GetMap.maps[((collision.gameObject.GetComponent<Block>().yy-1) * 1000) + collision.gameObject.GetComponent<Block>().xx])
                    {
                        bbflag = true;
                    }
                    if (0 <= collision.gameObject.GetComponent<Block>().xx - 1)
                    {
                        if (!GetMap.maps[((collision.gameObject.GetComponent<Block>().yy - 1) * 1000) + collision.gameObject.GetComponent<Block>().xx - 1])
                        {
                            backflag = true;
                        }
                    }
                    else
                    {
                        backflag = true;
                    }
                }
                else
                {
                    backflag = true;
                    bbflag = true;
                }
            }
            if (playerMove == PlayerMove.Left)
            {
                if (GetMap.hight > collision.gameObject.GetComponent<Block>().yy + 1)
                {
                    if (!GetMap.maps[((collision.gameObject.GetComponent<Block>().yy + 1) * 1000) + collision.gameObject.GetComponent<Block>().xx])
                    {
                        bbflag = true;
                    }
                    if (GetMap.width > collision.gameObject.GetComponent<Block>().xx + 1)
                    {
                        if (!GetMap.maps[((collision.gameObject.GetComponent<Block>().yy + 1) * 1000) + collision.gameObject.GetComponent<Block>().xx + 1])
                        {
                            backflag = true;
                        }
                    }
                    else
                    {
                        backflag = true;
                    }
                }
                else
                {
                    backflag = true;
                    bbflag = true;
                }
                if (GetMap.hight > collision.gameObject.GetComponent<Block>().yy + 1)
                {
                    if (!GetMap.maps[((collision.gameObject.GetComponent<Block>().yy + 1) * 1000) + collision.gameObject.GetComponent<Block>().xx])
                    {
                        fbflag = true;
                    }
                    if (GetMap.width > collision.gameObject.GetComponent<Block>().xx + 1)
                    {
                        if (!GetMap.maps[((collision.gameObject.GetComponent<Block>().yy + 1) * 1000) + collision.gameObject.GetComponent<Block>().xx + 1])
                        {
                            flontflag = true;
                        }
                    }
                    else
                    {
                        flontflag = true;
                    }
                }
                else
                {
                    flontflag = true;
                    fbflag = true;
                }
            }
            x = collision.gameObject.GetComponent<Block>().xx;
            y = collision.gameObject.GetComponent<Block>().yy;
            //collision.gameObject.GetComponent<Block>().damageflag=true;
            if (!cam.changeflag)
            {
                cam.changeflag = true;
            }
        }
        if (collision.gameObject.tag == "Ground")
        {
            //hight = map.maps.Length - 1;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (!damageflag)
            {
                damageflag = true;
                hp--;
                Destroy(hps[hpcount]);
                hpcount++;
            }
            // moveflag = false;
        }
        if (other.tag == "Item")
        {
            if (other.gameObject.GetComponent<Item>().item == ItemSelect.Hart)
            {
                if (hp < hps.Length)
                {
                    hp++;
                }
            }
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
