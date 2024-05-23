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
    [SerializeField] Image img_nightScopeBtn;
    [SerializeField] GameObject redPanel; // (ゲームオーバー時)画面を赤くする点滅させるパネル
    [SerializeField] GameObject greenPanel; // (暗視スコープ装着中)画面を緑色にするパネル
    [SerializeField] BoxCollider2D boxCol_redLasers;
    [SerializeField] Image img_goBtn;
    [SerializeField] Image img_backBtn;
    [SerializeField] Animator animator_playerL;
    [SerializeField] Animator animator_playerR;
    [SerializeField] GameObject police;
    [SerializeField] Animator animator_tutankhamun;
    [SerializeField] SpriteRenderer sr_greenLaser;
    [SerializeField] Animator animator_treasuresStand;
    [SerializeField] GameObject stagePanel_UI; // スクロールさせるUI
    [SerializeField] GameObject stagePanel;    // スクロールさせるゲームオブジェクト
    [SerializeField] Sprite redSwitchSpr;     // レーザースイッチ(赤)画像
    [SerializeField] Sprite greenSwitchSpr;   // レーザースイッチ(緑)画像

    private StageScrollCnt scrollCnt_UI;
    private StageScrollCnt scrollCnt;
    private PlayerLController_30 playerLCnt;
    internal ActiveLaser currentLaser = ActiveLaser.red; // アクティブなレーザーを"赤"に設定
    private bool isOpen = false;  // ツタンカーメン開閉状態フラグ

    void Start()
    {
        playerLCnt = animator_playerL.GetComponent<PlayerLController_30>();
        scrollCnt_UI = stagePanel_UI.GetComponent<StageScrollCnt>();
        scrollCnt = stagePanel.GetComponent<StageScrollCnt>();
        
        // ステージ初期位置から右に1ページ分だけ移動できるように設定
        scrollCnt_UI.maxCountR = 1;
        scrollCnt.maxCountR = 1;
    }



    // -------- Button --------
    // 「進む」ボタン
    public void ClickGoBtn()
    {
        // Player(L)移動開始
        animator_playerL.SetBool("walkFlag", true);
        playerLCnt.isMoving = true;
    }

    // ツタンカーメン
    public void ClickTutankhamunBtn()
    {
        // 開 → 閉
        if (isOpen)
        {
            // 中の暗視スコープを取得不可能に
            animator_tutankhamun.Play("TutankhamunSmile");
            img_nightScopeBtn.enabled = false;
        }
        // 閉 → 開
        else
        {
            // 中の暗視スコープを取得可能に
            animator_tutankhamun.Play("TutankhamunIsOpen");
            img_nightScopeBtn.enabled = true;
        }
        isOpen = !isOpen;
    }

    // Treasure'sStand
    public async void ClickTreasuresStandBtn()
    {
        // 台スライド → 台下の穴からPlayer脱出
        CantGameControl();
        animator_treasuresStand.enabled = true;
        await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: this.GetCancellationTokenOnDestroy());
        animator_playerR.Play("PlayerGetInTheHole");

    }
    // レーザー切り替えスイッチ
    // レーザー(赤・緑)切り替え処理
    public void ClickSwitchOfLaserBtn(Image img_switchOfLaserBtn)
    {
        // 赤 → 緑
        if (currentLaser == ActiveLaser.red)
        {
            // レーザー(赤)を非アクティブに
            for (var i = 0; i < boxCol_redLasers.transform.childCount; i++)
            {
                boxCol_redLasers.transform.GetChild(i).GetComponent<Image>().enabled = false;
            }
            boxCol_redLasers.enabled = false;

            // レーザー(緑)をアクティブに
            // Playerが暗視スコープを装備していないなら、レーザー(緑)を表示する
            if (!animator_playerL.GetBool("isWearing"))
            {
                sr_greenLaser.enabled = true;
            }
            img_switchOfLaserBtn.sprite = greenSwitchSpr;
            currentLaser = ActiveLaser.green;

        }
        // 緑 → 赤
        else
        {
            // レーザー(赤)をアクティブに
            // Playerが暗視スコープを装備してるなら、レーザー(赤)を表示
            if (animator_playerL.GetBool("isWearing"))
            {
                for (var i = 0; i < boxCol_redLasers.transform.childCount; i++)
                {
                    boxCol_redLasers.transform.GetChild(i).GetComponent<Image>().enabled = true;
                }
            }
            boxCol_redLasers.enabled = true;

            // レーザー(緑)を非アクティブに
            sr_greenLaser.enabled = false;
            img_switchOfLaserBtn.sprite = redSwitchSpr;
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
        scrollCnt_UI.ScrollStagePnl(dir);
        scrollCnt.ScrollStagePnl(dir);
    }

    // アクティブなPlayerを返すメソッド
    internal GameObject GetActivePlayer()
    {
        GameObject player = null;
        if (animator_playerL.gameObject.activeSelf)
        {
            player = animator_playerL.gameObject;
        }
        else
        {
            player = animator_playerR.gameObject;
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
        playerLCnt.isMoving = false;

        // PlayerL(PlayerR)のゲームオーバーアニメーションを再生
        Animator animator_player = GetActivePlayer().GetComponent<Animator>();
        animator_player.applyRootMotion = true;
        animator_player.SetBool("isOver", true);

        // Police移動開始
        police.SetActive(true);
    }

    // ゲーム操作禁止処理
    public void CantGameControl()
    {
        img_goBtn.enabled = false;
        img_backBtn.enabled = false;
        this.GetComponent<StageManager>().CantGameControl();
    }
}
