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

    Material shellMat;

    Material slimeMat;


    // Start is called before the first frame update
    void Start()
    {
        shellMat = shell.GetComponent<Renderer>().material;
        slimeMat = slime.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if(sc.GetComponent<SceneChange>().allflag == true)
        {
            shell.GetComponent<Renderer>().material = gShell;
            slime.GetComponent<Renderer>().material = gSlime;
        }
        else
        {
            shell.GetComponent<Renderer>().material = shellMat;
            slime.GetComponent<Renderer>().material = slimeMat;
        }
    }
}
