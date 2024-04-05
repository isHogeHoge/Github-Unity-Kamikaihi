using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainHitBox : MonoBehaviour
{
    // 地面に接触時、雨粒Prefabを破棄
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
    }
}
