using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum BlockType
{
    Null,
    Nomal,
    Goal,
    NonBreak,
    Ice
}
public class Block : MonoBehaviour
{
    public BlockType block;
    public float hp;
    public float Maxhp;
    private bool hitflag;
    private int count;
    public bool damageflag;
    private Player player;
    private bool changeflag;
    private MapCreate mapCreate;
    public int hight, tyle;
    private Vector3 pos;
    private float z;
    private int change;
    public GameObject kati;
    public ParticleSystem syuwa, syuwawa;

    private bool moveendflag;
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
    public ParticleSystem particle;
    private bool palrticleflag;

    public GameObject enemy,hart,jet,fire,gravety;

    public int xx, yy;

    public Map map;
    private SceneChange sceneChange;

    private float damegetime;

    public GameObject pivot;

    public Collider collider;
    private ChangeCam cam;
    public Text text;

    private int savehp;
    private int savecount;
    public GameObject deathPar;
    public Material material;

    private ChangeMat mat;

    private CapsuleCollider capsule;
    private SEManager sEManager;

    public GameObject boom;
    private bool desth;
    public GameObject right, left, up, down;
    // Start is called before the first frame update
    void Start()
    {
        desth = false;
        sEManager = GameObject.Find("SEManager").GetComponent<SEManager>();
        capsule = GetComponent<CapsuleCollider>();
        mat = GetComponent<ChangeMat>();
        savehp = (int)hp;
        cam = GameObject.Find("ChangeCam").GetComponent<ChangeCam>();
        sceneChange = GameObject.Find("SceneChange").GetComponent<SceneChange>();
        mapCreate = GameObject.Find("MapCreate").GetComponent<MapCreate>();
        Maxhp = hp;
        if (type == "0")
        {
            block = BlockType.Null;
            map.maps[(yy * 1000) + xx] = false;
            Destroy(gameObject);
        }
        if (type == "2")
        {
            block = BlockType.Nomal;
        }
        if (type == "3")
        {
            block = BlockType.Goal;
            cam.endobj.transform.position = new Vector3(transform.position.x, transform.position.y, 15);
            cam.end.LookAt = transform;
            mapCreate.goalObj = gameObject;
            gameObject.GetComponent<Renderer>().material = material;
        }
        if (type == "4")
        {
            block = BlockType.NonBreak;

        }
        if (type == "5")
        {
            block = BlockType.Ice;
            Maxhp = 3;
            hp = 3;
            savehp = 3;
        }
        if (type == "6")
        {
            Instantiate(hart, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (type == "7")
        {
            Instantiate(fire, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (type == "8")
        {
            Instantiate(gravety, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (type == "9")
        {
            Instantiate(jet, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        source = gameObject.GetComponent<AudioSource>();
        //mat.color = new Color(mat.color.r, mat.color.g, mat.color.b,0);
        manager = GameObject.Find("Manager").gameObject.GetComponent<Manager>();
        count = 0;
        player = GameObject.Find("Player").gameObject.GetComponent<Player>();
        count = 0;
        savehight = hight;
        rigidbody = gameObject.GetComponent<Rigidbody>();
        change = 0;
        count = 0;
        transform.parent = null;
        palrticleflag = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(block==BlockType.Nomal)
        {
            text.text = "" + ((int)hp+1);
        }
        else
        {
            text.text = "";
        }

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
            sEManager.juu = true;
            if (savecount!=player.movecount)
            {
                savecount = player.movecount;
                savehp--;
            }
            if(hp>savehp)
            {
                hp -= Time.deltaTime*player.speed;
            }
            if(hp<=savehp)
            {
                hp = Mathf.RoundToInt(hp);
            }
            if(palrticleflag)
            {
                if(player.playerMove==PlayerMove.Down)
                {
                    down.SetActive(true);
                }
                if (player.playerMove == PlayerMove.Up)
                {
                    up.SetActive(true);
                }
                if (player.playerMove == PlayerMove.Right)
                {
                    right.SetActive(true);
                }
                if (player.playerMove == PlayerMove.Left)
                {
                    left.SetActive(true);
                }
                //particle.Play();
                palrticleflag = false;
                sEManager.ban = true;
            }
        }

        if(block==BlockType.Ice)
        {
            //hp = Mathf.RoundToInt(hp);
            if (hp<=2)
            {
                type = "2";
                block = BlockType.Nomal;
                sEManager.ice = true;
            }
        }
        if(mat.end)
        {
            //mapCreate.blocks--;
            Destroy(gameObject);
        }
        if(block==BlockType.Goal)
        {
            collider.enabled = false;
        }else
        {
            if (player.outrightroll || player.outleftroll)
            {
                collider.enabled = false;
            }
            else
            {
                collider.enabled = true;
            }
        }
        if(!sceneChange.creaflag)
        {
            if(hp<0)
            {
                if(!desth)
                {
                    mapCreate.blocks--;
                    desth = true;
                }

            }
        }
        if (hp < 0)
        {
            if(capsule.enabled)
            {
                sEManager.pon = true;
                sEManager.juu = false;
            }
            Instantiate(deathPar, transform.position, Quaternion.identity);
            map.maps[(yy * 1000) + xx] = false;
            capsule.enabled = false;
            collider.enabled = false;
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            if(block==BlockType.Nomal|| block == BlockType.Ice)
            damageflag = true;

            if(block==BlockType.Goal)
            {
                sceneChange.creaflag = true;
               player.anim.SetTrigger("Death");
            }
            savecount = player.movecount;
            //color.material.color = Color.red;
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (block == BlockType.Goal&& sceneChange.creaflag)
        {
            Instantiate(boom, transform.position, Quaternion.identity);
        }
    }
}
