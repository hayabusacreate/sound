using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{
    Nomal,
    Fire
}
public class Player : MonoBehaviour
{
    public float speed;
    private float radius;
    public float inradius,outradius;
    private Vector3 defPosition;
    private float x;
    private float z;
    public GameObject center,inobj,outobj;
    private Vector3 invec, outvec;
    private int changeflag,saveflag,ren;
    private Rigidbody rigidbody;
    public float jumppower;
    private bool jumpflag;
    public PlayerType type;
    private Block block;
    // Start is called before the first frame update
    void Start()
    {
        radius = inradius;
        defPosition = transform.position;
        changeflag = 0;
        saveflag = 0;
        rigidbody = gameObject.transform.GetComponent<Rigidbody>();
        ren = 0;
        type = PlayerType.Nomal;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }
    void Move()
    {
        radius = Vector3.Distance(transform.position, center.transform.position);
        if (Input.GetKey(KeyCode.D))
        {
            changeflag = 0;
        }
        else
        if (Input.GetKey(KeyCode.A))
        {
            changeflag = 1;

        }
        else
        if (Input.GetKeyDown(KeyCode.W)&&ren!=0)
        {
            changeflag = 2;
        }
        else
        if (Input.GetKeyDown(KeyCode.S)&&ren!=1)
        {
            changeflag = 3;
        }
        if (changeflag == 0)
        {
            //RotateAround(円運動の中心,進行方向,速度)
            transform.RotateAround(center.transform.position,
            transform.forward, speed / radius);
        }
        else if (changeflag == 1)
        {
            //RotateAround(円運動の中心,進行方向,速度)
            transform.RotateAround(center.transform.position,
            -transform.forward, speed / radius);
        }
        else if (changeflag == 2)
        {
            radius = inradius;
        }
        else if (changeflag == 3)
        {
            radius = outradius;
        }

        if(changeflag==0||changeflag==1)
        {
            invec = inobj.transform.position;
            outvec = outobj.transform.position;
            saveflag = changeflag;
        }else
        {
            if(saveflag==0)
            {
                if(changeflag==2)
                {
                    transform.position = Vector3.Lerp(transform.position, invec, 0.1f);
                    if ((transform.position.x > invec.x&& transform.position.x <= invec.x + 0.1f)
                        ||(transform.position.x < invec.x&& transform.position.x >= invec.x - 0.1f))
                    {
                        transform.position = invec;
                        changeflag = 0;
                        ren = 0;
                    }
                }
                else
                {
                    transform.position = Vector3.Lerp(transform.position, outvec, 0.1f);
                    if ((transform.position.x > outvec.x && transform.position.x <= outvec.x + 0.1f)
                        || (transform.position.x < outvec.x && transform.position.x >= outvec.x - 0.1f))
                    {
                        transform.position = outvec;
                        changeflag = 0;
                        ren = 1;
                    }
                }
            }else
            {
                if (changeflag == 2)
                {
                    transform.position = Vector3.Lerp(transform.position, invec, 0.1f);
                    if ((transform.position.x > invec.x && transform.position.x <= invec.x + 0.1f)
                        || (transform.position.x < invec.x && transform.position.x >= invec.x - 0.1f))
                    {
                        transform.position = invec;
                        changeflag = 1;
                        ren = 0;
                    }
                }
                else
                {
                    transform.position = Vector3.Lerp(transform.position, outvec, 0.1f);
                    if ((transform.position.x > outvec.x && transform.position.x <= outvec.x + 0.1f)
                        || (transform.position.x < outvec.x && transform.position.x >= outvec.x - 0.1f))
                    {
                        transform.position = outvec;
                        changeflag = 1;
                        ren = 1;
                    }
                }
            }
        }
    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space)&&!jumpflag)
        {
            rigidbody.velocity += new Vector3(0, jumppower, 0);
            jumpflag = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(jumpflag)
        {
            jumpflag = false;
        }
        if(collision.gameObject.tag=="Block")
        {
            if(type==PlayerType.Nomal&&collision.gameObject.GetComponent<Block>().block==BlockType.Fire)
            {
                type = PlayerType.Fire;
                block.hp++;
            }
            if (type == PlayerType.Fire && collision.gameObject.GetComponent<Block>().block == BlockType.Nomal)
            {
                type = PlayerType.Nomal;
                block.hp++;
            }
            block = collision.gameObject.GetComponent<Block>();
        }
    }
}
