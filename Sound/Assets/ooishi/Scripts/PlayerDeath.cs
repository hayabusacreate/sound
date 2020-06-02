using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    Renderer renderer;
    private SceneChange sceneChange;

    public GameObject boom;
    // Start is called before the first frame update
    void Start()
    {
        sceneChange = GameObject.Find("SceneChange").GetComponent<SceneChange>();
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(renderer.isVisible)
        //{
        //    sceneChange.endflag = true;
        //}
    }
    private void OnBecameInvisible()
    {
        sceneChange.endflag = true;
        Instantiate(boom, transform.position, Quaternion.identity);
    }
    private void OnCollisionEnter(Collision collision)
    {


    }
    private void OnTriggerEnter(Collider other)
    {
    }
}
