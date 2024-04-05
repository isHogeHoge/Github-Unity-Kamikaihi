using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player1Controller_19 : MonoBehaviour
{
    [SerializeField] GameObject clickCancelPnl;
    [SerializeField] GameObject itemManager;
    [SerializeField] GameObject stageManager;
    // アイテム画像
    [SerializeField] Sprite smartPhoneSpr;
    [SerializeField] Sprite apronSpr;

    internal bool called = false;         // スマートフォン使用フラグ
    internal bool isWearingApron = false;    // エプロン着用フラグ

    // 接触判定(Item)
    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        // スマートフォンアイテム使用
        if (col.GetComponent<Image>().sprite == smartPhoneSpr)
        {
            // アイテム使用処理
            col.GetComponent<Image>().sprite = null;
            itemManager.GetComponent<ItemManager>().UsedItem(); 

            clickCancelPnl.SetActive(true);

            // Brotherにスマートフォンを渡すアニメーション再生
            this.GetComponent<Animator>().Play("PlayerPass");
            called = true;

        }
        // エプロンアイテム使用
        else if (col.GetComponent<Image>().sprite == apronSpr)
        {
            // アイテム使用処理
            col.GetComponent<Image>().sprite = null;
            itemManager.GetComponent<ItemManager>().UsedItem();

            // Playerにエプロン着用させる
            this.GetComponent<Animator>().Play("PlayerInAApronHope");
            isWearingApron = true;

        }
    }
}
