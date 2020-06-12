using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hanabi2 : MonoBehaviour
{
    [SerializeField]
    GameObject hanabi;

    float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var ps = hanabi.transform.GetComponent<ParticleSystem>();
        ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        if (hanabi == null)
        {
            transform.position = transform.position;
        }

        time += Time.deltaTime;

        if(time >= 4)
        {
            Destroy(hanabi);
        }
        transform.position = hanabi.transform.position;
    }
}
