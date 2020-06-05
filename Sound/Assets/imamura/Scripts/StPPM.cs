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

    PostProcessVolume m_Volume;

    bool lumino;

    public float val;

    public float aiueo;

    static bool once = false;

    void Start()
    {
        val = 8;
        var ae = ScriptableObject.CreateInstance<AutoExposure>();
        ae.keyValue.Override(val);
        ae.maxLuminance.Override(-val);
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
                ae.enabled.Override(true);
                ae.keyValue.Override(val);
                ae.maxLuminance.Override(-val + 2);

                val -= Time.deltaTime * 2;
            }

            if (val <= aiueo)
            {
                ae.enabled.Override(true);
                val = aiueo;
                ae.keyValue.Override(val);
                ae.maxLuminance.Override(-val);
                Canvas.SetActive(true);
                once = true;
            }
        }

    }

    ////void FixDOF()
    ////{
        
    ////    //PostProcessManager.instance.QuickVolume(postProcessGameObject.layer, 1, ae);
    ////}
}
