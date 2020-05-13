using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    public float speed;

    public float hp;

    private bool damageflag;

    private Renderer renderer;
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
            renderer.material.color = Color.red;
            hp -= Time.deltaTime;
        }else
        {
            renderer.material.color = Color.white;
        }
        if(hp<0)
        {
            Destroy(gameObject);
        }
        Move();
    }

    void Move()
    {
        if(transform.position.x<player.transform.position.x)
        {
            transform.position += new Vector3(speed, 0, 0);
        }else if(transform.position.x > player.transform.position.x)
        {
            transform.position -= new Vector3(speed, 0, 0);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag==("Block"))
        {
            if(collision.gameObject.GetComponent<Block>().damageflag)
            {
                damageflag = true;
            }else
            {
                damageflag = false;
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == ("Block"))
        {
            damageflag = false;
        }
    }
}
