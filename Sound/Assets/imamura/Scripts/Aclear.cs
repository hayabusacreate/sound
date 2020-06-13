using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aclear : MonoBehaviour
{
    [SerializeField]
    GameObject shell;

    [SerializeField]
    GameObject slime;

    [SerializeField]
    Material gShell;

    [SerializeField]
    Material gSlime;

    [SerializeField]
    GameObject sc;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(sc.GetComponent<SceneChange>().allflag == true)
        {
            shell.GetComponent<Renderer>().material = gShell;
            slime.GetComponent<Renderer>().material = gSlime;
        }
    }
}
