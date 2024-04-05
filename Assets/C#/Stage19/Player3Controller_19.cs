using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player3Controller_19 : MonoBehaviour
{
    [SerializeField] GameObject itemManager;
    [SerializeField] Sprite chopsticksSpr;  // 箸アイテム画像

    // 接触判定(Item)
    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        // 箸アイテム使用
        if (col.GetComponent<Image>().sprite == chopsticksSpr)
        {
            // アイテム使用処理
            col.GetComponent<Image>().sprite = null; 
            itemManager.GetComponent<ItemManager>().UsedItem();

            // Playerが箸を持つ→料理するアニメーション再生
            this.GetComponent<Animator>().Play("PlayerGetAChopsticks");

        }

    }
}
