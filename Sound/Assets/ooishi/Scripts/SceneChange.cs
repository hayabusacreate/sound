using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    // Start is called before the first frame update
    void Start()
    {
        if(scene==Scene.GamePlay)
        {
            player=GameObject.Find("Player").GetComponent<Player>();
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
