using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BF
{
    Flont,
    Back,
    Frontdown,
    Backdown
}

public class BFCheck : MonoBehaviour
{
    public BF bf;
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
            //Debug.Log("a");
            if(bf==BF.Flont)
            {
                player.flontflag = false;
            }else if (bf == BF.Back)
            {
                player.backflag = false;
            }
            else if (bf == BF.Backdown)
            {
                player.backflag = false;
            }
            else if (bf == BF.Frontdown)
            {
                player.flontflag = false;
            }
        }
        if(other.gameObject.tag == "Wall")
        {
            if (bf == BF.Flont)
            {
                player.flontflag = false;
            }
            else if (bf == BF.Back)
            {
                player.backflag = false;
            }
            else if (bf == BF.Backdown)
            {
                player.backflag = false;
            }
            else if (bf == BF.Frontdown)
            {
                player.flontflag = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.tag == "Block")
        //{
        //    if (bf == BF.Flont)
        //    {
        //        player.flontflag = true;
        //    }
        //    else
        //    {
        //        player.backflag = true;
        //    }
        //}
    }
}
