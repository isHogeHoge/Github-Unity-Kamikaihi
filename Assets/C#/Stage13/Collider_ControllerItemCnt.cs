using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collider_ControllerItemCnt : MonoBehaviour
{
    [SerializeField] GameObject itemManager;
    [SerializeField] GameObject clearPanel;
    [SerializeField] Sprite controllerItemSpr;

    // 接触判定(Item)
    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        // コントローラーアイテム使用
        if (col.GetComponent<Image>().sprite == controllerItemSpr)
        {
            // アイテム使用処理
            col.GetComponent<Image>().sprite = null;
            itemManager.GetComponent<ItemManager>().UsedItem();

            // 中央のページに「あける」or「かいひ」の選択画面を表示
            clearPanel.SetActive(true);

        }
    }
}
