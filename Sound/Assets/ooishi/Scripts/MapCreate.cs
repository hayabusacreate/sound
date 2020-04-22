using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreate : MonoBehaviour
{
    public int inblock, outblock;
    public GameObject[] maps;
    public Dictionary<int, bool> inmap, outmap;
    public Dictionary<int, Block> intype, outtype;
    private Dictionary<int, bool> rout;
    private Player player;
    private bool endflag;
    // Start is called before the first frame update
    void Start()
    {
        inmap = new Dictionary<int, bool>();
        outmap = new Dictionary<int, bool>();
        intype = new Dictionary<int, Block>();
        outtype = new Dictionary<int, Block>();
        rout = new Dictionary<int, bool>();
        for (int i = 0; i < inblock + 1; i++)
        {
            inmap.Add(i, false);
        }
        for (int i = 0; i < outblock + 1; i++)
        {
            outmap.Add(i, false);
        }
        for (int i = 1; i < maps.Length; i++)
        {

            maps[i].GetComponent<Map>().hight = i;
            rout[i] = false;
            GameObject gameObject = Instantiate(maps[i], new Vector3(transform.position.x, i * (-5), transform.position.z), new Quaternion(0, 0.3f, 0, 0));
            gameObject.transform.rotation = Quaternion.Euler(0, 109, 0);
        }
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int y = 0; y < maps.Length; y++)
        {
            for (int x = 0; x < inblock; x++)
            {
                if (!inmap[y * 100 + x])
                {
                    if (x + 1 < inblock)
                    {
                        if (!inmap[y * 100 + x + 1])
                        {
                            rout[y] = true;
                        }
                    }
                    else
                    {
                        if (!inmap[y * 100])
                        {
                            rout[y] = true;
                        }
                    }

                }
            }
            for (int z = 0; z < outblock; z++)
            {
                if (!outmap[y * 100 + z])
                {

                    if (z + 1 < outblock)
                    {
                        if (!outmap[y * 100 + z + 1])
                        {
                            rout[y] = true;
                        }
                    }
                    else
                    {
                        if (!outmap[y * 100])
                        {
                            rout[y] = true;
                        }
                    }

                }

            }
        }
        for (int i = 0; i < rout.Count; i++)
        {
            if (rout[i])
            {
                endflag = true;
            }
            else
            {
                endflag = false;
            }
        }

        if (endflag)
        {
            player.endflag = true;
        }
    }
}
