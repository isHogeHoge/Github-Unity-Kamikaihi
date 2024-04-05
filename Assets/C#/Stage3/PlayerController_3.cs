using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_3 : MonoBehaviour
{
    [SerializeField] GameObject sittingOni;
    private const float flowSpeedY = -0.6f;  // (プレイヤーが)川を流れるスピード

    private void Update()
    {
        // ポーズ中ならUpdateを抜ける
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        // -Y軸方向に川を流れ続ける
        this.transform.Translate(0, flowSpeedY * Time.deltaTime, 0);
    }

    // アニメーション切り替えコライダーと接触後
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Change")
        {
            // Oniのアニメーション切り替え
            Animator animator = sittingOni.GetComponent<Animator>();
            animator.Play("OniStop");
        }
    }
}
