using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookedDoughtAnimaCnt : MonoBehaviour
{
    [SerializeField] Animator animator_cookieCutter;
    [SerializeField] SpriteRenderer sr_dough_OnBoard;
    [SerializeField] GameObject cookieDoughBtn;
    [SerializeField] GameObject bakedCookieBtn;
    [SerializeField] GameObject plateOnTable;

    // ++++++ FinishedDough ++++++
    // Boardの上まで移動後
    private void StartCuttingOut()
    {
        // 生地の型抜き開始
        // boardの上にある生地を表示
        sr_dough_OnBoard.enabled = true;
        // cookieCutterのアニメーション再生
        animator_cookieCutter.enabled = true;
    }
    // +++++++++++++++++++++++++++

    // ++++++ CookieCutter ++++++
    // "CookieCutter_Up"アニメーション再生中
    private void CookieDoughIsFinished()
    {
        // 型抜きクッキー(生地)完成
        // boardの上にある生地を非表示
        sr_dough_OnBoard.enabled = false;
        // アイテム取得 & チリソースアイテム使用可能に
        cookieDoughBtn.GetComponent<Image>().enabled = true;
        cookieDoughBtn.GetComponent<BoxCollider2D>().enabled = true;

    }
    // ++++++++++++++++++++++++++

    // +++ PlateInTheFirePlace +++
    // クッキーを焼くアニメーション終了時
    private void BakedCookieIsFinished()
    {
        // テーブルの上にプレートを表示
        plateOnTable.GetComponent<SpriteRenderer>().enabled = true;
        // 焼き上がったクッキー(チョコペン使用×)アイテムを使用可能に
        plateOnTable.GetComponent<BoxCollider2D>().enabled = true;

        // 焼き上がったクッキーアイテムを取得可能に
        bakedCookieBtn.GetComponent<Image>().enabled = true;
        // チリソースアイテム使用可能に
        bakedCookieBtn.GetComponent<BoxCollider2D>().enabled = true;
    }
    // +++++++++++++++++++++++++++
}
