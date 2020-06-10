using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getFill : MonoBehaviour
{

    public Image cooldown;

    public SceneChange sc;

    public float value;
    public float a = 0;

    public float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        cooldown = gameObject.GetComponent<Image>();
        sc = GameObject.Find("SceneChange").GetComponent<SceneChange>();
        cooldown.material.SetFloat("_Ratio", 0);
        value= 0;
        a = 0;
        //var ae = ScriptableObject.CreateInstance<AutoExposure>();
    }

    // Update is called once per frame
    void Update()
    {
        a = (Mathf.Floor(value *100)/100) +(1 / (float)sc.mapCreate.maxblock);
        cooldown = gameObject.GetComponent<Image>();
        if (value < cooldown.fillAmount && sc.mapCreate.maxblock - sc.mapCreate.blocks != 0)
        {
            value += Time.deltaTime * speed;
        }

        //a = cooldown.fillAmount;

        //if(Input.GetKey(KeyCode.J))
        //{
        //    value += Time.deltaTime;
        //    Debug.Log("dada");
        //}
        cooldown.material.SetFloat("_Ratio", Mathf.Floor(value*100)/100);
    }
}
