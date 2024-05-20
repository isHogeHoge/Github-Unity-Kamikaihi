using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using System;
public class Friend1Controller_12 : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Animator animator_friend2;
    [SerializeField] SpriteRenderer sr_speechBubble;
    [SerializeField] GameObject playerAndFriend1;
    [SerializeField] GameObject itemManager;
    [SerializeField] GameObject stageManager;
    [SerializeField] Sprite candySpr;
    [SerializeField] Sprite flagSpr;
    [SerializeField] Sprite pendulumSpr;
    [SerializeField] Sprite SB_ClearSpr;  // クリア時の吹き出し画像
    [SerializeField] Sprite SB_OverSpr;   // オーバー時の吹き出し画像

    private ItemManager im;
    private StageManager sm;
    private Animator animator_friend1;

    private void Start()
    {
        im = itemManager.GetComponent<ItemManager>();
        sm = stageManager.GetComponent<StageManager>();
        animator_friend1 = this.GetComponent<Animator>();
    }

    // 接触判定(Item)
    private async void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }
        
        Sprite itemSpr = col.GetComponent<Image>().sprite;
        // アイテム使用処理
        col.GetComponent<Image>().sprite = null;
        im.UsedItem();
        // ゲーム操作を禁止に
        sm.CantGameControl();    

        // キャンディーアイテム
        if (itemSpr == candySpr)
        {
            // キャンディー取得アニメーションを再生
            animator_friend1.Play("Friend1EatACandy");
            animator_friend2.Play("Friend2EatACandy");

            // 吹き出しの画像変更
            sr_speechBubble.sprite = SB_ClearSpr;

            // Playerのゲームクリアアニメーションを再生
            await UniTask.Delay(TimeSpan.FromSeconds(1f),cancellationToken: this.GetCancellationTokenOnDestroy());
            player.GetComponent<Animator>().Play("PlayerClear_12");

            // ゲームクリア処理
            sm.GameClear(12, this.GetCancellationTokenOnDestroy()).Forget();
        }
        // フラッグアイテム
        else if (itemSpr == flagSpr)
        {
            PlayGameOverAnima("Friend1EatAFlag1");
        }
        // 振り子アイテム
        else if (itemSpr == pendulumSpr)
        {
            PlayGameOverAnima("Friend1EatAPendulum1");
        }


    }
    /// <summary>
    /// ゲームオーバーアニメーション再生
    /// </summary>
    /// <param name="animation">再生するアニメーション</param>
    private void PlayGameOverAnima(string animation)
    {
        // 吹き出しの画像変更
        sr_speechBubble.sprite = SB_OverSpr;

        // 使用されたアイテムを食べるアニメーション再生
        animator_friend1.Play($"{animation}");
    }

    // --------- Animation ---------
    //フラッグor振り子を食べるアニメーション後、Player&Friend1の切り替え
    private void PlayPlayerAndFriend1Anima()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
        player.GetComponent<SpriteRenderer>().enabled = false;
        playerAndFriend1.SetActive(true);
    }
    // -----------------------------

}
