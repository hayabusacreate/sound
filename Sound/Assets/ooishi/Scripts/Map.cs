using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public int hight;
    private int intyle,halftyle, outtyle;
    private MapCreate map;
    private bool check;
    // Start is called before the first frame update
    void Start()
    {
        check = false;
        map = GameObject.Find("MapCreate").GetComponent<MapCreate>();
        intyle = 0;
        outtyle = 0;
        halftyle = 0;

    }

    private void FixedUpdate()
    {
        //transform.DetachChildren();
    }
    // Update is called once per frame
    void Update()
    {

    }
}
