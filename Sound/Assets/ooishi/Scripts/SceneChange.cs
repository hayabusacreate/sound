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

    public AudioClip sound1;
    AudioSource audio;

    private bool ui;
    private float count;
    public GameObject endobj;
    public GameObject[] stages;
    public GameObject[] scs;
    private int stagenum;
    public GameObject pl;
    private float scale;
    private bool inout;
    // Start is called before the first frame update
    void Start()
    {
        inout = true;
        audio = GetComponent<AudioSource>();
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
            stagenum = 1;
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
                if(Input.GetKeyDown(KeyCode.I))
                {
                    ui = true;
                }
                if(ui)
                {
                    over.SetActive(false);
                    bronze.SetActive(false);
                    silver.SetActive(false);
                    gold.SetActive(false);
                    serect.SetActive(false);
                }
                if (Input.GetKeyDown(KeyCode.Space) || (Input.GetKeyDown("joystick button 7")))
                {
                    SceneManager.LoadScene("StageSerect");
                }
                if (player.endflag)
                {
                    SceneManager.LoadScene("GameOver");
                }
                if (Input.GetKeyDown(KeyCode.R) || (Input.GetKeyDown("joystick button 3")))
                {
                    
                    SceneManager.LoadScene("Stage" + mapCreate.ReturnMapnum());
                }
                if(endflag)
                {
                    count++;
                    endobj.SetActive(true);
                }
                if(count>60)
                {
                    SceneManager.LoadScene("Stage" + mapCreate.ReturnMapnum());
                }
                break;
            case Scene.StageSelect:
                text.text = "" + mapnum;
                if (inout)
                {
                    if (scale <= 1)
                    {
                        scale += 10*Time.deltaTime;
                    }
                    scs[mapnum].transform.localScale = new Vector3(scale, scale, scale);
                }
                else
                {
                    if (scale >= 0)
                    {
                        scale -= 10 * Time.deltaTime;
                    }
                    if (mapnum < stagenum)
                    {
                        scs[mapnum+1].transform.localScale = new Vector3(scale, scale, scale);
                    }
                    else
                    {
                        scs[mapnum-1].transform.localScale = new Vector3(scale, scale, scale);
                    }

                }
                if (stagenum==mapnum)
                {


                    if (Input.GetKeyDown(KeyCode.D) || (Input.GetKeyDown("joystick button 5")))
                    {
                        mapnum++;
                        inout = false;
                        if (mapnum > mapcount)
                        {
                            mapnum = mapcount;
                        }
                        mapCreate.ChangeMap(mapnum);
                    }
                    if (Input.GetKeyDown(KeyCode.A) || (Input.GetKeyDown("joystick button 4")))
                    {
                        inout = false;
                        mapnum--;
                        if (mapnum < 1)
                        {
                            mapnum = 1;
                        }
                        mapCreate.ChangeMap(mapnum);
                    }
                }else
                {
                    if(mapnum<stagenum)
                    {
                        pl.transform.position = Vector3.Lerp(pl.transform.position, stages[mapnum].transform.position, 0.1f);
                        if(pl.transform.position.x-0.1f<= stages[mapnum].transform.position.x)
                        {
                            inout = true;
                            stagenum =mapnum;
                        }
                    }else
                    {
                        pl.transform.position = Vector3.Lerp(pl.transform.position, stages[mapnum].transform.position, 0.1f);
                        if (pl.transform.position.x + 0.1f >= stages[mapnum].transform.position.x)
                        {
                            inout = true;
                            stagenum = mapnum;
                        }
                    }
                }

                if (Input.GetKey(KeyCode.Space) || (Input.GetKeyDown("joystick button 0")))
                {
                    SceneManager.LoadScene("Stage" + mapCreate.ReturnMapnum());
                    audio.PlayOneShot(sound1);
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
