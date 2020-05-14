using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gage : MonoBehaviour
{
    private Player player;
    private float hight;
    private RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        rectTransform = gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        hight = player.transform.position.y;

        rectTransform.localPosition= new Vector3(-900,hight/2,0);
    }
}
