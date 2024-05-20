using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController_10 : MonoBehaviour
{
    [SerializeField] GameObject itemManager;
    [SerializeField] GameObject cancelPnl;
    [SerializeField] GameObject ball;
    [SerializeField] GameObject magicCircle;
    [SerializeField] Animator animator_friend;
    [SerializeField] Sprite linecarSpr;

    private Animator animator_player;
    private void Start()
    {
        animator_player = this.GetComponent<Animator>();
    }
    // --------- Animation ---------
    // ボールを投げるモーション終了時
    // ボールがクリックしたEnemyの方に向かっていくアニメーション再生
    private void BallIsThrowedAtEnemy()
    {
        ball.GetComponent<Image>().enabled = true;
        ball.GetComponent<Animator>().SetBool("PlayerThrow", true);
    }

    // Friendにボールを渡した後
    // Friendのアニメーション切り替え
    private void PlayFriendStopAnima()
    {
        animator_friend.Play("FriendStop");
    }
    // 再度ボールを渡すアニメーションが再生されないようにする
    private void CantPassABall()
    {
        animator_player.SetBool("canPass", false);
    }
    // -----------------------------

    // 接触判定(Item)
    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        Image img_item = col.GetComponent<Image>();
        // ラインカーアイテム使用
        if (img_item.sprite == linecarSpr)
        {
            // アイテム使用処理
            img_item.sprite = null;
            itemManager.GetComponent<ItemManager>().UsedItem();

            // ゲーム操作できないようにする
            cancelPnl.SetActive(true);

            // 魔法陣出現アニメーション再生
            animator_player.Play("PlayerDrawR");
            magicCircle.SetActive(true);
        }

    }
}
