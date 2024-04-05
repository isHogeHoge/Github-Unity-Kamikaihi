using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;

public class MotherAnimaCnt_19 : MonoBehaviour
{
    [SerializeField] GameObject brother;
    [SerializeField] GameObject radioWaveEffect; // 電波エフェクト
    [SerializeField] GameObject sb_phone; // 電話の吹き出し
    [SerializeField] GameObject player1; // player移動前
    [SerializeField] GameObject apron;
    [SerializeField] GameObject chopsticks;
    [SerializeField] GameObject chopsticksBtn; // 箸アイテムボタン
    [SerializeField] GameObject apronBtn;      // エプロンアイテムボタン
    [SerializeField] GameObject clickCancelPnl;
    [SerializeField] GameObject stageManager;

    // 電話を取りに行くアニメーション開始時
    private void ActiveApronAndChopsticksItem()
    {
        // エプロンアイテム出現
        apron.GetComponent<SpriteRenderer>().enabled = true;
        apronBtn.GetComponent<Button>().enabled = true;

        // 箸アイテム出現
        chopsticks.GetComponent<SpriteRenderer>().enabled = true;
        chopsticksBtn.GetComponent<Button>().enabled = true;
    }
    // 電話を取り終えた後
    private void EndRinging()
    {
        clickCancelPnl.SetActive(false);

        // 電話エフェクトを非表示
        radioWaveEffect.GetComponent<SpriteRenderer>().enabled = false;
        sb_phone.GetComponent<SpriteRenderer>().enabled = false;

        // Player1とBrotherのアニメーションを初期のものに
        player1.GetComponent<Animator>().Play("Player1Start");
        brother.GetComponent<Animator>().Play("BrotherStart");
    }

}
