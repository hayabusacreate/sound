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
    private float time;
    private Text timeText;
    private Slider slider;
    public int map;
    public MapCreate mapCreate;
    // Start is called before the first frame update
    void Start()
    {
        if(scene==Scene.GamePlay)
        {
            player=GameObject.Find("Player").GetComponent<Player>();
            timeText = GameObject.Find("Time").GetComponent<Text>();
            slider = GameObject.Find("Slider").GetComponent<Slider>();
            // 
            //Physics.gravity = new Vector3(0, -5, 0);
            slider.maxValue = time;
        }
        if (scene == Scene.Load)
        {
            mapCreate = GameObject.Find("MapCreate").GetComponent<MapCreate>();
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
                timeText.text = "高さ" + (int)player.transform.position.y;
                //time -= Time.deltaTime;
                //slider.value = time;
                if(time<0)
                {

                        SceneManager.LoadScene("Clear");


                }
                if (player.endflag)
                {
                    SceneManager.LoadScene("GameOver");
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    
                    SceneManager.LoadScene("Test");
                }

                break;
            case Scene.StageSelect:
                break;
            case Scene.GameOver:
                if (Input.GetKeyDown(KeyCode.R))
                {

                    SceneManager.LoadScene("Test");
                }
                break;
            case Scene.GameCrear:
                if (Input.GetKeyDown(KeyCode.R))
                {

                    SceneManager.LoadScene("Test");
                }
                break;
            case Scene.Load:
                for(int i=0;i<map;i++)
                {
                    mapCreate.LoadMap(Resources.Load("map"+i) as TextAsset,i);
                }
                SceneManager.LoadScene("Test");
                break;
        }

    }
}
