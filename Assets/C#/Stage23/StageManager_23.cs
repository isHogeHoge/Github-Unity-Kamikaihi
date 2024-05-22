using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager_23 : MonoBehaviour
{
    [SerializeField] GameObject clickCancelPnl;
    [SerializeField] BoxCollider2D boxCol_gatyaL;
    [SerializeField] BoxCollider2D boxCol_gatyaR;
    [SerializeField] SpriteRenderer sr_posterL;
    [SerializeField] SpriteRenderer sr_posterR;
    [SerializeField] GameObject itemManager;

    private ItemManager im;
    private bool isEnabled_GatyaLCol = false; // GatyaLにアタッチされているコライダーのアクティブ状態
    private bool isEnabled_GatyaRCol = false; // GatyaRにアタッチされているコライダーのアクティブ状態
    private void Start()
    {
        im = itemManager.GetComponent<ItemManager>();
    }
    // --------- Button ----------
    // コイン
    public void ClickCoinBtn(SpriteRenderer sr_coin) // coin1~3
    {
        // アイテム所持数がMax(5)ならメソッドを抜ける
        if (im.isFull)
        {
            return;
        }

        // コインの画像を非表示に
        sr_coin.enabled = false;
    }
    // 「故障中」ポスター
    public void ClickPosterBtn(string poster)
    {
        // アイテム所持数がMax(5)ならメソッドを抜ける
        if (im.isFull)
        {
            return;
        }

        // ガチャの前からポスターを剥がし、100円アイテム使用可能に
        switch (poster)
        {
            case "PosterL":
                sr_posterL.enabled = false;
                boxCol_gatyaL.enabled = true;
                break;
            case "PosterR":
                sr_posterR.enabled = false;
                boxCol_gatyaR.enabled = true;
                break;
            default:
                Debug.Log($"{poster}は無効な文字列です");
                    break;
        }
    }
    // ----------------------------

    // ゲーム操作をできないようにする
    internal void CantGameControl()
    {
        // ガチャにアタッチされているコライダーの状態を保存
        isEnabled_GatyaLCol = boxCol_gatyaL.enabled;
        isEnabled_GatyaRCol = boxCol_gatyaR.enabled;

        clickCancelPnl.SetActive(true);
        boxCol_gatyaL.enabled = false;
        boxCol_gatyaR.enabled = false;
    }
    // ゲーム操作を可能にする
    internal void CanGameControl()
    {
        clickCancelPnl.SetActive(false);
        // コライダーの状態をゲーム操作禁止前に戻す
        boxCol_gatyaL.enabled = isEnabled_GatyaLCol;
        boxCol_gatyaR.enabled = isEnabled_GatyaRCol;
    }
}
