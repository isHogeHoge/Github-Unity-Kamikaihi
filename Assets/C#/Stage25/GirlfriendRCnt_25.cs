using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class GirlfriendRCnt_25 : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] GameObject speechBubble;  // 自身の吹き出し
    [SerializeField] GameObject itemManager;
    // --- アイテム画像 ---
    [SerializeField] Sprite chocolateItemSpr;  // チョコレートアイテムの画像
    [SerializeField] Sprite girlfriendItem1Spr;  // 自身がプレーンケーキを持っている画像
    [SerializeField] Sprite girlfriendItem2Spr;  // 自身がチョコレートケーキを持っている画像
    // ------------------
    [SerializeField] Sprite newSpeechBubbleSpr;  // 料理完成時に表示される吹き出しの画像

    internal bool usedChocolateItem = false; // チョコレートアイテム使用フラグ

    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        // チョコレートアイテム使用
        if (col.GetComponent<Image>().sprite == chocolateItemSpr)
        {
            // アイテム使用処理
            col.GetComponent<Image>().sprite = null;
            itemManager.GetComponent<ItemManager>().UsedItem();

            // チョコレートケーキを作るアニメーション開始
            this.GetComponent<Animator>().Play("GirlfriendIsCooking");
            speechBubble.GetComponent<SpriteRenderer>().enabled = false;
            speechBubble.GetComponent<SpriteRenderer>().sprite = newSpeechBubbleSpr;

            usedChocolateItem = true;
        }
    }

    // 自身をクリックした時、アイテムとして取得する
    public void OnPointerClick(PointerEventData eventData)
    {
        this.gameObject.SetActive(false);
        speechBubble.SetActive(false);

        // チョコレートケーキを持ったGirlfriendをアイテム画像に設定
        if (usedChocolateItem)
        {
            itemManager.GetComponent<ItemManager>().ClickItemBtn(girlfriendItem2Spr);
        }
        // プレーンケーキを持ったGirlfriendをアイテム画像に設定
        else
        {
            itemManager.GetComponent<ItemManager>().ClickItemBtn(girlfriendItem1Spr);
        }
    }
}
