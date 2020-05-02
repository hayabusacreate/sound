using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreate : MonoBehaviour
{
    public int inblock,halfblock, outblock;
    public GameObject[] maps;
    public Dictionary<int, bool> inmap,halfmap, outmap;
    public Dictionary<int, Block> intype,halftype, outtype;
    private Dictionary<int, bool> rout;
    private Player player;
    private bool endflag;
    private SceneChange sceneChange;
    // Start is called before the first frame update
    void Start()
    {
        sceneChange = GameObject.Find("SceneChange").gameObject.GetComponent<SceneChange>();

        inmap = new Dictionary<int, bool>();
        halfmap = new Dictionary<int, bool>();
        outmap = new Dictionary<int, bool>();

        intype = new Dictionary<int, Block>();
        halftype = new Dictionary<int, Block>();
        outtype = new Dictionary<int, Block>();

        rout = new Dictionary<int, bool>();
        for (int i = 0; i < inblock + 1; i++)
        {
            inmap.Add(i, false);
        }
        for (int i = 0; i < halfblock + 1; i++)
        {
            halfmap.Add(i, false);
        }
        for (int i = 0; i < outblock + 1; i++)
        {
            outmap.Add(i, false);
        }
        for (int i = 1; i < maps.Length; i++)
        {

            maps[i].GetComponent<Map>().hight = i;
            rout.Add(i, false);
            GameObject gameObject = Instantiate(maps[i], new Vector3(transform.position.x, i * (-5), transform.position.z), Quaternion.identity);
            //gameObject.transform.rotation = Quaternion.Euler(0, 109, 0);
            //gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        player = GameObject.Find("Player").GetComponent<Player>();

        for(int y=-100;y<0;y++)
        {
            for (int x =0; x <inblock+1; x++)
            {
                inmap.Add(y * 100 + x, false);
            }
            for (int z = 0; z<outblock+1; z++)
            {
                outmap.Add(y * 100 + z, false);
            }
            for (int v = 0; v < halfblock + 1; v++)
            {
                halfmap.Add(y * 100 + v, false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int y = 1; y < maps.Length; y++)
        {
            for (int x = 0; x < inblock-1; x++)
            {
                if (!inmap[y * 100 + x])
                {
                    if (x + 1 < inblock)
                    {
                        if (!inmap[(y-1) * 100 + x])
                        {
                            rout[y] = true;
                        }
                    }
                    else
                    {
                        if (!inmap[(y-1) * 100])
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
                        if (!outmap[(y-1) * 100 + z])
                        {
                            rout[y] = true;
                        }
                    }
                    else
                    {
                        if (!outmap[(y-1) * 100])
                        {
                            rout[y] = true;
                        }
                    }

                }

            }
            for (int v = 0; v < halfblock; v++)
            {
                if (!halfmap[y * 100 + v])
                {

                    if (v + 1 < halfblock)
                    {
                        if (!halfmap[(y - 1) * 100 + v])
                        {
                            rout[y] = true;
                        }
                    }
                    else
                    {
                        if (!halfmap[(y - 1) * 100])
                        {
                            rout[y] = true;
                        }
                    }

                }

            }
        }
        for (int i = 1; i <= rout.Count; i++)
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
            sceneChange.clearflag = true;
        }else
        {
            sceneChange.clearflag = false;
        }
    }
}
