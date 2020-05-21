using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Nomal,
    Block,
    Sneak
}

public class Enemy : MonoBehaviour
{
    private GameObject player;
    public float speed;

    public float hp;

    private bool damageflag;

    private Renderer renderer;

    private float renge;

    public GameObject dethpa;
    public ParticleSystem damegeefe;

    public EnemyType type;
    // Start is called before the first frame update
    void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(damageflag)
        {
            hp -= Time.deltaTime;
        }
        if(hp<0)
        {
            Instantiate(dethpa, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        Move();
    }

    void Move()
    {
        if(type==EnemyType.Nomal||type==EnemyType.Block)
        {
            if (transform.position.x < player.transform.position.x)
            {
                transform.position += new Vector3(speed, 0, 0);
            }
            else if (transform.position.x > player.transform.position.x)
            {
                transform.position -= new Vector3(speed, 0, 0);
            }
        }

        //Debug.Log(renge);

    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag==("Block"))
        {
            if(collision.gameObject.GetComponent<Block>().damageflag)
            {
                damageflag = true;
                damegeefe.Play();
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == ("Block"))
        {
            damageflag = false;
            damegeefe.Stop();
        }
    }
}
