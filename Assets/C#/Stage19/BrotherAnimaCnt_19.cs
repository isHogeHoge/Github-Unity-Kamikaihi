using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrotherAnimaCnt_19 : MonoBehaviour
{
    [SerializeField] GameObject radioWaveEffect; // 電波エフェクト
    [SerializeField] GameObject sb_phone;  // 電話の吹き出し
    [SerializeField] GameObject mother;

    // 電話をかけるアニメーション再生中
    private void StartRinging()
    {
        // 電話のエフェクトを表示
        radioWaveEffect.GetComponent<SpriteRenderer>().enabled = true;
        sb_phone.GetComponent<SpriteRenderer>().enabled = true;

        // Motherが電話を取りに行くアニメーション開始
        mother.GetComponent<Animator>().Play("MotherStop");

    }
}
