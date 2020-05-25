using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BlockType
{
    Null,
    Nomal,
    Goal
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

    public int x, y;

    public Map map;
    private SceneChange sceneChange;

    private float damegetime;

    public GameObject pivot;

    public Collider collider;
    private ChangeCam cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("ChangeCam").GetComponent<ChangeCam>();
        sceneChange = GameObject.Find("SceneChange").GetComponent<SceneChange>();
        mapCreate = GameObject.Find("MapCreate").GetComponent<MapCreate>();
        Maxhp = hp;
        if (type == "0")
        {
            block = BlockType.Null;
            map.maps[(y * 1000) + x] = false;
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
        }
        if (type == "4")
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (type == "5")
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
            Destroy(gameObject);
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


        //if (block == BlockType.Nomal)
        //{
        //    color.material.color = Color.green;
        //}
        //else
        //{
        //    color.material.color = Color.yellow;
        //}
        transform.parent = null;
        palrticleflag = true;
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
            if(player.moveflag)
            {
                hp -= player.speed;
            }else
            {
                hp = Mathf.RoundToInt(hp);
            }


            if(palrticleflag)
            {
                particle.Play();
                palrticleflag = false;
            }

        }

        if (hp < 0)
        {
            map.maps[(y * 1000) + x] = false;
            mapCreate.blocks--;
            Destroy(gameObject);
        }
        if(player.outrightroll||player.outleftroll)
        {
            collider.enabled = false;
        }else
        {
            collider.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            damageflag = true;
            if(block==BlockType.Goal)
            {
                sceneChange.creaflag = true;
            }
            //color.material.color = Color.red;
        }
    }
}
