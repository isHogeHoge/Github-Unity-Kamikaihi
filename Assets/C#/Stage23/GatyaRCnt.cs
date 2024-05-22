using UnityEngine;
using UnityEngine.UI;
public class GatyaRCnt : MonoBehaviour
{
    [SerializeField] GameObject posterRBtn; // 「故障中」ポスターを取るボタン
    [SerializeField] GameObject player;
    [SerializeField] SpriteRenderer sr_strap;
    [SerializeField] SpriteRenderer sr_figure;
    [SerializeField] BoxCollider2D boxCol_gatyaL;
    [SerializeField] SpriteRenderer sr_gatyaCapsule;
    [SerializeField] SpriteRenderer sr_posterL;
    [SerializeField] SpriteRenderer sr_posterR;
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

    private PlayerController_23 pc_23;
    private int count_TurnTheGatyaR = 0; // GatyaRを回した回数
    private void Start()
    {
        pc_23 = player.GetComponent<PlayerController_23>();
    }

    // 接触判定
    private void OnTriggerEnter2D(Collider2D col)
    {
        // GatyaLへのアイテム使用を禁止(ダブルブッキング防止)
        if (!sr_posterL.enabled) boxCol_gatyaL.enabled = false;
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            if (!sr_posterL.enabled) boxCol_gatyaL.enabled = true;
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
            if (pc_23.canGetAGoldMan)
            {
                sr_gatyaCapsule.sprite = gatyaCapsule_Red;
                sr_figure.sprite = figure_GoldMan;
                sr_strap.sprite = null;
            }
            else
            {
                count_TurnTheGatyaR++;
                // ガチャを回した回数に応じて、ガチャカプセルの色&取得するストラップ・フィギュアを変更
                switch (count_TurnTheGatyaR)
                {
                    // 赤ガチャカプセル & スライムストラップ
                    case 1:
                        sr_gatyaCapsule.sprite = gatyaCapsule_Red;
                        sr_strap.sprite = strap_Slime;
                        sr_figure.sprite = null;
                        break;
                    // ピンクガチャカプセル & 蜘蛛ストラップ
                    case 2:
                        sr_gatyaCapsule.sprite = gatyaCapsule_Pink;
                        sr_strap.sprite = strap_Spider;
                        sr_figure.sprite = null;
                        break;
                    // 青ガチャカプセル & 魚フィギュア
                    case 3:
                        sr_gatyaCapsule.sprite = gatyaCapsule_Blue;
                        sr_figure.sprite = figure_Fish;
                        sr_strap.sprite = null;
                        break;
                    // 緑ガチャカプセル & 日本人形フィギュア
                    case 4:
                        sr_gatyaCapsule.sprite = gatyaCapsule_Green;
                        sr_figure.sprite = figure_Doll;
                        sr_strap.sprite = null;
                        break;
                    // 緑ガチャカプセル & カニストラップ
                    case 5:
                        sr_gatyaCapsule.sprite = gatyaCapsule_Green;
                        sr_strap.sprite = strap_Crab;
                        sr_figure.sprite = null;
                        break;
                    // 青ガチャカプセル & うんちストラップ
                    case 6:
                        sr_gatyaCapsule.sprite = gatyaCapsule_Blue;
                        sr_strap.sprite = strap_Number2;
                        sr_figure.sprite = null;
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
            sr_posterR.enabled = true;
            this.GetComponent<BoxCollider2D>().enabled = false;
        }

        if (!sr_posterL.enabled) boxCol_gatyaL.enabled = true;

    }
}
