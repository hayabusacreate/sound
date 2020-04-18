using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreate : MonoBehaviour
{
    public int inblock, outblock;
    public GameObject[] maps;
    public Dictionary<int, bool> inmap,outmap;
    public Dictionary<int, Block> intype, outtype;
    // Start is called before the first frame update
    void Start()
    {
        inmap = new Dictionary<int, bool>();
        outmap = new Dictionary<int, bool>();
        intype = new Dictionary<int, Block>();
        outtype = new Dictionary<int, Block>();
        for(int i=0;i<inblock+1;i++)
        {
            inmap.Add(i, false);
        }
        for (int i = 0; i < outblock+1; i++)
        {
            outmap.Add( i, false);
        }
        for (int i=1; i<maps.Length;i++)
        {

            maps[i].GetComponent<Map>().hight = i;
            GameObject gameObject = Instantiate(maps[i], new Vector3(transform.position.x, i * (-5), transform.position.z), new Quaternion(0, 0.3f, 0, 0));
            gameObject.transform.rotation = Quaternion.Euler(0, 109, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
