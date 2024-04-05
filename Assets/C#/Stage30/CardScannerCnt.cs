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
    [SerializeField] GameObject door;
    [SerializeField] GameObject itemManager;
    [SerializeField] GameObject stageManager;
    // アイテム画像
    [SerializeField] Sprite sliverCardSpr;

    // 接触判定(Item)
    private async void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        // シルバーカードアイテム使用
        if (col.GetComponent<Image>().sprite == sliverCardSpr)
        {
            // アイテム使用処理
            col.GetComponent<Image>().sprite = null;
            itemManager.GetComponent<ItemManager>().UsedItem();

            stageManager.GetComponent<StageManager_30>().CantGameControl();

            // カードをスキャンするアニメーション再生
            sliverCard.GetComponent<SpriteRenderer>().enabled = true;
            sliverCard.GetComponent<Animator>().enabled = true;
            await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: this.GetCancellationTokenOnDestroy());

            // ドアが開くアニメーション再生
            door.GetComponent<Animator>().enabled = true;
            await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: this.GetCancellationTokenOnDestroy());

            // Playerが逮捕されるアニメーション再生(ゲームオーバー)
            // Playerが画面左側にいるならスクロール
            GameObject player = stageManager.GetComponent<StageManager_30>().GetActivePlayer();
            if (player == playerL)
            {
                stageManager.GetComponent<StageManager_30>().ScrollStagePnl("LEFT");
            }
            player.GetComponent<Animator>().applyRootMotion = true;
            player.GetComponent<Animator>().SetBool("isArrested", true);
        }

    }
    
}
