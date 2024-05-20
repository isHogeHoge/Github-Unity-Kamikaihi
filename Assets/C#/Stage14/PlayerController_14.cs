using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController_14 : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr_ballAtPlayersFeet;
    [SerializeField] SpriteRenderer sr_raisedSoccerBall;
    [SerializeField] SpriteRenderer sr_ballBesidePlayer;
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

        Image img_item = col.GetComponent<Image>();
        // サッカーボールアイテム使用
        if (img_item.sprite == soccerBallSpr)
        {
            // アイテム使用処理
            img_item.sprite = null;
            itemManager.GetComponent<ItemManager>().UsedItem();

            // サッカーボールを掲げるアニメーション再生
            this.GetComponent<Animator>().Play("PlayerRaiseABall");
            sr_raisedSoccerBall.enabled = true;
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
            sr_raisedSoccerBall.enabled = false;
            sr_ballAtPlayersFeet.enabled = true;
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
            sr_raisedSoccerBall.enabled = false;
            sr_ballAtPlayersFeet.enabled = false;
            sr_ballBesidePlayer.enabled = true;
        }
    }
    // Clearアニメーション開始時
    // サッカーボールアイテムを使用していたら、サッカーボールを掲げる位置に移動
    private void isActiveRaisedSoccerBall()
    {
        if (usedBallItem)
        {
            sr_ballAtPlayersFeet.enabled = false;
            sr_raisedSoccerBall.enabled = true;
        }
    }
    // -----------------------------------
}
