using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public GameObject[] wave;
    public int[] tyle;
    public int wavetime;
    private float time;
    private int count;
    private MapCreate map;
    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.Find("MapCreate").GetComponent<MapCreate>();
        count = 0;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time>wavetime&&wave.Length>count)
        {
            GameObject gameObject= Instantiate(wave[count], transform.position, Quaternion.identity);
            gameObject.transform.rotation = Quaternion.Euler(0, (360 / map.inblock) * tyle[count],0);
            gameObject.GetComponent<Block>().tyle = tyle[count];
            gameObject.GetComponent<Block>().hight = -count-1;
            count++;
            time = 0;
        }
    }
}
