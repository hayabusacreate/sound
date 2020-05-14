using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleScale : MonoBehaviour
{
    ParticleSystem m_particle;
    float scale;
    // Start is called before the first frame update
    void Start()
    {
        scale = 0;
        m_particle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKey(KeyCode.UpArrow))
        //{
        //    scale += 0.1f;
        //}
        //if (Input.GetKey(KeyCode.DownArrow))
        //{
        //    scale -= 0.1f;
        //}

        //if(Input.GetKeyDown(KeyCode.A))
        //{
        //    scale = 0;
        //}

        if(scale <= 1 && m_particle.isPlaying)
        {
            scale = scale + Time.deltaTime + Time.deltaTime;
        }
        

        var sh = m_particle.shape;

        sh.scale = new Vector3(scale, 1, 0);
    }
}
