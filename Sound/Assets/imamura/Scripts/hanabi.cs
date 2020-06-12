using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hanabi : MonoBehaviour
{
    public float value,targetY,speed,valueX;


    [SerializeField]
    GameObject hanabi2;

    Vector3 pos;

    GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        value = 0;
        targetY = Random.Range(6f, 10f);
        valueX = Random.Range(-8f, 8f);
        camera = GameObject.Find("Main Camera");
        pos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(value <= targetY)
        {
            value += Time.deltaTime * speed;
        }
        else if(value >= targetY)
        {
            hanabi2.SetActive(true);
            
        }

        transform.position = new Vector3(camera.transform.position.x + valueX,( camera.transform.position.y-7) + value, camera.transform.position.z - 10);

    }
}
