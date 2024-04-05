using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CookieBtnController : MonoBehaviour
{
    // 自身がBoardかPlateの上にあるかで"usingChiliSauce"と"redCookieSpr"に代入するObj(Img)を変える
    [SerializeField] GameObject usingChiliSauce; // usingChiliSauce_Board or usingChiliSauce_Plate
    [SerializeField] GameObject itemManager;
    // -- アイテム画像 --
    [SerializeField] Sprite chiliSauceSpr;
    [SerializeField] Sprite chocolatePenSpr;
    // ----------------
    [SerializeField] Sprite redCookieSpr;    // 型抜きクッキーの生地(赤) or 焼き上がったクッキー(赤)&チョコペン使用× 画像
    [SerializeField] Sprite redCookie2Spr;    // 焼き上がったクッキー(赤)&チョコペン使用⚪︎ 画像
    [SerializeField] Sprite yellowCookie2Spr; // 焼き上がったクッキー(黄)&チョコペン使用⚪︎ 画像
    [SerializeField] bool wasBaked;          // クッキーが焼かれていたらture
     
    // 接触判定(Item)
    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        // チリソースアイテム使用
        if (col.GetComponent<Image>().sprite == chiliSauceSpr)
        {
            // アイテム使用処理
            col.GetComponent<Image>().sprite = null;
            itemManager.GetComponent<ItemManager>().UsedItem();

            // チリソース使用アニメーション再生
            usingChiliSauce.GetComponent<Animator>().enabled = true;

            // 生地を赤にする
            // チョコペンアイテムの使用状況を反映する
            // 事前にチョコペンアイテム使用済み
            if (this.GetComponent<Image>().sprite == yellowCookie2Spr)
            {
                this.GetComponent<Image>().sprite = redCookie2Spr;
            }
            // チョコペンアイテムを使用していない
            else
            {
                this.GetComponent<Image>().sprite = redCookieSpr;
            }
            
        }
        // チョコレートペンアイテム使用
        else if(col.GetComponent<Image>().sprite == chocolatePenSpr)
        {
            // 自身が生地の状態ならメソッドを抜ける
            if (wasBaked)
            {
                // アイテム使用処理
                col.GetComponent<Image>().sprite = null;
                itemManager.GetComponent<ItemManager>().UsedItem();

                // クッキーをチョコペン使用済みの画像に変更
                if (this.GetComponent<Image>().sprite == redCookieSpr)
                {
                    this.GetComponent<Image>().sprite = redCookie2Spr;
                }
                else
                {
                    this.GetComponent<Image>().sprite = yellowCookie2Spr;
                }
            }
            
        }
    }
}
