using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    public GameObject se;
    public GameObject iceobj;
    public bool pon;
    public bool ice;
    public bool juu;
    public bool ban;
    private float volume;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pon)
        {
            Instantiate(se, transform.position, Quaternion.identity);
            pon = false;
        }
        if(ice)
        {
            Instantiate(iceobj, transform.position, Quaternion.identity);
            ice = false;
        }
        //if(juu)
        //{
        //    if(ban)
        //    {
        //        volume+= Time.deltaTime*10;
        //        ban = false;
        //        if(volume>1)
        //        {
        //            volume = 1;
        //        }
        //    }
        //    if (volume > 0.8f)
        //    {
        //        volume -= Time.deltaTime/10;
        //    }
        //}
        //else
        //{
        //    if(volume>0)
        //    {
        //        volume -= Time.deltaTime/10;
        //    }
        //}
        //audioSource.volume = volume;
    }
}
