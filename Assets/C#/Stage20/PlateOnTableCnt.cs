using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlateOnTableCnt : MonoBehaviour
{
    [SerializeField] GameObject cookieBtn;
    [SerializeField] GameObject itemManager;
    // アイテム画像
    [SerializeField] Sprite yellowCookie1Spr;  // 焼き上がったクッキー(黄色)&チョコペン使用× 画像
    [SerializeField] Sprite redCookie1Spr;     // 焼き上がったクッキー(赤色)&チョコペン使用× 画像

    // 接触判定(Item)
    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        Sprite sprite = col.GetComponent<Image>().sprite;
        //「焼き上がったクッキー(チョコペン使用×)」アイテム使用
        if (sprite == yellowCookie1Spr || sprite == redCookie1Spr)
        {
            // アイテム使用処理
            col.GetComponent<Image>().sprite = null;
            itemManager.GetComponent<ItemManager>().UsedItem();

            // クッキーアイテムを取得できるように
            cookieBtn.SetActive(true);
        }
    }

}
