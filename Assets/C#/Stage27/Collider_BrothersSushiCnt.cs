using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Collider_BrothersSushiCnt : MonoBehaviour
{
    [SerializeField] GameObject itemManager;
    // アイテム画像
    [SerializeField] Sprite shrimpSpr1; // えび寿司(わさびあり)画像
    [SerializeField] Sprite shrimpSpr2; // えび寿司(わさび抜き)画像

    private SpriteRenderer sr_brothersSushi;
    private void Start()
    {
        sr_brothersSushi = this.GetComponent<SpriteRenderer>();
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)または(皿に別の寿司がある)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0) || sr_brothersSushi.enabled)
        {
            return;
        }

        Sprite itemSpr = col.GetComponent<Image>().sprite;
        // アイテム使用処理
        col.GetComponent<Image>().sprite = null;
        itemManager.GetComponent<ItemManager>().UsedItem();

        // 使用した寿司を皿に表示
        // えび寿司はわさびありの画像に固定
        if(itemSpr == shrimpSpr1 || itemSpr == shrimpSpr2)
        {
            sr_brothersSushi.sprite = shrimpSpr1;
        }
        // それ以外の寿司はアイテム画像をそのまま代入
        else
        {
            sr_brothersSushi.sprite = itemSpr;
        }
        sr_brothersSushi.enabled = true;

        // 再度クリックした時のアイテム画像を設定
        this.GetComponent<SushiController>().sushiSpr = itemSpr;

    }
}
