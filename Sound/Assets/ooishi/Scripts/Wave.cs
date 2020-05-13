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
    public GameObject enemy;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        map = GameObject.Find("MapCreate").GetComponent<MapCreate>();
        count = 0;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time>wavetime)
        {
            time = 0;
            Instantiate(enemy, new Vector3(player.transform.position.x + 3, player.transform.position.y + 3, 0), Quaternion.identity);
        }
    }
}
