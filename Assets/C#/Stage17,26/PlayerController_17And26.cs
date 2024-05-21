using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController_17And26 : MonoBehaviour
{
    [SerializeField] Animator animator_friend1;
    [SerializeField] Image img_helmet;
    [SerializeField] GameObject stageManager;
    [SerializeField] GameObject itemManager;
    // アイテム画像
    [SerializeField] Sprite watermelonSpr; 
    [SerializeField] Sprite clabSpr;       
    [SerializeField] Sprite helmetSpr;     

    // 接触判定(Item)
    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        Sprite itemSpr = col.GetComponent<Image>().sprite;

        // アイテム消費処理
        col.GetComponent<Image>().sprite = null;
        itemManager.GetComponent<ItemManager>().UsedItem();
        // ゲーム操作できないようにする
        stageManager.GetComponent<StageManager>().CantGameControl();

        // スイカアイテム使用
        if (itemSpr == watermelonSpr)
        {
            // Playerの頭の上にスイカを表示 & Friend1に叩かれる
            this.transform.GetChild(0).GetComponent<Image>().enabled = true;
            animator_friend1.Play("Friend1Swing1");

        }
        // カニアイテム使用
        else if (itemSpr == clabSpr)
        {
            // ゲームオーバーアニメーション再生
            this.GetComponent<Animator>().Play("PlayerOver2");
        }
        // ヘルメットアイテム使用
        else if(itemSpr == helmetSpr)
        {
            // ヘルメット装着 & Friend1に叩かれる
            img_helmet.enabled = true;
            animator_friend1.Play("Friend1Swing1");
        }

    }

}
