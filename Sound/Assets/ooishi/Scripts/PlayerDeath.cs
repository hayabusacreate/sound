using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.transform.root.transform.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        player.endflag = true;
    }
}
