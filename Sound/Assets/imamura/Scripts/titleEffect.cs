using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class titleEffect : MonoBehaviour
{
    [SerializeField]
    GameObject shell;
    

    public Color EColor;

    bool cColor;

    public bool exEnd;

    float cl;
    public float intensity;

    [SerializeField]
    GameObject ppc;

    PostProcessVolume m_Volume;

    bool lumino;

    float val = 1;

    public float aiueo;

    float delay = 0;

    // Start is called before the first frame update
    void Start()
    {
        //shell.GetComponent<Renderer>().material
        val = 1;
        var ae = ScriptableObject.CreateInstance<AutoExposure>();
        ae.keyValue.Override(val+5);
        ae.maxLuminance.Override(-val);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0"))
        {
            cColor = true;
            lumino = true;
        }

        if(cColor == true)
        {
            if (cl <= 1)
            {
                cl += Time.deltaTime;
                EColor = new Color(cl, cl - 0.7f, cl - 0.9f);
            }
            shell.GetComponent<Renderer>().material.SetColor("_EmissionColor", EColor* intensity);
        }

        var ae = ScriptableObject.CreateInstance<AutoExposure>();
        ae.keyValue.Override(val);
        ae.maxLuminance.Override(-val);

        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 1f, ae);
        

        if (lumino == true && val <= aiueo)
        {
            ae.enabled.Override(true);
            ae.keyValue.Override(val-0.3f);
            ae.maxLuminance.Override(-val+2);

            val += Time.deltaTime * 2;
        }

        if (val >= aiueo)
        {
            ae.enabled.Override(true);
            val = aiueo;
            ae.keyValue.Override(val);
            ae.maxLuminance.Override(-val);
        }

        if (val >= aiueo)
        {
            delay += Time.deltaTime;
            if(delay >= 0.5f)
            {
                exEnd = true;
            }
        }
    }
}
