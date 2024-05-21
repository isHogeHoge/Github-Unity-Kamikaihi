using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BrotherController_20 : MonoBehaviour
{
    [SerializeField] Button chocolatePenBtn; // チョコペンアイテムボタン
    [SerializeField] Button brotherBtn;
    [SerializeField] SpriteRenderer sr_chocolatePen;    // チョコペンアイテム画像
    [SerializeField] GameObject itemManager;
    // アイテム画像
    [SerializeField] Sprite candySpr;

    // 接触判定(Item)
    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        Image img_item = col.GetComponent<Image>();
        // キャンディーアイテム使用
        if (img_item.sprite == candySpr)
        {
            // アイテム使用処理
            img_item.sprite = null;
            itemManager.GetComponent<ItemManager>().UsedItem();

            // キャンディー取得アニメーションを再生
            brotherBtn.enabled = false;
            this.GetComponent<Animator>().Play("BrotherGetCandy");

            // チョコペンアイテム出現
            sr_chocolatePen.enabled = true;
            chocolatePenBtn.enabled = true;

        }
    }
}
