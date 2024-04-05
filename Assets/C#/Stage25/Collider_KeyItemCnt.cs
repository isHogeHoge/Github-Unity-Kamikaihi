using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Collider_KeyItemCnt : MonoBehaviour
{
    [SerializeField] GameObject doorBtn;  // CDPlayer部屋のドア開扉ボタン
    [SerializeField] GameObject collider_CDItem; // CDアイテム接触用コライダー 
    [SerializeField] GameObject itemManager;
    // アイテム画像
    [SerializeField] Sprite keyItemSpr;

    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        // 鍵アイテム使用
        if (col.GetComponent<Image>().sprite == keyItemSpr)
        {
            // アイテム使用処理
            col.GetComponent<Image>().sprite = null;
            itemManager.GetComponent<ItemManager>().UsedItem();

            // CDPlayer部屋のドアを開扉できるようにする
            doorBtn.GetComponent<Button>().enabled = true;

            // CDアイテムを使用できるようにする
            collider_CDItem.SetActive(true);
        }
    }
}
