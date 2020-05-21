using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{
    Nomal,
    Fire,
    Fish,
    Etc,
}
public enum PlayerMove
{
    Down,
    Up,
    Right,
    Left
}

public enum PlayerSkill
{
    Null,
    Gravity,
    Fire,
    Jet,
}

public class Player : MonoBehaviour
{
    public float speed;
    private float x;
    private float z;
    public int changeflag, saveflag, ren;
    private Rigidbody rigidbody;
    public float jumppower;
    private bool jumpflag;
    public PlayerType type;
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

    public bool flontflag, backflag,fbflag,bbflag;

    public int maphight;

    public bool damageflag;

    public float damagetime;
    private float savedamege;

    public int hp;
    public GameObject[] hps;
    private int hpcount;

    public PlayerMove playerMove;

    private float angle;

    public PlayerSkill skill;
    public GameObject jetBallet;
    private float jetcount;
    public float range;
    // Start is called before the first frame update
    void Start()
    {
        jumpflag = true;
        playerMove = PlayerMove.Down;
        hpcount = 0 ;
        savedamege = damagetime;
        source = gameObject.GetComponent<AudioSource>();
        hight = 0;
        changeflag = 0;
        saveflag = 0;
        rigidbody = gameObject.transform.GetComponent<Rigidbody>();
        ren = 0;
        type = PlayerType.Nomal;
        moveflag = true;
        map = GameObject.Find("MapCreate").GetComponent<MapCreate>();
        //renderer = gameObject.transform.GetComponent<Renderer>();
        flontflag = true;
        backflag = true;
        bbflag = false;
        fbflag = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Damage();
        //hight = -(int)transform.position.y;
    }

 

    void Move()
    {
        if ((!Input.GetKey(KeyCode.D) || !Input.GetKey(KeyCode.A) )|| (Input.GetAxis("Horizontal") < -0.8f&& Input.GetAxis("Horizontal") > 0.8f))
        {
            //changeflag = 0;
            moveflag = false;
            anim.SetBool("Dash", false);
        }
        if (!backflag||!flontflag)
        {
            moveflag = false;
        }


        if ((Input.GetKey(KeyCode.D) || Input.GetAxis("Horizontal") > 0.8f) && (changeflag == 0 || changeflag == 1))
        {
            if(skill==PlayerSkill.Gravity)
            {
                if (backflag)
                {
                    if (!flontflag)
                    {
                        flontflag = true;
                    }
                    if(bbflag)
                    {
                        transform.Rotate(0, 0, 5);
                        moveflag = false;
                    }
                    else
                    {
                        changeflag = 0;
                        moveflag = true;
                        anim.SetBool("Dash", true);
                    }

                }
                else
                {
                    transform.Rotate(0, 0, -5);
                    moveflag = false;
                }
            }
            else
            {
                changeflag = 0;
                moveflag = true;
                anim.SetBool("Dash", true);
            }


        }
        else
        if ((Input.GetKey(KeyCode.A) || Input.GetAxis("Horizontal") < -0.8f) && (changeflag == 0 || changeflag == 1))
        {
            if(skill==PlayerSkill.Gravity)
            {
                if (flontflag)
                {
                    if (!backflag)
                    {
                        backflag = true;
                    }
                    if(fbflag)
                    {
                        transform.Rotate(0, 0, -5);
                        moveflag = false;
                    }else
                    {
                        changeflag = 1;
                        moveflag = true;
                        anim.SetBool("Dash", true);
                    }
                }
                else
                {
                    moveflag = false;
                    transform.Rotate(0, 0, 5);
                }
            }
            else
            {
                changeflag = 1;
                moveflag = true;
                anim.SetBool("Dash", true);
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
        Quaternion woldangle=transform.localRotation;
        Vector3 vector3 = woldangle.eulerAngles;


        //woldangle = Quaternion.Euler(vector3);
        if(skill==PlayerSkill.Gravity)
        {
            angle = transform.rotation.eulerAngles.z;
            //if(moveflag)
            //{
            //    if (playerMove == PlayerMove.Down)
            //    {

            //        if (!jumpflag)
            //        {
            //            if (angle <= 270 && angle > 180)
            //            {
            //                playerMove = PlayerMove.Right;
            //                flontflag = true;
            //                backflag = true;
            //                fbflag = false;
            //                bbflag = false;
            //            }
            //            else

            //               if (angle >= 90 && angle < 180)
            //            {
            //                playerMove = PlayerMove.Left;
            //                flontflag = true;
            //                backflag = true;
            //                fbflag = false;
            //                bbflag = false;
            //            }
            //        }

            //    }
            //    if (playerMove == PlayerMove.Up)
            //    {
            //        if (!jumpflag)
            //        {
            //            if (angle <= 90 && angle > 0)
            //            {
            //                playerMove = PlayerMove.Left;
            //                flontflag = true;
            //                backflag = true;
            //                fbflag = false;
            //                bbflag = false;
            //            }
            //            else
            //            if (angle <= 360 && angle > 270)
            //            {
            //                playerMove = PlayerMove.Right;
            //                flontflag = true;
            //                backflag = true;
            //                fbflag = false;
            //                bbflag = false;
            //            }
            //        }

            //    }
            //    if (playerMove == PlayerMove.Right)
            //    {
            //        if (!jumpflag)
            //        {
            //            if (angle < 90 && angle > 0)
            //            {
            //                playerMove = PlayerMove.Down;
            //                flontflag = true;
            //                backflag = true;
            //                fbflag = false;
            //                bbflag = false;
            //            }
            //            else
            //            if (angle > 90 && angle < 180)
            //            {
            //                playerMove = PlayerMove.Up;
            //                flontflag = true;
            //                backflag = true;
            //                fbflag = false;
            //                bbflag = false;
            //            }
            //        }

            //    }
            //    if (playerMove == PlayerMove.Left)
            //    {
            //        if (!jumpflag)
            //        {
            //            if (angle > 180 && angle < 270)
            //            {
            //                playerMove = PlayerMove.Up;
            //                flontflag = true;
            //                backflag = true;
            //                fbflag = false;
            //                bbflag = false;
            //            }
            //            if (angle > 0 && angle > 270)
            //            {
            //                playerMove = PlayerMove.Down;
            //                flontflag = true;
            //                backflag = true;
            //                fbflag = false;
            //                bbflag = false;
            //            }
            //        }
            //    }


            //}
            switch (playerMove)
            {
                case PlayerMove.Down:
                    if (moveflag)
                    {
                        if (changeflag == 0)
                        {
                            rigidbody.AddForce(-speed, 0, 0);
                            transform.position -= new Vector3(speed, 0, 0);
                        }
                        else
                        {
                            rigidbody.AddForce(speed, 0, 0);
                            transform.position += new Vector3(speed, 0, 0);
                        }
                    }

                    rigidbody.AddForce(0, -10, 0);
                    break;
                case PlayerMove.Up:
                    if (moveflag)
                    {
                        if (changeflag == 0)
                        {
                            rigidbody.AddForce(-speed, 0, 0);
                            transform.position += new Vector3(speed, 0, 0);
                        }
                        else
                        {
                            rigidbody.AddForce(speed, 0, 0);
                            transform.position -= new Vector3(speed, 0, 0);
                        }
                    }

                    rigidbody.AddForce(0, 10, 0);
                    break;
                case PlayerMove.Right:
                    if (moveflag)
                    {
                        if (changeflag == 0)
                        {
                            rigidbody.AddForce(-speed, 0, 0);
                            transform.position += new Vector3(0, speed, 0);
                        }
                        else
                        {
                            rigidbody.AddForce(speed, 0, 0);
                            transform.position -= new Vector3(0, speed, 0);
                        }
                    }

                    rigidbody.AddForce(-10, 0, 0);
                    break;
                case PlayerMove.Left:
                    if (moveflag)
                    {
                        if (changeflag == 0)
                        {
                            rigidbody.AddForce(-speed, 0, 0);
                            transform.position -= new Vector3(0, speed, 0);
                        }
                        else
                        {
                            rigidbody.AddForce(speed, 0, 0);
                            transform.position += new Vector3(0, speed, 0);
                        }
                    }

                    rigidbody.AddForce(10, 0, 0);
                    break;
            }
        }else
        {
            if(moveflag)
            {
                if (changeflag == 0)
                {
                    rigidbody.AddForce(-speed, 0, 0);
                    transform.position -= new Vector3(speed, 0, 0);
                }
                else
                {
                    rigidbody.AddForce(speed, 0, 0);
                    transform.position += new Vector3(speed, 0, 0);
                }
            }


            rigidbody.AddForce(0, -10, 0);
        }








        if (moveflag)
        {
            if (transform.position.x + speed * 2 < (-map.width + 1) || transform.position.x + -speed * 2 > 0)
            {
                moveflag = false;
                if (changeflag == 0)
                {
                    //rigidbody.AddForce(speed, 0, 0);
                    transform.position += new Vector3(speed, 0, 0);
                }
                else
                {
                    //rigidbody.AddForce(-speed, 0, 0);
                    transform.position -= new Vector3(speed, 0, 0);
                }
            }
        }

    }

    void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.JoystickButton0)) && !jumpflag)
        {
            source.PlayOneShot(jumpse);
            rigidbody.velocity += new Vector3(0, jumppower, 0);
            jumpflag = true;
            anim.SetBool("Jump", true);
        }
        if(skill==PlayerSkill.Jet)
        {
            if(Input.GetKey(KeyCode.Space))
            {
                if(range<3)
                {
                    rigidbody.velocity += new Vector3(0, jumppower / 20, 0);
                }

                if(jetcount>0.3f)
                {
                    Instantiate(jetBallet, transform.position, Quaternion.identity);
                    jetcount = 0;
                }

            }
            jetcount += Time.deltaTime;
        }
    }

    void Damage()
    {
        if(damageflag)
        {
            gameObject.layer = 13;
            damagetime -= Time.deltaTime;
        }
        if(damagetime<0)
        {
            gameObject.layer = 14;

            damagetime = savedamege;
            damageflag = false;
        }

        if(hp<=0)
        {
            endflag = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (jumpflag)
        {
            jumpflag = false;
            anim.SetBool("Jump", false);
        }
        if (collision.gameObject.tag == "Block")
        {
            block = collision.gameObject.GetComponent<Block>();
            hight = collision.gameObject.GetComponent<Block>().maphight;
            tyle = collision.gameObject.GetComponent<Block>().tyle;
            collision.gameObject.GetComponent<Block>().damageflag=true;
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

            if(!damageflag)
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
            if(other.gameObject.GetComponent<Item>().item==ItemSelect.Hart)
            {
                if(hp<hps.Length)
                {
                    hp++;
                }
            }
            if (other.gameObject.GetComponent<Item>().item == ItemSelect.Gravity)
            {
                skill = PlayerSkill.Gravity;
            }
            if (other.gameObject.GetComponent<Item>().item == ItemSelect.Jet)
            {
                skill = PlayerSkill.Jet;
            }
            if (other.gameObject.GetComponent<Item>().item == ItemSelect.Fire)
            {
                skill = PlayerSkill.Fire;
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
