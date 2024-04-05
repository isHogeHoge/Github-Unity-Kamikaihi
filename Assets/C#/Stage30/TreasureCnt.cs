using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TreasureCnt : MonoBehaviour
{
    [SerializeField] GameObject playerR;
    [SerializeField] GameObject itemManager;
    // アイテム画像
    [SerializeField] Sprite furoshikiSpr; // 風呂敷
    // 接触判定(Item)
    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        // 風呂敷アイテム使用
        if (col.GetComponent<Image>().sprite == furoshikiSpr)
        {
            // アイテム使用処理
            col.GetComponent<Image>().sprite = null;
            itemManager.GetComponent<ItemManager>().UsedItem();

            // 宝を取得
            this.GetComponent<SpriteRenderer>().enabled = false;
            playerR.GetComponent<Animator>().Play("PlayerGetATreasure");

            // Playerゲームオーバー時、足元の宝が表示されるように設定
            playerR.transform.GetChild(0).gameObject.SetActive(true);

        }

    }
}
