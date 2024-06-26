using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController_23 : MonoBehaviour
{
    [SerializeField] GameObject clickCancelPnl;
    [SerializeField] GameObject itemInventory; // アイテム欄
    [SerializeField] GameObject speechBubble; // Playerの吹き出し
    [SerializeField] SpriteRenderer sr_strap;
    [SerializeField] SpriteRenderer sr_figure;
    [SerializeField] Animator animator_clerk;
    [SerializeField] Animator animator_gatyaL; 
    [SerializeField] Animator animator_gatyaR; 
    [SerializeField] SpriteRenderer sr_glassL; // ガチャ機のガラス(左)
    [SerializeField] SpriteRenderer sr_glassR; // ガチャ機のガラス(右)
    [SerializeField] Animator animator_gatyaHandleL;
    [SerializeField] Animator animator_gatyaHandleR;
    [SerializeField] SpriteRenderer sr_gatyaCapsuleR;
    [SerializeField] GameObject gatyaCapsules; // ガチャ(左)から溢れ出るガチャカプセル
    [SerializeField] GameObject stageManager;
    [SerializeField] Sprite glassLSpr1; // ガチャ(左)を1回揺らした時のガラス画像
    [SerializeField] Sprite glassLSpr2; // ガチャ(左)を2回揺らした時のガラス画像
    [SerializeField] Sprite glassLSpr3; // ガチャ(左)を3回揺らした時のガラス画像
    [SerializeField] Sprite glassRSpr1; // ガチャ(右)を1回揺らした時のガラス画像
    [SerializeField] Sprite glassRSpr2; // ガチャ(右)を2回揺らした時のガラス画像
    [SerializeField] Sprite glassRSpr3; // ガチャ(右)を3回揺らした時のガラス画像
    [SerializeField] Sprite coinItemSpr; // 100円アイテム画像

    private StageManager_23 sm_23;
    private Animator animator_player;
    private int count_PushGatyaR = 0; // 右のガチャを揺らした回数
    private int count_PushGatyaL = 0; // 左のガチャを揺らした回数
    internal bool canGetAGoldMan = false; // ゴールドマン取得可能フラグ
    private void Start()
    {
        sm_23 = stageManager.GetComponent<StageManager_23>();
        animator_player = this.GetComponent<Animator>();
    }

    // ---------- Animation -----------
    // "PlayerStop"アニメーション開始時
    private void ActiveSpeechBubble()
    {
        // 吹き出しを表示
        for(var i = 0; i < speechBubble.transform.childCount; i++)
        {
            speechBubble.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    
    // ガチャを揺らすアニメーション開始時
    private void PlayGatyaIsPushedAnima(string gatya) // Playerが押したガチャ
    {
        // ガチャが揺れるアニメーション再生
        switch (gatya)
        {
            // 右
            case "GatyaR":
                animator_gatyaR.Play("GatyaIsPushed");
                break;
            // 左
            case "GatyaL":
                animator_gatyaL.Play("GatyaIsPushed");
                break;
            default:
                Debug.Log($"{gatya}は無効な文字列です");
                break;
        }
    }
    private void isPlayingClerkTurnRightAnima()
    {
        // ClerkがPlayerの方を向いているアニメーションが再生されていたら、ゲームオーバー
        if (animator_clerk.GetCurrentAnimatorClipInfo(0)[0].clip.name == "ClerkTurnRight")
        {
            CantGameControl();
            animator_clerk.Play("ClerkIsSurprised");
        }
    }

    // 左のガチャを揺らすアニメーション終了時
    private void ChangeGlassLSpr()
    {
        count_PushGatyaL++;
        // ガチャ(左)を揺らした回数に応じて、ガラスの画像を変更
        switch (count_PushGatyaL)
        {
            case 1:
                sr_glassL.sprite = glassLSpr1;
                break;
            case 2:
                sr_glassL.sprite = glassLSpr2;
                break;
            case 3:
                sr_glassL.sprite = glassLSpr3;
                break;
        }

    }
    // 右のガチャを揺らすアニメーション終了時
    private void ChageGlassRSpr()
    {
        count_PushGatyaR++;
        // ガチャ(右)を揺らした回数に応じて、ガラスの画像を変更
        switch (count_PushGatyaR)
        {
            case 1:
                sr_glassR.sprite = glassRSpr1;
                break;
            case 2:
                sr_glassR.sprite = glassRSpr2;
                break;
            case 3:
                sr_glassR.sprite = glassRSpr3;
                break;
        }
    }
    private void isCanGetAGoldMan()
    {
        // ガチャ(右)を3回揺らしていたら、ゴールドマン取得可能に
        if(count_PushGatyaR == 3)
        {
            // ゴールドマン取得可能に
            canGetAGoldMan = true;
            animator_player.SetBool("ClearFlag", true);
        }
    }
    
    // ガチャを回すアニメーション開始時
    private void PlayGatyaHandleIsTurnedAnima(string gatya)
    {
        // ガチャハンドルを回す
        switch (gatya)
        {
            // 右
            case "GatyaR":
                animator_gatyaHandleR.Play("GatyaHandleIsTurned");
                break;
            // 左
            case "GatyaL":
                animator_gatyaHandleL.Play("GatyaHandleIsTurned");
                break;
            default:
                Debug.Log($"{gatya}は無効な文字列です");
                break;
        }
        
    }

    // 右のガチャを回すアニメーション終了時
    private async void GetAStrapOrFigure()
    {
        // ハンドルの動きを停止
        animator_gatyaHandleR.Play("GatyaHandleStart");
        // ガチャカプセルを表示
        sr_gatyaCapsuleR.enabled = true;

        //  --- ストラップorフィギュア取得処理 ---
        await UniTask.Delay(TimeSpan.FromSeconds(1f),cancellationToken: this.GetCancellationTokenOnDestroy());
        sr_gatyaCapsuleR.enabled = false;

        // GatyaRCntクラスで、100円を入れた回数に応じてストラップが出るかフィギュアが出るか設定している
        // 出る方には画像を設定、出ない方はSpriteをNullにしている

        // ストラップが出るなら
        if (sr_strap.sprite != null)
        {
            // ストラップ取得
            animator_player.Play("PlayerGetAStrap");
            sr_strap.enabled = true;
        }
        // フィギュアが出るなら
        else
        {
            // フィギュア取得
            animator_player.Play("PlayerGetAFigure");
            sr_figure.enabled = true;
            // フィギュアがゴールドマン(当たり)ならそのままクリア処理
        }
        // ------------------------------------
    }
    // 左のガチャを回すアニメーション終了時
    private async void PlayGatyaCapsulesAnima()
    {
        // ハンドルの動きを停止
        animator_gatyaHandleL.Play("GatyaHandleStart");

        // ガチャカプセルが溢れ出るアニメーション再生
        animator_gatyaL.Play("GatyaShake");
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f),cancellationToken: this.GetCancellationTokenOnDestroy());
        gatyaCapsules.GetComponent<SpriteRenderer>().enabled = true;
        gatyaCapsules.GetComponent<Animator>().enabled = true;


    }

    // ストラップ・フィギュア取得アニメーション終了時
    private void InActiveStrapAndFigure()
    {
        // ストラップ・フィギュア非表示
        sr_strap.enabled = false;
        sr_figure.enabled = false;
    }
    private void isHaveAnyCoinItem()
    {
        // ゴールドマン(当たり)を引いていたら、そのままメソッドを抜ける
        if (canGetAGoldMan)
        {
            return;
        }

        // ---- 100円アイテム所持チェック ----
        for (var i = 0; i < itemInventory.transform.childCount; i++)
        {
            Sprite itemSpr = itemInventory.transform.GetChild(i).GetComponent<Image>().sprite;

            // 100円アイテムを1枚でも持っていたら、ゲーム続行
            if (itemSpr == coinItemSpr)
            {
                // ゲーム操作を可能に
                sm_23.CanGameControl();
                return;
            }
        }
        // 100円アイテムを1枚も所持していなかったら、ゲームオーバー処理
        animator_player.SetBool("OverFlag", true);
        // --------------------------------
    }

    // ガチャを揺らすor回すアニメーション開始時
    private void CantGameControl()
    {
        // 吹き出しを非表示に
        InActiveSpeechBubble();
        // ゲーム操作をできないようにする
        sm_23.CantGameControl();
    }

    // ガチャを揺らすアニメーション終了時
    private void CanGameControl()
    {
        // ゲーム操作を可能にする
        sm_23.CanGameControl();
    }
    // ---------------------------------------

    // 吹き出し非表示メソッド
    private void InActiveSpeechBubble()
    {
        // 吹き出しを非表示
        for (var i = 0; i < speechBubble.transform.childCount; i++)
        {
            speechBubble.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = false;
        }
    }

}
