using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BlockType
{
    Red,
    Blue,
    Green,
    Hedro,
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
    public ParticleSystem syuwa,syuwawa;

    private bool moveendflag;

    public bool fallflag, noneflag, rightflag, leftflag;
    private int savehight;
    public Rigidbody rigidbody;

    private Manager manager;
    //private Material mat;

    public AudioClip fall;
    private AudioSource source;
    private bool particlflag;

    public bool effectflag;
    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();

        //mat.color = new Color(mat.color.r, mat.color.g, mat.color.b,0);
        manager = GameObject.Find("Manager").gameObject.GetComponent<Manager>();
        quaternion = this.transform.rotation;
        savequaternion = this.transform.rotation;
        blocks = new Dictionary<int, Block>();
        linkBlocks = new Dictionary<int, LinkBlock>();
        color = gameObject.transform.GetComponent<Renderer>();
        savecolor = color;
        if (block == BlockType.Red)
        {
            sethp = manager.nomalHp;
            if (hp <= sethp)
            {
                hp = sethp;
            }
        }
        if (block == BlockType.Blue)
        {
            sethp = manager.FireHp;
            if (hp <= sethp)
            {
                hp = sethp;
            }
        }
        if (block == BlockType.Green)
        {
            sethp = manager.FishHp;
            if (hp <= sethp)
            {
                hp = sethp;
            }
        }
        if (block == BlockType.Hedro)
        {
            sethp = manager.EtcHp;
            if (hp <= sethp)
            {
                hp = sethp;
            }
        }
        Maxhp = hp;
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
        }

        //if (block == BlockType.Nomal)
        //{
        //    color.material.color = Color.green;
        //}
        //else
        //{
        //    color.material.color = Color.yellow;
        //}
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        Damage();

    }
 

    void Damage()
    {
 
        if (damageflag)
        {
            if(particlflag)
            {
                particlflag = false;
                if(effectflag)
                {
                    syuwa.Play();
                    syuwawa.Play();
                }
            }
        }



            if (damageflag)
            {
                hp -= Time.deltaTime;
                bubbletime += Time.deltaTime;

            }

        if (hp < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
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
