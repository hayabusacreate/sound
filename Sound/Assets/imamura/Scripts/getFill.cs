using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getFill : MonoBehaviour
{

    public Image cooldown;

    public float value;
    public float a = 0;

    public float speed = 1f;

    public MapCreate mapC;

    // Start is called before the first frame update
    void Start()
    {
        cooldown.material.SetFloat("_Ratio", 0);
        value= 0;
        a = 0;
        mapC = GameObject.Find("MapCreate").GetComponent<MapCreate>();
    }

    // Update is called once per frame
    void Update()
    {
        if (value < (1 - ((float)mapC.blocks) / (float)mapC.maxblock) && mapC.maxblock - mapC.blocks != 0)
        {
            value += Time.deltaTime * speed;
        }
        cooldown.material.SetFloat("_Ratio", (Mathf.Floor(value * 100) / 100));
    }
}
