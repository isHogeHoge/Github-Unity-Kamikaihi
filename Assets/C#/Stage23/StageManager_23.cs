using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager_23 : MonoBehaviour
{
    [SerializeField] GameObject clickCancelPnl;
    [SerializeField] GameObject gatyaL;
    [SerializeField] GameObject gatyaR;
    [SerializeField] GameObject posterL;
    [SerializeField] GameObject posterR;
    [SerializeField] GameObject itemManager;

    private bool isEnabled_GatyaLCol = false; // GatyaLにアタッチされているコライダーのアクティブ状態
    private bool isEnabled_GatyaRCol = false; // GatyaRにアタッチされているコライダーのアクティブ状態
    // コインをクリックした時
    public void ClickCoinBtn(GameObject coin) // coin1~3
    {
        // アイテム所持数がMax(5)ならメソッドを抜ける
        if (itemManager.GetComponent<ItemManager>().isFull)
        {
            return;
        }

        // コインの画像を非表示に
        coin.GetComponent<SpriteRenderer>().enabled = false;
    }
    // 「故障中」ポスターをクリックした時
    public void ClickPosterBtn(string poster)
    {
        // アイテム所持数がMax(5)ならメソッドを抜ける
        if (itemManager.GetComponent<ItemManager>().isFull)
        {
            return;
        }

        // ガチャの前からポスターを剥がし、100円アイテム使用可能に
        switch (poster)
        {
            case "PosterL":
                posterL.GetComponent<SpriteRenderer>().enabled = false;
                gatyaL.GetComponent<BoxCollider2D>().enabled = true;
                break;
            case "PosterR":
                posterR.GetComponent<SpriteRenderer>().enabled = false;
                gatyaR.GetComponent<BoxCollider2D>().enabled = true;
                break;
            default:
                Debug.Log($"{poster}は無効な文字列です");
                    break;
        }
    }

    // ゲーム操作をできないようにする
    internal void CantGameControl()
    {
        // ガチャにアタッチされているコライダーの状態を保存
        isEnabled_GatyaLCol = gatyaL.GetComponent<BoxCollider2D>().enabled;
        isEnabled_GatyaRCol = gatyaR.GetComponent<BoxCollider2D>().enabled;

        clickCancelPnl.SetActive(true);
        gatyaL.GetComponent<BoxCollider2D>().enabled = false;
        gatyaR.GetComponent<BoxCollider2D>().enabled = false;
    }
    // ゲーム操作を可能にする
    internal void CanGameControl()
    {
        clickCancelPnl.SetActive(false);
        // コライダーの状態をゲーム操作禁止前に戻す
        gatyaL.GetComponent<BoxCollider2D>().enabled = isEnabled_GatyaLCol;
        gatyaR.GetComponent<BoxCollider2D>().enabled = isEnabled_GatyaRCol;
    }
}
