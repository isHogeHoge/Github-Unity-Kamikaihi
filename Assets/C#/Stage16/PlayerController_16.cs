using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;
public class PlayerController_16 : MonoBehaviour
{
    [SerializeField] Animator animator_centaur1;
    [SerializeField] Image img_player2;   // Player_PlayingTheGame
    [SerializeField] GameObject fadePanel;
    [SerializeField] GameObject stageManager;
    [SerializeField] GameObject itemManager;
    [SerializeField] Sprite scissorsSpr;

    private StageManager sm;
    private FadeInAndOut fadeCnt;
    private void Start()
    {
        sm = stageManager.GetComponent<StageManager>();
        fadeCnt = fadePanel.GetComponent<FadeInAndOut>();
    }

    // 接触判定(Item)
    private async void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        Image img_item = col.GetComponent<Image>();
        // ハサミアイテム使用
        if (img_item.sprite == scissorsSpr)
        {
            // アイテム使用処理
            img_item.sprite = null;
            itemManager.GetComponent<ItemManager>().UsedItem();

            // 場面切り替え(フェードイン&アウト)
            await FadeInAndOut(this.GetCancellationTokenOnDestroy());

            // ゲームクリア処理
            await sm.GameClear(16, this.GetCancellationTokenOnDestroy());
        }


    }

    // 場面切り替え(フェードイン&アウト)
    private async UniTask FadeInAndOut(CancellationToken ct)
    {
        // ゲーム操作をできないようにする
        sm.CantGameControl();

        // -------- 場面切り替え処理 ----------
        // フェードイン
        await fadeCnt.FadeIn(ct);

        // 画面左側にスクロール
        stageManager.GetComponent<StageScrollCnt>().ScrollStagePnl("LEFT");
        img_player2.enabled = true;

        // フェードアウト
        await fadeCnt.FadeOut(ct);
        // ---------------------------------
    }

    // -------- Animation --------
    // Centaur1にポテチを渡した後
    private void PlayCentaur1TryToOpenAnima()
    {
        // Centaur1がポテチを開けるアニメーション再生
        animator_centaur1.Play("Centaur1TryToOpen");
    }
    // ---------------------------
}
