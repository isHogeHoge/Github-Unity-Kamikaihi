using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BrotherController_20 : MonoBehaviour
{
    [SerializeField] GameObject brotherBtn;
    [SerializeField] GameObject chocolatePenBtn; // チョコペンアイテムボタン
    [SerializeField] GameObject chocolatePen;    // チョコペンアイテム画像
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
        
        // キャンディーアイテム使用
        if (col.GetComponent<Image>().sprite == candySpr)
        {
            // アイテム使用処理
            col.GetComponent<Image>().sprite = null;
            itemManager.GetComponent<ItemManager>().UsedItem();

            // キャンディー取得アニメーションを再生
            brotherBtn.GetComponent<Button>().enabled = false;
            this.GetComponent<Animator>().Play("BrotherGetCandy");

            // チョコペンアイテム出現
            chocolatePen.GetComponent<SpriteRenderer>().enabled = true;
            chocolatePenBtn.GetComponent<Button>().enabled = true;

        }
    }
}
