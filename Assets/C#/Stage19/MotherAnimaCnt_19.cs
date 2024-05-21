using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;

public class MotherAnimaCnt_19 : MonoBehaviour
{
    [SerializeField] Button chopsticksBtn; // 箸アイテムボタン
    [SerializeField] Button apronBtn;      // エプロンアイテムボタン
    [SerializeField] GameObject clickCancelPnl;
    [SerializeField] SpriteRenderer sr_apron;
    [SerializeField] SpriteRenderer sr_chopsticks;
    [SerializeField] Animator animator_brother;
    [SerializeField] SpriteRenderer sr_radioWaveEffect; // 電波エフェクト
    [SerializeField] SpriteRenderer sr_sb_phone; // 電話の吹き出し
    [SerializeField] Animator animator_player1; // player移動前
    [SerializeField] GameObject stageManager;

    // 電話を取りに行くアニメーション開始時
    private void ActiveApronAndChopsticksItem()
    {
        // エプロンアイテム出現
        sr_apron.enabled = true;
        apronBtn.enabled = true;

        // 箸アイテム出現
        sr_chopsticks.enabled = true;
        chopsticksBtn.enabled = true;
    }
    // 電話を取り終えた後
    private void EndRinging()
    {
        clickCancelPnl.SetActive(false);

        // 電話エフェクトを非表示
        sr_radioWaveEffect.enabled = false;
        sr_sb_phone.enabled = false;

        // Player1とBrotherのアニメーションを初期のものに
        animator_player1.Play("Player1Start");
        animator_brother.Play("BrotherStart");
    }

}
