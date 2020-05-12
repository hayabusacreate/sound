using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    public float speed;

    public float hp;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
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
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag==("Block"))
        {
            if(collision.gameObject.GetComponent<Block>().damageflag)
            {
                hp -= 0.01f;
            }
        }
    }
}
