using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController_14 : MonoBehaviour
{
    [SerializeField] GameObject car;
    [SerializeField] GameObject ballAtPlayersFeet;
    [SerializeField] GameObject raisedSoccerBall;
    [SerializeField] GameObject ballBesidePlayer;
    [SerializeField] GameObject stageManager;
    [SerializeField] GameObject itemManager;
    [SerializeField] Sprite soccerBallSpr;

    internal bool usedBallItem = false;   // サッカーボールアイテム使用フラグ

    // 接触判定(Item)
    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        // サッカーボールアイテム使用
        if (col.GetComponent<Image>().sprite == soccerBallSpr)
        {
            // アイテム使用処理
            col.GetComponent<Image>().sprite = null;
            itemManager.GetComponent<ItemManager>().UsedItem();

            // サッカーボールを掲げるアニメーション再生
            this.GetComponent<Animator>().Play("PlayerRaiseABall");
            raisedSoccerBall.GetComponent<SpriteRenderer>().enabled = true;
            usedBallItem = true;
        }
    }

    // ------------ Animation ------------
    // 車の方を見るアニメーション開始時("PlayerStop")
    // サッカーボールアイテムを使用していたら、サッカーボールを足元に移動
    private void isActiveBallAtPlayersFeet()
    {
        if (usedBallItem)
        {
            raisedSoccerBall.GetComponent<SpriteRenderer>().enabled = false;
            ballAtPlayersFeet.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    // ゲームオーバー(クリア)アニメーション開始時
    // ゲーム操作を禁止に
    private void CantGameControl()
    {
        stageManager.GetComponent<StageManager>().CantGameControl();
    }
    // Overアニメーション開始時
    // サッカーボールアイテムを使用していたら、サッカーボールをPlayerの横に移動
    private void isActiveBallBesidePlayer()
    {
        if (usedBallItem)
        {
            raisedSoccerBall.GetComponent<SpriteRenderer>().enabled = false;
            ballAtPlayersFeet.GetComponent<SpriteRenderer>().enabled = false;
            ballBesidePlayer.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    // Clearアニメーション開始時
    // サッカーボールアイテムを使用していたら、サッカーボールを掲げる位置に移動
    private void isActiveRaisedSoccerBall()
    {
        if (usedBallItem)
        {
            ballAtPlayersFeet.GetComponent<SpriteRenderer>().enabled = false;
            raisedSoccerBall.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    // -----------------------------------
}
