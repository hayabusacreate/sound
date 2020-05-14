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

    private Animator anim;

    public bool flontflag, backflag;

    public int maphight;

    public bool damageflag;

    public float damagetime;
    private float savedamege;

    public int hp;
    public GameObject[] hps;
    private int hpcount;
    // Start is called before the first frame update
    void Start()
    {
        hpcount = 0 ;
        savedamege = damagetime;
        anim = gameObject.GetComponent<Animator>();
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
        if ((!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A) )|| (Input.GetAxis("Horizontal") < -0.8f&& Input.GetAxis("Horizontal") > 0.8f))
        {
            //changeflag = 0;
            moveflag = false;
            anim.SetBool("Dash", false);
        }
        if (!backflag||!flontflag)
        {
            moveflag = false;
        }

        
        if ((Input.GetKey(KeyCode.D)||Input.GetAxis("Horizontal") >0.8f)&&backflag && (changeflag == 0 || changeflag == 1))
        {
            if(!flontflag)
            {
                flontflag = true;
            }
            changeflag = 0;
            moveflag = true;
            anim.SetBool("Dash", true);
        }
        else
        if ((Input.GetKey(KeyCode.A) || Input.GetAxis("Horizontal") <-0.8f) && flontflag&&(changeflag==0||changeflag==1))
        {
            if (!backflag)
            {
                backflag = true;
            }
            changeflag = 1;
            moveflag = true;
            anim.SetBool("Dash", true);

        }
        if(transform.position.x+speed*2<(-map.width+1)|| transform.position.x+-speed*2 > 0)
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
        if(moveflag)
        {
            if(changeflag==0)
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
    }

    void Damage()
    {
        if(damageflag)
        {
            damagetime -= Time.deltaTime;
        }
        if(damagetime<0)
        {
            hp--;
            Destroy(hps[hpcount]);
            hpcount++;
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
            damageflag = true;   
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
