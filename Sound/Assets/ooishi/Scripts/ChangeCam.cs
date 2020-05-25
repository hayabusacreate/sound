using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class ChangeCam : MonoBehaviour
{
    public CinemachineVirtualCamera start, end;
    public GameObject endobj;
    public bool changeflag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(changeflag)
        {
            end.Priority = 100;
            start.Priority = 0;
        }
    }
}
