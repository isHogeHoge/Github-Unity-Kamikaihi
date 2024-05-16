using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class Collider_LoupeItemCnt : MonoBehaviour
{
    [SerializeField] GameObject creamPuffImg;      // 吹き出し内のシュークリーム画像
    [SerializeField] GameObject creamPuffWithLoupeImg; // 吹き出し内のシュークリーム&ルーペ画像
    [SerializeField] GameObject itemManager;
    [SerializeField] GameObject stageManager;
    [SerializeField] Sprite loupeItemSpr;

    internal int indexOfSpicyCf; // からし入りシュークリームのインデックス
    internal bool usedLoupe;      // ルーペ使用フラグ
    private ItemManager im;
    private RotateFoodsCnt rfc;

    private void Start()
    {
        usedLoupe = false;
        im = itemManager.GetComponent<ItemManager>();
        rfc = stageManager.GetComponent<RotateFoodsCnt>();
    }


    // 接触判定
    private void OnTriggerExit2D(Collider2D col)
    {

        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        // ルーペ使用
        if (col.GetComponent<Image>().sprite == loupeItemSpr)
        {
            // アイテム消費処理
            col.GetComponent<Image>().sprite = null; 
            im.UsedItem();

            // 吹き出しの画像をシュークリーム&ルーペに変更
            creamPuffImg.SetActive(false);
            creamPuffWithLoupeImg.SetActive(true);

            // ルーペを使用した時、Playerの目前にあるシュークリーム(からし入りシュークリーム)のインデックスを取得
            indexOfSpicyCf = rfc.indexOfFoods[0];
            usedLoupe = true; 

        }

    }
}
