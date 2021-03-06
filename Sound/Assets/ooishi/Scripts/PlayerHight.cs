﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHight : MonoBehaviour
{
    private Player player;
    private MapCreate map;
    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.transform.root.gameObject.GetComponent<Player>();
        map = GameObject.Find("MapCreate").gameObject.GetComponent<MapCreate>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 1, player.transform.position.z);
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag=="Ground")
        {
        }
        if (other.gameObject.tag == "Block")
        {
            //player.hight = other.gameObject.GetComponent<Block>().hight - 1;
            player.tyle = other.gameObject.GetComponent<Block>().tyle;
        }
    }
}
