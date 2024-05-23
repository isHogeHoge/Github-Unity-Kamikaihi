using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System;

public class CardScannerCnt : MonoBehaviour
{
    [SerializeField] GameObject playerL;
    [SerializeField] GameObject sliverCard;
    [SerializeField] Animator animator_door;
    [SerializeField] GameObject itemManager;
    [SerializeField] GameObject stageManager;
    // アイテム画像
    [SerializeField] Sprite sliverCardSpr;

    private StageManager_30 sm_30;
    private void Start()
    {
        sm_30 = stageManager.GetComponent<StageManager_30>();
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
        // シルバーカードアイテム使用
        if (img_item.sprite == sliverCardSpr)
        {
            // アイテム使用処理
            img_item.sprite = null;
            itemManager.GetComponent<ItemManager>().UsedItem();

            sm_30.CantGameControl();

            // カードをスキャンするアニメーション再生
            sliverCard.GetComponent<SpriteRenderer>().enabled = true;
            sliverCard.GetComponent<Animator>().enabled = true;
            await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: this.GetCancellationTokenOnDestroy());

            // ドアが開くアニメーション再生
            animator_door.enabled = true;
            await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: this.GetCancellationTokenOnDestroy());

            // Playerが逮捕されるアニメーション再生(ゲームオーバー)
            // Playerが画面左側にいるならスクロール
            GameObject player = sm_30.GetActivePlayer();
            if (player == playerL)
            {
                sm_30.ScrollStagePnl("LEFT");
            }
            Animator animator_player = player.GetComponent<Animator>();
            animator_player.applyRootMotion = true;
            animator_player.SetBool("isArrested", true);
        }

    }
    
}
