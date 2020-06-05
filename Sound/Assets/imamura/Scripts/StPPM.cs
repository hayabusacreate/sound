using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class TitlePPM : MonoBehaviour
{

    // Post Process Volume がついているGameObject
    [SerializeField]
    GameObject ppc;

    PostProcessVolume m_Volume;

    bool lumino;

    public float val = 1;

    public float aiueo;

    void Start()
    {
        //Invoke("FixDOF", 1f);
    }

    void Update()
    {
        var ae = ScriptableObject.CreateInstance<AutoExposure>();
        ae.keyValue.Override(val);
        ae.maxLuminance.Override(-val);

        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 1f, ae);
        if (Input.GetKeyDown(KeyCode.S))
        {
            lumino = true;
        }

        if(lumino == true && val <= aiueo)
        {
            ae.enabled.Override(true);
            ae.keyValue.Override(val);
            ae.maxLuminance.Override(-val+2);

            val += Time.deltaTime*2;
        }
        
        if(val >= aiueo)
        {
            ae.enabled.Override(true);
            val = aiueo;
            ae.keyValue.Override(val);
            ae.maxLuminance.Override(-val);
        }

    }

    ////void FixDOF()
    ////{
        
    ////    //PostProcessManager.instance.QuickVolume(postProcessGameObject.layer, 1, ae);
    ////}
}
