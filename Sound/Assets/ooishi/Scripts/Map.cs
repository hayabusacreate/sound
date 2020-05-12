using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Map : MonoBehaviour
{
    private int hight,width;
    private MapCreate map;
    private bool check;
    public GameObject block;
    private List<string[]> csvDatas = new List<string[]>();
    private int mapnum;
    private int maphight;
    // Start is called before the first frame update
    void Start()
    {

        map = GameObject.Find("MapCreate").GetComponent<MapCreate>();
        mapnum = Random.Range(0, map.csvFile.Length);
        StringReader reader = new StringReader(map.csvFile[mapnum].text);
        width = map.width;
        hight = map.hight;
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            csvDatas.Add(line.Split(','));
        }
        check = false;


        for(int y=0;y<hight;y++)
        {
            for(int x=0;x<width; x++)
            {
                GameObject gameObject = Instantiate(block, new Vector3(-x,-transform.position.y-y, 0), Quaternion.identity);
                gameObject.GetComponent<Block>().type = csvDatas[y][x];
                gameObject.GetComponent<Block>().maphight = map.maphight;
            }
        }


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
