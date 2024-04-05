using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;
public class PlayerController_16 : MonoBehaviour
{
    [SerializeField] GameObject centaur1;
    [SerializeField] GameObject player2;   // Player_PlayingTheGame
    [SerializeField] GameObject rButton;
    [SerializeField] GameObject fadePanel;
    [SerializeField] GameObject stageManager;
    [SerializeField] GameObject itemManager;
    [SerializeField] Sprite scissorsSpr;  

    // 接触判定(Item)
    private async void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        // ハサミアイテム使用
        if (col.GetComponent<Image>().sprite == scissorsSpr)
        {
            // アイテム使用処理
            col.GetComponent<Image>().sprite = null;
            itemManager.GetComponent<ItemManager>().UsedItem();

            // 場面切り替え(フェードイン&アウト)
            await FadeInAndOut(this.GetCancellationTokenOnDestroy());

            // ゲームクリア処理
            await stageManager.GetComponent<StageManager>().GameClear(16, this.GetCancellationTokenOnDestroy());
        }


    }

    // 場面切り替え(フェードイン&アウト)
    private async UniTask FadeInAndOut(CancellationToken ct)
    {
        // ゲーム操作をできないようにする
        stageManager.GetComponent<StageManager>().CantGameControl();

        // -------- 場面切り替え処理 ----------
        // フェードイン
        await fadePanel.GetComponent<FadeInAndOut>().FadeIn(ct);

        // 画面左側にスクロール
        stageManager.GetComponent<StageScrollCnt>().ScrollStagePnl("LEFT");
        player2.GetComponent<Image>().enabled = true;

        // フェードアウト
        await fadePanel.GetComponent<FadeInAndOut>().FadeOut(ct);
        // ---------------------------------
    }

    // -------- Animation --------
    // Centaur1にポテチを渡した後
    private void PlayCentaur1TryToOpenAnima()
    {
        // Centaur1がポテチを開けるアニメーション再生
        centaur1.GetComponent<Animator>().Play("Centaur1TryToOpen");
    }
    // ---------------------------
}
