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
}

public class SceneChange : MonoBehaviour
{
    public Scene scene;
    private Player player;
    private float time;
    public float setTime;
    private Text timeText;
    // Start is called before the first frame update
    void Start()
    {
        if(scene==Scene.GamePlay)
        {
            player=GameObject.Find("Player").GetComponent<Player>();
            timeText = GameObject.Find("Time").GetComponent<Text>();
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
                timeText.text = "" + time;
                time += Time.deltaTime;
                if(setTime<time)
                {
                    SceneManager.LoadScene("Test");
                }
                if(Input.GetKeyDown(KeyCode.R)||player.endflag)
                {
                    
                    SceneManager.LoadScene("Test");
                }
                break;
            case Scene.StageSelect:
                break;
        }

    }
}
