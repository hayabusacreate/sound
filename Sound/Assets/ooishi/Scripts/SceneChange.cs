using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public enum Scene
{
    Title,
    GamePlay,
    StageSelect,
    GameOver,
    GameCrear,
    Load
}

public class SceneChange : MonoBehaviour
{
    public Scene scene;
    private Player player;
    public float time;
    private Text timeText;
    private Slider slider;
    public int map;
    public MapCreate mapCreate;
    public bool creaflag;
    public Text text;

    private int mapnum;

    public int mapcount;

    public bool endflag;
    // Start is called before the first frame update
    void Start()
    {
        if(scene==Scene.GamePlay)
        {
            player=GameObject.Find("Player").GetComponent<Player>();
            timeText = GameObject.Find("Time").GetComponent<Text>();
            mapCreate = GameObject.Find("MapCreate").GetComponent<MapCreate>();
            // 
            //Physics.gravity = new Vector3(0, -5, 0);
        }
        if (scene == Scene.Load)
        {
            mapCreate = GameObject.Find("MapCreate").GetComponent<MapCreate>();
        }
        if (scene == Scene.StageSelect)
        {
            mapnum = 1;
            mapCreate = GameObject.Find("MapCreate").GetComponent<MapCreate>();
            mapCreate.ChangeMap(mapnum);

        }
    }

    // Update is called once per frame
    void Update()
    {
        Change();
    }

    void Change()
    {
        switch (scene)
        {
            case Scene.Title:

                break;
            case Scene.GamePlay:
                //time -= Time.deltaTime;
                //slider.value = time;
                if(creaflag)
                {
                    if(mapCreate.blocks/mapCreate.maxblock*100<50)
                    {

                    }else if(mapCreate.blocks / mapCreate.maxblock * 100 < 75)
                    {

                    }
                    else if (mapCreate.blocks / mapCreate.maxblock * 100 < 100)
                    {

                    }else
                    {

                    }
                    SceneManager.LoadScene("Stage" + mapCreate.ReturnMapnum());
                }
                if (player.endflag)
                {
                    SceneManager.LoadScene("GameOver");
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    
                    SceneManager.LoadScene("Stage" + mapCreate.ReturnMapnum());
                }
                if(endflag)
                {
                    SceneManager.LoadScene("Stage" + mapCreate.ReturnMapnum());
                }
                break;
            case Scene.StageSelect:
                text.text = "" + mapnum;
                if (Input.GetKeyDown(KeyCode.D))
                {
                    mapnum++;

                    if(mapnum>mapcount)
                    {
                        mapnum = mapcount;
                    }
                    mapCreate.ChangeMap(mapnum);
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    mapnum--;
                    if(mapnum<1)
                    {
                        mapnum = 1;
                    }
                    mapCreate.ChangeMap(mapnum);
                }
                if (Input.GetKey(KeyCode.Space))
                {
                    SceneManager.LoadScene("Stage" + mapCreate.ReturnMapnum());
                }
                break;
            case Scene.GameOver:
                if (Input.GetKeyDown(KeyCode.R))
                {

                    SceneManager.LoadScene("Stage" + mapCreate.ReturnMapnum());
                }
                break;
            case Scene.GameCrear:
                if (Input.GetKeyDown(KeyCode.R))
                {

                    SceneManager.LoadScene("Stage" + mapCreate.ReturnMapnum());
                }
                break;
            case Scene.Load:
                for(int i=0;i<map;i++)
                {
                    mapCreate.LoadMap(Resources.Load("map"+i) as TextAsset,i);
                }
                SceneManager.LoadScene("StageSerect");
                break;
        }

    }
}
