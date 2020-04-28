using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coffin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform myTransform = this.transform;

        // 座標を取得
        Vector3 pos = myTransform.position;
        if(Input.GetKey(KeyCode.D))
        {
            pos.x += 0.02f;    // x座標へ0.01加算

        }

        myTransform.position = pos;  // 座標を設定
    }
}
