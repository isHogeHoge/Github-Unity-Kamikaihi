using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;

public class FirePlaceController : MonoBehaviour
{
    [SerializeField] Image img_bakedCookieBtn;     
    [SerializeField] SpriteRenderer sr_plateWithTheDough;
    [SerializeField] Animator animator_plateInTheFirePlace;
    [SerializeField] GameObject itemManager;
    [SerializeField] Sprite yCookieDoughSpr;  // 黄色のクッキー(生地)画像
    [SerializeField] Sprite rCookieDoughSpr;  // 赤色のクッキー(生地)画像
    [SerializeField] Sprite yellowCookieSpr;  // 焼き上がったクッキー(黄色)画像
    [SerializeField] Sprite redCookieSpr;     // 焼き上がったクッキー(赤色)画像
    [SerializeField] Sprite plateWithYDoughSpr; // 黄色のクッキー(生地)がプレートの上にある画像
    [SerializeField] Sprite plateWithRDoughSpr; // 赤色のクッキー(生地)がプレートの上にある画像
    

    // 接触判定(Item)
    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        Image img_item = col.GetComponent<Image>();
        //「黄色のクッキー(生地)」アイテム使用
        if (img_item.sprite == yCookieDoughSpr)
        {
            // クッキー(黄色)を焼く
            BakeCookie(img_item, plateWithYDoughSpr, yellowCookieSpr, this.GetCancellationTokenOnDestroy()).Forget();
        }
        //「赤色のクッキー(生地)」アイテム使用
        else if (img_item.sprite == rCookieDoughSpr)
        {
            // クッキー(赤色)を焼く
            BakeCookie(img_item, plateWithRDoughSpr, redCookieSpr, this.GetCancellationTokenOnDestroy()).Forget();
        }
    }

    // クッキーを焼く処理
    private async UniTask BakeCookie(Image img_item, Sprite plateSpr, Sprite cookieSpr, CancellationToken ct)
    {
        // アイテム使用処理
        img_item.sprite = null;
        itemManager.GetComponent<ItemManager>().UsedItem();

        // クッキー(生地)が乗っているプレートを表示
        sr_plateWithTheDough.sprite = plateSpr;
        sr_plateWithTheDough.enabled = true;
        // 焼き上がったクッキーの画像を設定
        img_bakedCookieBtn.sprite = cookieSpr;

        await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: ct);

        // クッキーを焼くアニメーション再生
        sr_plateWithTheDough.enabled = false;
        animator_plateInTheFirePlace.enabled = true;
    }

}
