using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapCreate : MonoBehaviour
{
    private Player player;
    private bool endflag;
    private SceneChange sceneChange;
    public TextAsset[] csvFile;
    private List<string[]> csvDatas = new List<string[]>();
    public int eazy, nomal, hard;
    public int eazymap, nomalmap, hardmap;
    public int hight, width;
    public GameObject map;
    public int maphight;
    // Start is called before the first frame update
    void Start()
    {
        maphight = 0;
        player = GameObject.Find("Player").GetComponent<Player>();
        sceneChange = GameObject.Find("SceneChange").gameObject.GetComponent<SceneChange>();

        Instantiate(map, new Vector3(0, 0, 0), Quaternion.identity);
        maphight++;
        //Debug.Log(csvDatas[2][2]);

    }

    // Update is called once per frame
    void Update()
    {
        if(maphight<=player.hight+1)
        {
            Instantiate(map, new Vector3(0, hight * maphight, 0), Quaternion.identity);
            maphight++;
        }
    }
}
