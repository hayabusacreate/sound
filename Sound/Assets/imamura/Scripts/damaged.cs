using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damaged : MonoBehaviour
{
    Player player;

    public float interval = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        player = GetComponentInParent<Player>();
        Renderer renderComponent = GetComponent<Renderer>();
        if (player.damageflag)
        {
            StartCoroutine("Blink");
        }
       else if(renderComponent.enabled)
        {
            StopCoroutine("Blink");
        }
        

    }


    IEnumerator Blink()
    {
        while (true)
        {
            Renderer renderComponent = GetComponent<Renderer>();
            renderComponent.enabled = !renderComponent.enabled;
            yield return new WaitForSeconds(interval);
        }
    }
}
