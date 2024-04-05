using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Collider_CDItemCnt : MonoBehaviour
{
    [SerializeField] GameObject cdPlayerBtn; // 音楽再生ボタン
    [SerializeField] GameObject musicalNotes; // 音符
    [SerializeField] GameObject itemManager;
    // アイテム画像
    [SerializeField] Sprite cdItemSpr;

    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        // CDアイテム使用
        if (col.GetComponent<Image>().sprite == cdItemSpr)
        {
            // アイテム使用処理
            col.GetComponent<Image>().sprite = null;
            itemManager.GetComponent<ItemManager>().UsedItem();

            // 音楽を鳴らす(アニメーション)
            musicalNotes.GetComponent<Animator>().enabled = true;
            musicalNotes.GetComponent<SpriteRenderer>().enabled = true;
            // 音楽再生&停止ボタン使用可能に
            cdPlayerBtn.SetActive(true);
        }
    }
}
