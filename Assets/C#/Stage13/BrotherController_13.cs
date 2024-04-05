using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrotherController_13 : MonoBehaviour
{
    [SerializeField] GameObject itemManager;
    [SerializeField] GameObject brotherFoot;
    [SerializeField] GameObject display;
    [SerializeField] GameObject cakeBesideBrother;
    [SerializeField] GameObject cakeInFrontOfBrother;
    [SerializeField] GameObject controllerItem;
    [SerializeField] GameObject controllerBtn;
    [SerializeField] GameObject brotherBtn;
    [SerializeField] Sprite cakeItemSpr;
    [SerializeField] Sprite forkItemSpr;

    private bool usedCakeItem = false; // ケーキアイテム使用フラグ

    // 接触判定(Item)
    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        // ケーキアイテム使用
        if (col.GetComponent<Image>().sprite == cakeItemSpr)
        {
            // フォークアイテムを使用可に
            usedCakeItem = true;

            // アイテム使用処理
            col.GetComponent<Image>().sprite = null;
            itemManager.GetComponent<ItemManager>().UsedItem();

            // 右隣にケーキをおく
            cakeBesideBrother.GetComponent<Image>().enabled = true;

            // ゲームアニメーション → ケーキの方を見るアニメーションに切り替え
            this.GetComponent<Animator>().Play("BrotherTurn_Loop");
            display.GetComponent<Animator>().enabled = false;
            brotherBtn.GetComponent<Image>().enabled = false;

        }
        // フォークアイテム使用
        else if (col.GetComponent<Image>().sprite == forkItemSpr)
        {
            // ケーキアイテムを使用していたら、フォークアイテム使用判定に
            if(usedCakeItem)
            {
                // アイテム使用処理
                col.GetComponent<Image>().sprite = null;
                itemManager.GetComponent<ItemManager>().UsedItem();

                // コントローラーアイテム取得可能に
                controllerItem.GetComponent<Image>().enabled = true;
                controllerBtn.GetComponent<Button>().enabled = true;

                // ケーキを食べるアニメーション再生
                this.GetComponent<Animator>().Play("BrotherEat");
                brotherFoot.GetComponent<Image>().enabled = true;
                // ケーキを手前に表示する
                cakeBesideBrother.GetComponent<Image>().enabled = false;
                cakeInFrontOfBrother.GetComponent<Image>().enabled = true;
            }

            
        }
    }
}
