using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeachController : MonoBehaviour
{
    
    private const float fallSpeedY = -1.5f;     // 桃が落ちるスピード
    private bool isFlowed = false;               // (桃が)川を流れるフラグ

    private void Update()
    {
        // ポーズ中ならUpdateを抜ける
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        if (isFlowed)
        {
            // -Y軸方向に、移動し続ける
            this.transform.Translate(0, fallSpeedY * Time.deltaTime, 0);
        }
    }

    // アニメーション終了後、桃移動開始
    private void PeachFlow()
    {
        isFlowed = true;
    }

}
