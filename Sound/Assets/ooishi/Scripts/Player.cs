using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed, maxspeed, setspeed;
    public float radius;
    private Vector3 defPosition,vec;
    private float x;
    private float z;
    private float x2, z2;
    public GameObject center;
    private float time;
    public int changeflag;
    private Rigidbody rigidbody;
    private float angle;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        defPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        radius = Vector3.Distance(transform.position, center.transform.position);
        if (Input.GetKey(KeyCode.W))
        {
            changeflag = 0;
        }
        else
        if (Input.GetKey(KeyCode.S))
        {
            changeflag = 1;
        }else
        if(Input.GetKeyDown(KeyCode.D))
        {
            changeflag = 2;
        }else
        if(Input.GetKeyDown(KeyCode.A))
        {
            changeflag = 3;
        }
        if (changeflag==0)
        {
            //RotateAround(円運動の中心,進行方向,速度)
            transform.RotateAround(center.transform.position,
            transform.forward, speed/radius);
        }
        else if(changeflag==1)
        {
            //RotateAround(円運動の中心,進行方向,速度)
            transform.RotateAround(center.transform.position,
            -transform.forward,speed/radius);
        }else if (changeflag == 2)
        {
            radius = 1;
        }
        else if (changeflag == 3)
        {
            radius = 2;
        }


        if (changeflag == 2 || changeflag == 3)
        {
            defPosition =center.transform.position - transform.position;
            angle = Vector3.Angle(defPosition, -transform.up);
            if(changeflag==2)
            {
                //movex(Sin波）・moveZ座標(Cos波）の指定をしておく。わからないときは三角関数を調べる。
                x = radius * Mathf.Sin(angle * Mathf.Deg2Rad);
                z = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            }
            if(changeflag==3)
            {
                //movex(Sin波）・moveZ座標(Cos波）の指定をしておく。わからないときは三角関数を調べる。
                x = radius * Mathf.Sin(-angle*Mathf.Deg2Rad);
                z = radius * Mathf.Cos(-angle * Mathf.Deg2Rad);
            }

            transform.position = Vector3.Lerp(transform.position, new Vector3(x, transform.position.y, z), 1);
            if (transform.position == new Vector3(x, transform.position.y, z))
            {
                changeflag = 0;
            }
        }
    }
}
