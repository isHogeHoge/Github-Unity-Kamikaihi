using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrotherAnimaCnt_19 : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr_radioWaveEffect; // 電波エフェクト
    [SerializeField] SpriteRenderer sr_sb_phone;  // 電話の吹き出し
    [SerializeField] Animator animator_mother;

    // 電話をかけるアニメーション再生中
    private void StartRinging()
    {
        // 電話のエフェクトを表示
        sr_radioWaveEffect.enabled = true;
        sr_sb_phone.enabled = true;

        // Motherが電話を取りに行くアニメーション開始
        animator_mother.Play("MotherStop");

    }
}
