using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getFill : MonoBehaviour
{

    public Image cooldown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        cooldown = gameObject.GetComponent<Image>();
        cooldown.material.SetFloat("_Ratio", cooldown.fillAmount);
    }
}
