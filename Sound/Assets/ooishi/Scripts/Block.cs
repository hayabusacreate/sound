using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BlockType
{
    Null,
    Nomal
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
    private int count;
    public bool damageflag;
    private Player player;
    public bool moveflag;
    private GameObject center;
    private float radius;
    private bool changeflag;
    private MapCreate mapCreate;
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

    public string type;

    public int maphight;
    // Start is called before the first frame update
    void Start()
    {
        if(type=="0")
        {
            block = BlockType.Null;
            Destroy(gameObject);
        }
        if (type == "2")
        {
            block = BlockType.Nomal;
        }
        source = gameObject.GetComponent<AudioSource>();

        //mat.color = new Color(mat.color.r, mat.color.g, mat.color.b,0);
        manager = GameObject.Find("Manager").gameObject.GetComponent<Manager>();
        quaternion = this.transform.rotation;
        savequaternion = this.transform.rotation;
        blocks = new Dictionary<int, Block>();
        color = gameObject.transform.GetComponent<Renderer>();
        savecolor = color;
        Maxhp = hp;
        count = 0;
        center = transform.root.gameObject;
        player = GameObject.Find("Player").gameObject.GetComponent<Player>();
        count = 0;
        mapCreate = GameObject.Find("MapCreate").GetComponent<MapCreate>();

        quaternion = this.transform.rotation;
        savehight = hight;
        rigidbody = gameObject.GetComponent<Rigidbody>();
        z = Mathf.Round(quaternion.eulerAngles.y);

            moveflag = false;
            player.attackflag = false;
            change = 0;
            count = 0;
        

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
        if (maphight < player.hight - 1)
        {
            Destroy(gameObject);
        }
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Bubble")
        {
            hp -= 0.05f;
        }
    }
}
