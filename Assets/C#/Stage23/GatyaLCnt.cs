using UnityEngine;
using UnityEngine.UI;
public class GatyaLCnt : MonoBehaviour
{
    [SerializeField] GameObject posterLBtn; // 「故障中」ポスター取得ボタン
    [SerializeField] Animator animator_player;
    [SerializeField] SpriteRenderer sr_posterL; // 「故障中」ポスター
    [SerializeField] GameObject itemManager;
    // アイテム画像
    [SerializeField] Sprite coinItemSpr;

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
            // Playerがガチャを回すアニメーション再生
            animator_player.Play("PlayerTurnTheGatyaL");
        }
        // 「故障中」ポスターアイテム使用
        else
        {
            // ガチャの前に「故障中」ポスターを表示 & ガチャを回せないようにする
            posterLBtn.SetActive(true);
            sr_posterL.GetComponent<SpriteRenderer>().enabled = true;
            this.GetComponent<BoxCollider2D>().enabled = false;
        }

    }
}
