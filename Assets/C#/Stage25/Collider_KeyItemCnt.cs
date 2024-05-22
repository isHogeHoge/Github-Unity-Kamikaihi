using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Collider_KeyItemCnt : MonoBehaviour
{
    [SerializeField] Button btn_openCDPlayersDoor;
    [SerializeField] GameObject collider_CDItem;
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

        Image img_item = col.GetComponent<Image>();
        // 鍵アイテム使用
        if (img_item.sprite == keyItemSpr)
        {
            // アイテム使用処理
            img_item.sprite = null;
            itemManager.GetComponent<ItemManager>().UsedItem();

            // CDPlayer部屋のドアを開扉できるようにする
            btn_openCDPlayersDoor.enabled = true;

            // CDアイテムを使用できるようにする
            collider_CDItem.SetActive(true);
        }
    }
}
