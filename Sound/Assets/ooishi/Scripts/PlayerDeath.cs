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
        transform.position = new Vector3(player.transform.position.x,player.transform.position.y+1,player.transform.position.z);
    }
    private void OnCollisionEnter(Collision collision)
    {


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Block")
        {
            player.endflag = true;
        }
    }
}
