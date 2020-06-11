using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleScaleT : MonoBehaviour
{
    float scale;
    // Start is called before the first frame update
    public float speed = 1;
    

    Vector3 pos;

    ParticleSystem m_particle;

    [SerializeField]
    GameObject left;

    [SerializeField]
    GameObject right;

    void Start()
    {
        scale = 0;
        speed = 1;
        m_particle = transform.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_particle.isPlaying)
        {
            if (scale <= 1 && m_particle.isPlaying)
            {
                scale = scale + Time.deltaTime + Time.deltaTime;
            }
            else if (scale >= 1)
            {
                left.SetActive(true);
                right.SetActive(true);
            }
        }

        var main = m_particle.main;

        var sh = m_particle.shape;
        
        main.startSpeed = speed;

        sh.scale = new Vector3(0.5f, scale, 0);

       
        
    }
}
