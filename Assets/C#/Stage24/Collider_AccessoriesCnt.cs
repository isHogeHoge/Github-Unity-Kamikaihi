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

    // 接触判定
    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        // 花輪アイテム使用
        if (col.GetComponent<Image>().sprite == garlandSpr)
        {
            // 飾りアイテム使用処理
            ActiveAccessory(col, garland);

            // すでにイチゴアイテムが使用されていたら
            if (strawberry.GetComponent<SpriteRenderer>().enabled)
            {
                // 自身がCentaurなら、フェードアウト
                if(this.transform.gameObject == centaur)
                {
                    this.GetComponent<Animator>().enabled = true;
                }
                // 自身がBrotherなら、Centaurに追いかけられる
                else if(this.transform.gameObject == brother)
                {
                    Chased();
                }
            }

        }
        // イチゴアイテム使用
        else if (col.GetComponent<Image>().sprite == strawberrySpr)
        {
            // 飾りアイテム使用処理
            ActiveAccessory(col, strawberry);

            // すでに花輪アイテムが使用されていたら
            if (garland.GetComponent<SpriteRenderer>().enabled)
            {
                // 自身がCentaurなら、フェードアウト
                if (this.transform.gameObject == centaur)
                {
                    this.GetComponent<Animator>().enabled = true;
                }
                // 自身がBrotherなら、Centaurに追いかけられる
                else if (this.transform.gameObject == brother)
                {
                    Chased();
                }
            }
        }
    }
    // 飾り(花輪・イチゴ)アイテム使用処理
    private void ActiveAccessory(Collider2D col,GameObject obj) // 接触したアイテム,表示する飾り
    {
        // アイテム使用処理
        col.GetComponent<Image>().sprite = null;
        itemManager.GetComponent<ItemManager>().UsedItem();

        // 自身に飾りアイテムを被せる
        obj.GetComponent<SpriteRenderer>().enabled = true;
    }

    // BrotherがCentaurに追いかけられる処理
    private void Chased()
    {
        // Centaurに追いかけられるアニメーション再生
        this.GetComponent<Animator>().SetBool("ChasedFlag", true);
        // Centaur移動開始
        centaur.SetActive(false);
        chasingCentaur.SetActive(true);
        chasingCentaur.GetComponent<ChasingCentaurCnt>().isChasing = true;
    }
}
