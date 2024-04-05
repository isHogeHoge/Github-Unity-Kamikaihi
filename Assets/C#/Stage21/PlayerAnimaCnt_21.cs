using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerAnimaCnt_21 : MonoBehaviour
{
    [SerializeField] GameObject smallEnemyBtn; // SmallEnemyを風呂に落とすボタン
    [SerializeField] GameObject goBtn;
    [SerializeField] GameObject treasureBtn;
    [SerializeField] GameObject smallEnemy;
    [SerializeField] GameObject collapsingSmallEnemy;
    [SerializeField] GameObject bigEnemy;
    [SerializeField] GameObject holeInTheWall; // BigEnemyが入っていた壁穴
    [SerializeField] GameObject fallingTreasure;
    [SerializeField] GameObject stagePanel_UI;
    [SerializeField] GameObject stagePanel;
    [SerializeField] GameObject clickCancelPnl;
    [SerializeField] GameObject stageManager;
    [SerializeField] Sprite holeInTheWallSpr; // 壁穴(空)の画像

    // "PlayerIsSurpriced1"アニメーション終了時
    // SmallEnemy出現
    private void SmallEnemyAppear()
    {
        // すでにSmallEnemyが出現済みならメソッドを抜ける
        if (smallEnemy.GetComponent<SpriteRenderer>().enabled)
        {
            return;
        }
        smallEnemy.GetComponent<SpriteRenderer>().enabled = true;
        smallEnemy.GetComponent<Animator>().enabled = true;

    }

    // "PlayerIsSurpriced2(3)"アニメーション再生後
    // BigEnemy出現
    private void BigEnemyAppear()
    {
        // 壁穴の画像を空に
        holeInTheWall.GetComponent<SpriteRenderer>().sprite = holeInTheWallSpr;
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
        stagePanel.GetComponent<StageScrollCnt>().ScrollStagePnl("RIGHT");
        stagePanel_UI.GetComponent<StageScrollCnt>().ScrollStagePnl("RIGHT");

        
    }
    private void SmallEnemyMoveToTheOtherSide()
    {
        // SmallEnemyを向こう岸へ移動させる
        smallEnemy.GetComponent<SpriteRenderer>().enabled = false;
        collapsingSmallEnemy.GetComponent<SpriteRenderer>().enabled = true;
    }    

    // 逃走アニメーション再生中
    private void ScrollStagePnl_Left()
    {
        // 左側のページにスクロール
        stagePanel.GetComponent<StageScrollCnt>().ScrollStagePnl("LEFT");
        stagePanel_UI.GetComponent<StageScrollCnt>().ScrollStagePnl("LEFT");
    }

    // BigEnemyに叩かれるアニメーション開始時
    private void TreasureFall()
    {
        // 宝を取得していたら、宝が手元から落ちる
        if (!treasureBtn.GetComponent<Button>().enabled)
        {
            fallingTreasure.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

}
