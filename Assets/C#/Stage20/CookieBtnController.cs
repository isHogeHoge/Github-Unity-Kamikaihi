using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CookieBtnController : MonoBehaviour
{
    // CookieBtnがBoardまたはPlateの上にあるかで"usingChiliSauce"と"redCookieSpr"に代入するオブジェクトを変える
    [SerializeField] Animator animator_usingChiliSauce; // usingChiliSauce_Board or usingChiliSauce_Plate
    [SerializeField] GameObject itemManager;
    // -- アイテム画像 --
    [SerializeField] Sprite chiliSauceSpr;
    [SerializeField] Sprite chocolatePenSpr;
    // ----------------
    [SerializeField] Sprite redCookieSpr;    // 型抜きクッキーの生地(赤) or 焼き上がったクッキー(赤)&チョコペン使用× 画像
    [SerializeField] Sprite redCookie2Spr;    // 焼き上がったクッキー(赤)&チョコペン使用⚪︎ 画像
    [SerializeField] Sprite yellowCookie2Spr; // 焼き上がったクッキー(黄)&チョコペン使用⚪︎ 画像
    [SerializeField] bool wasBaked;          // クッキーが焼かれていたらture

    private ItemManager im;
    private Image img_cookieBtn;
    private void Start()
    {
        im = itemManager.GetComponent<ItemManager>();
        img_cookieBtn = this.GetComponent<Image>();
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
        // チリソースアイテム使用
        if (img_item.sprite == chiliSauceSpr)
        {
            // アイテム使用処理
            img_item.sprite = null;
            im.UsedItem();

            // チリソース使用アニメーション再生
            animator_usingChiliSauce.enabled = true;

            // 生地を赤にする
            // チョコペンアイテムの使用状況を反映する
            // 事前にチョコペンアイテム使用済み
            if (img_cookieBtn.sprite == yellowCookie2Spr)
            {
                img_cookieBtn.sprite = redCookie2Spr;
            }
            // チョコペンアイテムを使用していない
            else
            {
                img_cookieBtn.sprite = redCookieSpr;
            }
            
        }
        // チョコレートペンアイテム使用
        else if(img_item.sprite == chocolatePenSpr)
        {
            // 自身が生地の状態ならメソッドを抜ける
            if (wasBaked)
            {
                // アイテム使用処理
                img_item.sprite = null;
                im.UsedItem();

                // クッキーをチョコペン使用済みの画像に変更
                if (img_cookieBtn.sprite == redCookieSpr)
                {
                    img_cookieBtn.sprite = redCookie2Spr;
                }
                else
                {
                    img_cookieBtn.sprite = yellowCookie2Spr;
                }
            }
            
        }
    }
}
