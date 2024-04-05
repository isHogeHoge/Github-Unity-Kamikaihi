using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutankhamunCnt : MonoBehaviour
{
    [SerializeField] GameObject tutankhamunBtn;
    [SerializeField] GameObject itemManager;
    // アイテム画像
    [SerializeField] Sprite goldCardSpr;

    // 接触判定(Item)
    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        // ゴールドカードアイテム使用
        if (col.GetComponent<Image>().sprite == goldCardSpr)
        {
            // アイテム使用処理
            col.GetComponent<Image>().sprite = null;
            itemManager.GetComponent<ItemManager>().UsedItem();

            // 自身がカードを飲み込むアニメーション再生
            this.GetComponent<Animator>().Play("TutankhamunSwallowACard");
        }
        
    }
    // ------- Animation --------
    // カードを飲み込んだ後、自身を開閉できるボタンを使用可能に
    private void ActiveTutankhamunBtn()
    {
        tutankhamunBtn.GetComponent<Button>().enabled = true;
    }
    // --------------------------
}
