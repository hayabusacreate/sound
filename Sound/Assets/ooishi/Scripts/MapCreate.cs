using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapCreate : MonoBehaviour
{
    private Player player;
    private bool endflag;
    private SceneChange sceneChange;
    public static TextAsset[] csvFile;
    private List<string[]> csvDatas = new List<string[]>();
    public int eazy, nomal, hard;
    public int eazymap, nomalmap, hardmap;
    public int hight, width;
    public GameObject map;
    public int maphight;

    public GameObject wall;
    // Start is called before the first frame update
    void Start()
    {
        maphight = 0;

        sceneChange = GameObject.Find("SceneChange").gameObject.transform.GetComponent<SceneChange>();
        if(sceneChange.scene==Scene.GamePlay)
        {
            player = GameObject.Find("Player").GetComponent<Player>();
            Instantiate(map, new Vector3(0, 0, 0), Quaternion.identity);
            maphight++;
        }else
            if(sceneChange.scene == Scene.Load)
        {
            csvFile = new TextAsset[sceneChange.map];
        }

        //Debug.Log(csvDatas[2][2]);

    }

    // Update is called once per frame
    void Update()
    {
        if (sceneChange.scene == Scene.GamePlay)
        {
            if (maphight <= player.hight + 1)
            {
                Instantiate(map, new Vector3(0, hight * maphight, 0), Quaternion.identity);
                maphight++;
            }
        }

    }

    public TextAsset ReturnMap(int a)
    {
        return csvFile[a];
    }
    public void LoadMap(TextAsset a,int b)
    {
        csvFile[b] = a;
    }
}
