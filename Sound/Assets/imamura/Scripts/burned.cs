using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burned : MonoBehaviour
{
    public GameObject target;

    Vector3 end;

    Vector3 pos;

    public float rate;


    private MapCreate mapCreate;

    float time;

    private float distance_two;

    ParticleSystem part;

    // Start is called before the first frame update
    void Start()
    {
        part = GetComponent<ParticleSystem>();
        mapCreate = GameObject.Find("MapCreate").GetComponent<MapCreate>();
        target = mapCreate.goalObj;
        end = target.transform.position;
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {



        
            distance_two = Vector3.Distance(pos, end);

        

            rate = rate + Time.deltaTime;

            float present_Location = (rate * 4.0f) / distance_two;
            transform.position = Vector3.Slerp(pos, end, present_Location);
            
        if(present_Location >= 1)
        {

            present_Location = 0;
            part.Stop();
            time += Time.deltaTime;
        }

        if(time >= 3)
        {

            Destroy(gameObject);
        }

    }
}
