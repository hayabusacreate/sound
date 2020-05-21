using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetBallet : MonoBehaviour
{
    private Player player;
    private GameObject pl;
    private float range;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        pl= GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, -0.5f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Block")
        {
            range = Vector3.Distance(transform.position, pl.transform.position);
            player.range = range;
            other.gameObject.GetComponent<Block>().damageflag = true;
            Destroy(gameObject);
        }
    }
}
