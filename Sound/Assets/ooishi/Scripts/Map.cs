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
    private int mapchoice;
    private StringReader reader;
    private Player player;

    public GameObject wall;

    public Dictionary<int, bool> maps;
    public Dictionary<int, GameObject> maplog;
    // Start is called before the first frame update
    void Start()
    {
        maps = new Dictionary<int, bool>();
        maplog = new Dictionary<int, GameObject>();
        map = GameObject.Find("MapCreate").GetComponent<MapCreate>();
        if(map.maphight<map.eazy)
        {
            mapnum = Random.Range(0, map.eazymap);
            reader = new StringReader(map.ReturnMap(mapnum).text);
        }
        else if(map.maphight < map.nomal)
        {
            mapnum = Random.Range(0, map.nomalmap);
             reader = new StringReader(map.ReturnMap(mapnum).text);
        }
        else
        {
            mapnum = Random.Range(0, map.hardmap);
             reader = new StringReader(map.ReturnMap(mapnum).text);
        }


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
                gameObject.GetComponent<Block>().x = x;
                gameObject.GetComponent<Block>().y = y;
                maps[(y * 1000) + x] = true;
                maplog[y * 1000 + x] = gameObject;

                gameObject.GetComponent<Block>().map = this.gameObject.GetComponent<Map>();
            }
            Instantiate(wall, new Vector3(1, -transform.position.y - y, 0), Quaternion.identity);
            Instantiate(wall, new Vector3(-width, -transform.position.y - y, 0), Quaternion.identity);
        }
        //Destroy(gameObject);
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
