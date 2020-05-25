using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burned : MonoBehaviour
{
    [SerializeField]
    GameObject target;

    Vector3 end;

    Vector3 pos;

    public float rate;

    bool move;

    private float distance_two;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        end = target.transform.position;
        if(move == false)
        {
            pos = transform.position;
        }
        
        distance_two = Vector3.Distance(pos, end);

        

        //Debug.Log("fuck");

        if (Input.GetKeyDown(KeyCode.Space) && move == false)
        {
            move = true;
        }

        if(move == true)
        {

            rate = rate + Time.deltaTime;

            float present_Location = (rate * 5.0f) / distance_two;
            transform.position = Vector3.Slerp(pos, end, present_Location);
            if (present_Location >= 1)
            {
                move = false;
                present_Location = 0;
            }
        }
        else if(move == false)
        {
            rate = 0;
            transform.position = new Vector3(-15,0,0);
        }
    }
}
