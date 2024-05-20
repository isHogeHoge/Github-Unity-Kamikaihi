using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrotherController_13 : MonoBehaviour
{
    [SerializeField] Image img_cakeBesideBrother;
    [SerializeField] Image img_cakeInFrontOfBrother;
    [SerializeField] Animator animator_display;
    [SerializeField] Image img_controllerItem;
    [SerializeField] Image img_brotherFoot;
    [SerializeField] Button controllerBtn;
    [SerializeField] Image img_brotherBtn;
    [SerializeField] GameObject itemManager;
    [SerializeField] Sprite cakeItemSpr;
    [SerializeField] Sprite forkItemSpr;

    private ItemManager im;
    private Animator animator_brother;
    private bool usedCakeItem = false; // ケーキアイテム使用フラグ

    private void Start()
    {
        im = itemManager.GetComponent<ItemManager>();
        animator_brother = this.GetComponent<Animator>();
    }

    // 接触判定(Item)
    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        Image img_item = col.GetComponent<Image>();
        // ケーキアイテム使用
        if (img_item.sprite == cakeItemSpr)
        {
            // フォークアイテムを使用可に
            usedCakeItem = true;

            // アイテム使用処理
            img_item.sprite = null;
            im.UsedItem();

            // 右隣にケーキをおく
            img_cakeBesideBrother.enabled = true;

            // ゲームアニメーション → ケーキの方を見るアニメーションに切り替え
            animator_brother.Play("BrotherTurn_Loop");
            animator_display.enabled = false;
            img_brotherBtn.enabled = false;

        }
        // フォークアイテム使用
        else if (img_item.sprite == forkItemSpr)
        {
            // ケーキアイテムを使用していたら、フォークアイテム使用判定に
            if(usedCakeItem)
            {
                // アイテム使用処理
                img_item.sprite = null;
                im.UsedItem();

                // コントローラーアイテム取得可能に
                img_controllerItem.enabled = true;
                controllerBtn.enabled = true;

                // ケーキを食べるアニメーション再生
                animator_brother.Play("BrotherEat");
                img_brotherFoot.enabled = true;
                // ケーキを手前に表示する
                img_cakeBesideBrother.enabled = false;
                img_cakeInFrontOfBrother.enabled = true;
            }

            
        }
    }
}
