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

    public GameObject gold, silver, bronze, over,serect;
    // Start is called before the first frame update
    void Start()
    {
        if(scene==Scene.GamePlay)
        {
            slider = GameObject.Find("Count").GetComponent<Slider>();
            player=GameObject.Find("Player").GetComponent<Player>();
            mapCreate = GameObject.Find("MapCreate").GetComponent<MapCreate>();
            slider.minValue = 0;
            slider.maxValue = 100;
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
                slider.value = (((float)mapCreate.maxblock - (float)mapCreate.blocks) / (float)mapCreate.maxblock)*100;

                if(creaflag)
                {
                    if((((float)mapCreate.maxblock - (float)mapCreate.blocks) / (float)mapCreate.maxblock) * 100 < 50)
                    {
                        over.SetActive(true);
                    }
                    else if((((float)mapCreate.maxblock - (float)mapCreate.blocks) / (float)mapCreate.maxblock) * 100 < 75)
                    {
                        bronze.SetActive(true);
                    }
                    else if ((((float)mapCreate.maxblock - (float)mapCreate.blocks) / (float)mapCreate.maxblock) * 100 < 100)
                    {
                        silver.SetActive(true);
                    }
                    else
                    {
                        gold.SetActive(true);
                    }
                    serect.SetActive(true);
                    if(Input.GetKeyDown(KeyCode.Space) || (Input.GetKeyDown("joystick button 0")))
                    {
                        SceneManager.LoadScene("StageSerect");
                    }

                }
                if (Input.GetKeyDown(KeyCode.Space) || (Input.GetKeyDown("joystick button 7")))
                {
                    SceneManager.LoadScene("StageSerect");
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
                if (Input.GetKeyDown(KeyCode.D) || (Input.GetKeyDown("joystick button 5")))
                {
                    mapnum++;

                    if(mapnum>mapcount)
                    {
                        mapnum = mapcount;
                    }
                    mapCreate.ChangeMap(mapnum);
                }
                if (Input.GetKeyDown(KeyCode.A) || (Input.GetKeyDown("joystick button 4")))
                {
                    mapnum--;
                    if(mapnum<1)
                    {
                        mapnum = 1;
                    }
                    mapCreate.ChangeMap(mapnum);
                }
                if (Input.GetKey(KeyCode.Space) || (Input.GetKeyDown("joystick button 0")))
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
