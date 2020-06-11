using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleScaleBLR : MonoBehaviour
{
    float scale;
    // Start is called before the first frame update
    public float speed = 1;

    ParticleSystem m_particle;

    float value;

    Vector3 pos;

    bool done;

    [SerializeField]
    GameObject par;

    void Start()
    {
        scale = 0;
        speed = 1;
        m_particle = transform.GetComponent<ParticleSystem>();
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
            if (scale <= 1 && m_particle.isPlaying)
            {
                scale = scale + Time.deltaTime + Time.deltaTime;
            }
        if (par.name == "YukaFireTop")
        {
            if (done == false)
            {
                if (transform.position.x >= pos.x - 0.45f && m_particle.isPlaying && transform.name == "fireBotR")
                {
                    value -= Time.deltaTime;

                }
                else if (value <= -0.45f)
                {
                    done = true;
                }
                transform.position = new Vector3(pos.x + value, transform.position.y, transform.position.z);
            }

            if (done == false)
            {
                if (transform.position.x <= pos.x + 0.45f && m_particle.isPlaying && transform.name == "fireBotL")
                {
                    value += Time.deltaTime;
                }
                else if (value >= 0.45f)
                {
                    done = true;
                }
                transform.position = new Vector3(pos.x + value, transform.position.y, transform.position.z);
            }
        }
        else if (par.name == "YukaFireRight")
        {
            if (done == false)
            {
                if (transform.position.y >= pos.y - 0.45f && m_particle.isPlaying && transform.name == "fireLeftT")
                {
                    value -= Time.deltaTime;

                }
                else if (value <= -0.45f)
                {
                    done = true;
                }
                transform.position = new Vector3(transform.position.x, pos.y + value, transform.position.z);
            }

            if (done == false)
            {
                if (transform.position.y <= pos.y + 0.45f && m_particle.isPlaying && transform.name == "fireLeftB")
                {
                    value += Time.deltaTime;
                }
                else if (value >= 0.45f)
                {
                    done = true;
                }
                transform.position = new Vector3(transform.position.x, pos.y + value, transform.position.z);
            }
        }
        else if (par.name == "YukaFireLeft")
        {
            if (done == false)
            {
                if (transform.position.y >= pos.y - 0.45f && m_particle.isPlaying && transform.name == "fireRightT")
                {
                    value -= Time.deltaTime;

                }
                else if (value <= -0.45f)
                {
                    done = true;
                }
                transform.position = new Vector3(transform.position.x, pos.y + value, transform.position.z);
            }

            if (done == false)
            {
                if (transform.position.y <= pos.y + 0.45f && m_particle.isPlaying && transform.name == "fireRightB")
                {
                    value += Time.deltaTime;
                }
                else if (value >= 0.45f)
                {
                    done = true;
                }
                transform.position = new Vector3(transform.position.x, pos.y + value, transform.position.z);
            }
        }
        else if (par.name == "YukaFireBot")
        {
            if (done == false)
            {
                if (transform.position.x >= pos.x - 0.45f && m_particle.isPlaying && transform.name == "fireTopR")
                {
                    value -= Time.deltaTime;

                }
                else if (value <= -0.45f)
                {
                    done = true;
                }
                transform.position = new Vector3(pos.x + value, transform.position.y, transform.position.z);
            }

            if (done == false)
            {
                if (transform.position.x <= pos.x + 0.45f && m_particle.isPlaying && transform.name == "fireTopL")
                {
                    value += Time.deltaTime;
                }
                else if (value >= 0.45f)
                {
                    done = true;
                }
                transform.position = new Vector3(pos.x + value, transform.position.y, transform.position.z);
            }
        }


        var main = m_particle.main;

        var sh = m_particle.shape;
        
        main.startSpeed = speed;

        sh.scale = new Vector3(0.5f, scale, 0);

        if (transform.name == "fireBotR" && done == true)
        {
            Destroy(gameObject);
        }
        else if (transform.name == "fireLeftB" && done == true)
        {
            Destroy(gameObject);
        }
        else if (transform.name == "fireRightB" && done == true)
        {
            Destroy(gameObject);
        }
        else if (transform.name == "fireTopR" && done == true)
        {
            Destroy(gameObject);
        }

    }
}
