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
    // Start is called before the first frame update
    void Start()
    {
        Maxhp = hp;
        if (type == "0")
        {
            block = BlockType.Null;
            Destroy(gameObject);
        }
        if (type == "2")
        {
            block = BlockType.Nomal;
        }
        if (type == "3")
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
            Destroy(gameObject);
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
        mapCreate = GameObject.Find("MapCreate").GetComponent<MapCreate>();
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
            hp -= Time.deltaTime;
            if(palrticleflag)
            {
                particle.Play();
                palrticleflag = false;
            }

        }

        if (hp < 0)
        {
            if (player.skill==PlayerSkill.Fire)
            {

            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        //if (collision.transform.tag == "Player")
        //{
        //    damageflag = true;
        //    //color.material.color = Color.red;
        //}
    }
}
