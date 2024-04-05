using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GatyaRCnt : MonoBehaviour
{
    [SerializeField] GameObject posterRBtn; // 「故障中」ポスターを取るボタン
    [SerializeField] GameObject posterR; // 「故障中」ポスター
    [SerializeField] GameObject player;
    [SerializeField] GameObject strap;
    [SerializeField] GameObject figure;
    [SerializeField] GameObject gatyaCapsule; // ガチャカプセル
    [SerializeField] GameObject itemManager;
    [SerializeField] Sprite coinItemSpr; // 100円アイテム画像
    [SerializeField] Sprite strap_Crab;    // カニストラップの画像
    [SerializeField] Sprite strap_Number2; // うんこストラップの画像
    [SerializeField] Sprite strap_Slime;   // スライムストラップの画像
    [SerializeField] Sprite strap_Spider;  // 蜘蛛ストラップの画像
    [SerializeField] Sprite figure_Doll;   // 日本人形フィギュアの画像
    [SerializeField] Sprite figure_Fish;   // 魚フィギュアの画像
    [SerializeField] Sprite figure_GoldMan; // ゴールドマンの画像
    // ガチャカプセル(赤・青・緑・ピンク)の画像
    [SerializeField] Sprite gatyaCapsule_Red; 
    [SerializeField] Sprite gatyaCapsule_Blue;
    [SerializeField] Sprite gatyaCapsule_Green;
    [SerializeField] Sprite gatyaCapsule_Pink;

    private int count_TurnTheGatyaR = 0; // GatyaRを回した回数

    // 接触判定
    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        Sprite itemSpr = col.GetComponent<Image>().sprite;
        // アイテム使用処理
        col.GetComponent<Image>().sprite = null;
        itemManager.GetComponent<ItemManager>().UsedItem();

        // 100円アイテム使用
        if (itemSpr == coinItemSpr)
        {
            // ガチャを3回以上揺らしているなら、ゴールドマン(当たり)を取得
            if (player.GetComponent<PlayerController_23>().canGetAGoldMan)
            {
                gatyaCapsule.GetComponent<SpriteRenderer>().sprite = gatyaCapsule_Red;
                figure.GetComponent<SpriteRenderer>().sprite = figure_GoldMan;
                strap.GetComponent<SpriteRenderer>().sprite = null;
            }
            else
            {
                count_TurnTheGatyaR++;
                // ガチャを回した回数に応じて、ガチャカプセルの色&取得するストラップ・フィギュアを変更
                switch (count_TurnTheGatyaR)
                {
                    // 赤ガチャカプセル & スライムストラップ
                    case 1:
                        gatyaCapsule.GetComponent<SpriteRenderer>().sprite = gatyaCapsule_Red;
                        strap.GetComponent<SpriteRenderer>().sprite = strap_Slime;
                        figure.GetComponent<SpriteRenderer>().sprite = null;
                        break;
                    // ピンクガチャカプセル & 蜘蛛ストラップ
                    case 2:
                        gatyaCapsule.GetComponent<SpriteRenderer>().sprite = gatyaCapsule_Pink;
                        strap.GetComponent<SpriteRenderer>().sprite = strap_Spider;
                        figure.GetComponent<SpriteRenderer>().sprite = null;
                        break;
                    // 青ガチャカプセル & 魚フィギュア
                    case 3:
                        gatyaCapsule.GetComponent<SpriteRenderer>().sprite = gatyaCapsule_Blue;
                        figure.GetComponent<SpriteRenderer>().sprite = figure_Fish;
                        strap.GetComponent<SpriteRenderer>().sprite = null;
                        break;
                    // 緑ガチャカプセル & 日本人形フィギュア
                    case 4:
                        gatyaCapsule.GetComponent<SpriteRenderer>().sprite = gatyaCapsule_Green;
                        figure.GetComponent<SpriteRenderer>().sprite = figure_Doll;
                        strap.GetComponent<SpriteRenderer>().sprite = null;
                        break;
                    // 緑ガチャカプセル & カニストラップ
                    case 5:
                        gatyaCapsule.GetComponent<SpriteRenderer>().sprite = gatyaCapsule_Green;
                        strap.GetComponent<SpriteRenderer>().sprite = strap_Crab;
                        figure.GetComponent<SpriteRenderer>().sprite = null;
                        break;
                    // 青ガチャカプセル & うんちストラップ
                    case 6:
                        gatyaCapsule.GetComponent<SpriteRenderer>().sprite = gatyaCapsule_Blue;
                        strap.GetComponent<SpriteRenderer>().sprite = strap_Number2;
                        figure.GetComponent<SpriteRenderer>().sprite = null;
                        break;

                }
            }

            // Playerがガチャを回すアニメーション再生
            player.GetComponent<Animator>().Play("PlayerTurnTheGatyaR");
            
        }
        // 「故障中」ポスターアイテム使用
        else
        {
            // ガチャの前に「故障中」ポスターを表示 & ガチャを回せないようにする
            posterRBtn.SetActive(true);
            posterR.GetComponent<SpriteRenderer>().enabled = true;
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
