﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public int hight;
    private int intyle, outtyle;
    private MapCreate map;
    private bool check;
    // Start is called before the first frame update
    void Start()
    {
        check = false;
        map = GameObject.Find("MapCreate").GetComponent<MapCreate>();
        intyle = 0;
        outtyle = 0;
        foreach (Transform child in transform)
        {
            child.GetComponent<Block>().hight = hight;
            if (child.GetComponent<Block>().inout == InOut.In)
            {
                map.inmap.Add(hight * 100 + intyle, true);
                child.GetComponent<Block>().tyle = intyle;
                intyle++;
            }
            else
            {
                map.outmap.Add(hight * 100 + outtyle, true);
                child.GetComponent<Block>().tyle = outtyle;
                outtyle++;
            }

        }
        //transform.DetachChildren();
    }
    // Update is called once per frame
    void Update()
    {

    }
}
