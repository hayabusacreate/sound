using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClear : MonoBehaviour
{
    public  bool crearflag;
    public GameObject clear, bad;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(crearflag)
        {
            clear.SetActive(true);
            bad.SetActive(false);
        }else
        {
            clear.SetActive(false);
            bad.SetActive(true);
        }
    }
    public void ChangeFlag(bool a)
    {
        crearflag = a;
    }
}
