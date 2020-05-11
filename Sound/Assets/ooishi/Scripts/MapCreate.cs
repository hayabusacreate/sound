using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapCreate : MonoBehaviour
{
    public int inblock,halfblock, outblock;
    public GameObject[] maps;
    private Player player;
    private bool endflag;
    private SceneChange sceneChange;
    public TextAsset csvFile;
    private List<string[]> csvDatas = new List<string[]>();
    // Start is called before the first frame update
    void Start()
    {
        //csvFile=Resources.Load("")
        StringReader reader = new StringReader(csvFile.text);

        while(reader.Peek()!=-1)
        {
            string line = reader.ReadLine();
            csvDatas.Add(line.Split(','));
        }
        sceneChange = GameObject.Find("SceneChange").gameObject.GetComponent<SceneChange>();
        player = GameObject.Find("Player").GetComponent<Player>();

        Debug.Log(csvDatas[2][2]);

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(csvDatas[3][3]);
    }
}
