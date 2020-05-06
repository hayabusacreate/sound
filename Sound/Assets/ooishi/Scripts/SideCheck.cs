using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum LR
{
    Right,
    Left
}

public class SideCheck : MonoBehaviour
{
    public LR lr;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.transform.root.gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag=="Block")
        {
            if(lr==LR.Right)
            {
                player.rightlfag = false;
            }else
            {
                player.leftflag = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Block")
        {
            if (lr == LR.Right)
            {
                player.rightlfag = true;
            }
            else
            {
                player.leftflag = true;
            }
        }
    }
}
