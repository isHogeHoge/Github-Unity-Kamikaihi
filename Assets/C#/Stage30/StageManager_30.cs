using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System;

// 現在アクティブなレーザー
public enum ActiveLaser
{
    red,
    green
}

public class StageManager_30 : MonoBehaviour
{
    [SerializeField] GameObject nightScopeBtn;
    [SerializeField] GameObject redPanel; // (ゲームオーバー時)画面を赤くする点滅させるパネル
    [SerializeField] GameObject greenPanel; // (暗視スコープ装着中)画面を緑色にするパネル
    [SerializeField] GameObject redLasers;
    [SerializeField] GameObject greenLaser;
    [SerializeField] GameObject goBtn;
    [SerializeField] GameObject backBtn;
    [SerializeField] GameObject playerL;
    [SerializeField] GameObject playerR;
    [SerializeField] GameObject police;
    [SerializeField] GameObject tutankhamun;
    [SerializeField] GameObject treasuresStand;
    [SerializeField] GameObject stagePanel_UI; // スクロールさせるUI
    [SerializeField] GameObject stagePanel;    // スクロールさせるゲームオブジェクト
    [SerializeField] Sprite redSwitchSpr;     // レーザースイッチ(赤)画像
    [SerializeField] Sprite greenSwitchSpr;   // レーザースイッチ(緑)画像

    internal ActiveLaser currentLaser = ActiveLaser.red; // アクティブなレーザーを"赤"に設定
    private bool isOpen = false;  // ツタンカーメン開閉状態フラグ

    void Start()
    {
        // ステージ初期位置から右に1ページ分だけ移動できるように設定
        stagePanel_UI.GetComponent<StageScrollCnt>().maxCountR = 1;
        stagePanel.GetComponent<StageScrollCnt>().maxCountR = 1;
    }



    // -------- Button --------
    // 「進む」ボタン
    public void ClickGoBtn()
    {
        // Player(L)移動開始
        playerL.GetComponent<Animator>().SetBool("walkFlag", true);
        playerL.GetComponent<PlayerLController_30>().isMoving = true;
    }

    // ツタンカーメン
    public void ClickTutankhamunBtn()
    {
        // 開 → 閉
        if (isOpen)
        {
            // 中の暗視スコープを取得不可能に
            tutankhamun.GetComponent<Animator>().Play("TutankhamunSmile");
            nightScopeBtn.GetComponent<Image>().enabled = false;
        }
        // 閉 → 開
        else
        {
            // 中の暗視スコープを取得可能に
            tutankhamun.GetComponent<Animator>().Play("TutankhamunIsOpen");
            nightScopeBtn.GetComponent<Image>().enabled = true;
        }
        isOpen = !isOpen;
    }

    // Treasure'sStand
    public async void ClickTreasuresStandBtn()
    {
        // 台スライド → 台下の穴からPlayer脱出
        CantGameControl();
        treasuresStand.GetComponent<Animator>().enabled = true;
        await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: this.GetCancellationTokenOnDestroy());
        playerR.GetComponent<Animator>().Play("PlayerGetInTheHole");

    }
    // レーザー切り替えスイッチ
    // レーザー(赤・緑)切り替え処理
    public void ClickSwitchOfLaserBtn(GameObject switchOfLaserBtn)
    {
        // 赤 → 緑
        if (currentLaser == ActiveLaser.red)
        {
            // レーザー(赤)を非アクティブに
            for (var i = 0; i < redLasers.transform.childCount; i++)
            {
                redLasers.transform.GetChild(i).GetComponent<Image>().enabled = false;
            }
            redLasers.GetComponent<BoxCollider2D>().enabled = false;

            // レーザー(緑)をアクティブに
            // Playerが暗視スコープを装備していないなら、レーザー(緑)を表示する
            if (!playerL.GetComponent<Animator>().GetBool("isWearing"))
            {
                greenLaser.GetComponent<SpriteRenderer>().enabled = true;
            }
            switchOfLaserBtn.GetComponent<Image>().sprite = greenSwitchSpr;
            currentLaser = ActiveLaser.green;

        }
        // 緑 → 赤
        else
        {
            // レーザー(赤)をアクティブに
            // Playerが暗視スコープを装備してるなら、レーザー(赤)を表示
            if (playerL.GetComponent<Animator>().GetBool("isWearing"))
            {
                for (var i = 0; i < redLasers.transform.childCount; i++)
                {
                    redLasers.transform.GetChild(i).GetComponent<Image>().enabled = true;
                }
            }
            redLasers.GetComponent<BoxCollider2D>().enabled = true;

            // レーザー(緑)を非アクティブに
            greenLaser.GetComponent<SpriteRenderer>().enabled = false;
            switchOfLaserBtn.GetComponent<Image>().sprite = redSwitchSpr;
            currentLaser = ActiveLaser.red;
        }
    }
    // --------------------------

    /// <summary>
    /// 右・左側のページに移動
    /// </summary>
    /// <param name="dir">"RIGHT"or"LEFT"</param>
    internal void ScrollStagePnl(string dir)
    {
        stagePanel_UI.GetComponent<StageScrollCnt>().ScrollStagePnl(dir);
        stagePanel.GetComponent<StageScrollCnt>().ScrollStagePnl(dir);
    }

    // アクティブなPlayerを返すメソッド
    internal GameObject GetActivePlayer()
    {
        GameObject player = null;
        if (playerL.activeSelf)
        {
            player = playerL;
        }
        else
        {
            player = playerR;
        }
        return player;
    }

    // ゲームオーバー処理
    // アラームボタンクリック時 & PlayerがLaserを踏んだ時
    public void GameOver()
    {
        redPanel.SetActive(true);

        // ゲーム操作を禁止 & Playerの動きをストップ
        CantGameControl();
        playerL.GetComponent<PlayerLController_30>().isMoving = false;

        // PlayerL(PlayerR)のゲームオーバーアニメーションを再生
        GetActivePlayer().GetComponent<Animator>().applyRootMotion = true;
        GetActivePlayer().GetComponent<Animator>().SetBool("isOver", true);

        // Police移動開始
        police.SetActive(true);
    }

    // ゲーム操作禁止処理
    public void CantGameControl()
    {
        goBtn.GetComponent<Image>().enabled = false;
        backBtn.GetComponent<Image>().enabled = false;
        this.GetComponent<StageManager>().CantGameControl();
    }
}
