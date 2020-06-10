using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class StPPM : MonoBehaviour
{

    // Post Process Volume がついているGameObject
    [SerializeField]
    GameObject ppc;

    [SerializeField]
    GameObject Canvas;

    [SerializeField]
    GameObject Canvas2;

    PostProcessVolume m_Volume;

    bool lumino;

    public float val;

    public float aiueo;

    public bool still = false;

    static bool once = false;

    void Start()
    {
        val = 8;
        var ae = ScriptableObject.CreateInstance<AutoExposure>();
        ae.keyValue.Override(val);
        ae.maxLuminance.Override(-val);
        if (once == true)
        {
            still = true;
            Canvas.SetActive(true);
            Canvas2.SetActive(true);
        }
    }

    void Update()
    {
        if (once == false)
        {
            var ae = ScriptableObject.CreateInstance<AutoExposure>();
            ae.keyValue.Override(val);
            ae.maxLuminance.Override(-val);

            m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 1f, ae);

            lumino = true;

            if (lumino == true && val >= aiueo)
            {
                //ae.enabled.Override(true);
                //ae.keyValue.Override(val);
                //ae.maxLuminance.Override(-val + 5);

                val -= Time.deltaTime * 2;
            }

            if (val <= aiueo)
            {
                //ae.enabled.Override(true);
                val = aiueo;
                //ae.keyValue.Override(val);
                //ae.maxLuminance.Override(-val);
                Canvas.SetActive(true);
                Canvas2.SetActive(true);
                once = true;
                still = true;
            }
        }


    }

    ////void FixDOF()
    ////{
        
    ////    //PostProcessManager.instance.QuickVolume(postProcessGameObject.layer, 1, ae);
    ////}
}
