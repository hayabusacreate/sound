using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hanabiEmit : MonoBehaviour
{
    [SerializeField]
    GameObject hanabi;

    float time;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(hanabi);
        Instantiate(hanabi);
        Instantiate(hanabi);
        Instantiate(hanabi);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= 2)
        {
            Instantiate(hanabi);
            time = 0;
        }
    }
}
