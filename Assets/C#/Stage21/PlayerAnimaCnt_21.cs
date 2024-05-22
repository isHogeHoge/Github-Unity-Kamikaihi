using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerAnimaCnt_21 : MonoBehaviour
{
    [SerializeField] GameObject smallEnemyBtn; // SmallEnemyを風呂に落とすボタン
    [SerializeField] Button treasureBtn;
    [SerializeField] GameObject goBtn;
    [SerializeField] GameObject clickCancelPnl;
    [SerializeField] GameObject smallEnemy;
    [SerializeField] SpriteRenderer sr_collapsingSmallEnemy;
    [SerializeField] SpriteRenderer sr_fallingTreasure;
    [SerializeField] GameObject bigEnemy;
    [SerializeField] SpriteRenderer sr_holeInTheWall; // BigEnemyが入っていた壁穴
    [SerializeField] GameObject stagePanel_UI;
    [SerializeField] GameObject stagePanel;
    [SerializeField] GameObject stageManager;
    [SerializeField] Sprite holeInTheWallSpr; // 壁穴(空)の画像

    private StageScrollCnt scrollCnt;
    private StageScrollCnt scrollCnt_UI;
    private SpriteRenderer sr_smallEnemy;
    private void Start()
    {
        scrollCnt = stagePanel.GetComponent<StageScrollCnt>();
        scrollCnt_UI = stagePanel_UI.GetComponent<StageScrollCnt>();
        sr_smallEnemy = smallEnemy.GetComponent<SpriteRenderer>();
    }

    // "PlayerIsSurpriced1"アニメーション終了時
    // SmallEnemy出現
    private void SmallEnemyAppear()
    {
        // すでにSmallEnemyが出現済みならメソッドを抜ける
        if (sr_smallEnemy.enabled)
        {
            return;
        }
        sr_smallEnemy.enabled = true;
        smallEnemy.GetComponent<Animator>().enabled = true;

    }

    // "PlayerIsSurpriced2(3)"アニメーション再生後
    // BigEnemy出現
    private void BigEnemyAppear()
    {
        // 壁穴の画像を空に
        sr_holeInTheWall.sprite = holeInTheWallSpr;
        // 出現 → Playerを叩くアニメーション再生
        bigEnemy.GetComponent<SpriteRenderer>().enabled = true;
        bigEnemy.GetComponent<Animator>().enabled = true;
        this.GetComponent<Animator>().Play("PlayerIsHitByBigEnemy");

    }

    // 橋(SmallEnemy)を渡った後
    private void InActiveSmallEnemyBtn()
    {
        smallEnemyBtn.SetActive(false);

    }

    // 歩行中
    private void ScrollStagePnl_Right()
    {
        // 右側のページにスクロール
        scrollCnt.ScrollStagePnl("RIGHT");
        scrollCnt_UI.ScrollStagePnl("RIGHT");

        
    }
    private void SmallEnemyMoveToTheOtherSide()
    {
        // SmallEnemyを向こう岸へ移動させる
        sr_smallEnemy.enabled = false;
        sr_collapsingSmallEnemy.enabled = true;
    }    

    // 逃走アニメーション再生中
    private void ScrollStagePnl_Left()
    {
        // 左側のページにスクロール
        scrollCnt.ScrollStagePnl("LEFT");
        scrollCnt_UI.ScrollStagePnl("LEFT");
    }

    // BigEnemyに叩かれるアニメーション開始時
    private void TreasureFall()
    {
        // 宝を取得していたら、宝が手元から落ちる
        if (!treasureBtn.enabled)
        {
            sr_fallingTreasure.enabled = true;
        }
    }

}
