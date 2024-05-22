using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController_24 : MonoBehaviour
{
    [SerializeField] GameObject itemManager;
    [SerializeField] GameObject stageManager;
    // アイテム画像
    [SerializeField] Sprite maskItemSpr;

    internal bool wearingMask = false;  // Playerがカッパマスク着用フラグ

    // 接触判定(Item)
    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        Image img_item = col.GetComponent<Image>();
        // カッパマスクアイテム使用
        if (img_item.sprite == maskItemSpr)
        {
            // アイテム使用処理
            img_item.sprite = null;
            itemManager.GetComponent<ItemManager>().UsedItem();

            // カッパマスク着用フラグON
            wearingMask = true;
            this.GetComponent<Animator>().SetBool("MaskFlag", true);

        }
    }
}
