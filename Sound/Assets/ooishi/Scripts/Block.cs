using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BlockType
{
    Nomal,
    Fire
}
public class Block : MonoBehaviour
{
    public BlockType block;
    public int hp;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Move()
    {

    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.transform.tag=="Player")
        {
            hp--;
            if(hp<0)
            {
                Destroy(gameObject);
            }
        }
    }
}
