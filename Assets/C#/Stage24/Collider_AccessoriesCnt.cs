using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Brother,Centaurにアタッチ
public class Collider_AccessoriesCnt : MonoBehaviour
{
    [SerializeField] GameObject garland;
    [SerializeField] GameObject strawberry;
    [SerializeField] GameObject brother;
    [SerializeField] GameObject centaur;    // 停止しているCentaur
    [SerializeField] GameObject chasingCentaur; // Brotherを追いかけるCentaur
    [SerializeField] GameObject itemManager;
    [SerializeField] Sprite garlandSpr;  // 花輪アイテム画像
    [SerializeField] Sprite strawberrySpr; // イチゴアイテム画像

    private Animator animator_this;
    private void Start()
    {
        animator_this = this.GetComponent<Animator>();
    }

    // 接触判定
    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        Image img_item = col.GetComponent<Image>();
        // 花輪アイテム使用
        if (img_item.sprite == garlandSpr)
        {
            // 飾りアイテム使用処理
            ActiveAccessory(img_item, garland);

            // すでにイチゴアイテムが使用されていたら
            if (strawberry.GetComponent<SpriteRenderer>().enabled)
            {
                // Centaurなら、フェードアウト
                if(this.transform.gameObject == centaur)
                {
                    animator_this.enabled = true;
                }
                // Brotherなら、Centaurに追いかけられる
                else if(this.transform.gameObject == brother)
                {
                    Chased();
                }
            }

        }
        // イチゴアイテム使用
        else if (img_item.sprite == strawberrySpr)
        {
            // 飾りアイテム使用処理
            ActiveAccessory(img_item, strawberry);

            // すでに花輪アイテムが使用されていたら
            if (garland.GetComponent<SpriteRenderer>().enabled)
            {
                // Centaurなら、フェードアウト
                if (this.transform.gameObject == centaur)
                {
                    animator_this.enabled = true;
                }
                // Brotherなら、Centaurに追いかけられる
                else if (this.transform.gameObject == brother)
                {
                    Chased();
                }
            }
        }
    }
    // 飾り(花輪・イチゴ)アイテム使用処理
    private void ActiveAccessory(Image img_item,GameObject obj) // 接触したアイテム,表示する飾り
    {
        // アイテム使用処理
        img_item.sprite = null;
        itemManager.GetComponent<ItemManager>().UsedItem();

        // 自身に飾りアイテムを被せる
        obj.GetComponent<SpriteRenderer>().enabled = true;
    }

    // BrotherがCentaurに追いかけられる処理
    private void Chased()
    {
        // Centaurに追いかけられるアニメーション再生
        animator_this.SetBool("ChasedFlag", true);
        // Centaur移動開始
        centaur.SetActive(false);
        chasingCentaur.SetActive(true);
        chasingCentaur.GetComponent<ChasingCentaurCnt>().isChasing = true;
    }
}
