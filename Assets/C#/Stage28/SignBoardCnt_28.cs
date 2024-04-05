using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;
public class SignBoardCnt_28 : MonoBehaviour
{
    [SerializeField] GameObject itemManager;
    [SerializeField] Sprite signBoardItemSpr; // 看板アイテムの画像

    // アイテム接触判定
    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        // 看板アイテム使用
        if (col.GetComponent<Image>().sprite == signBoardItemSpr)
        {
            // アイテム使用処理
            col.GetComponent<Image>().sprite = null;
            itemManager.GetComponent<ItemManager>().UsedItem();

            // 店の前に「準備中」の看板を表示
            this.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
