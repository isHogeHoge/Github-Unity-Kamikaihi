using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player1Controller_19 : MonoBehaviour
{
    [SerializeField] GameObject clickCancelPnl;
    [SerializeField] GameObject itemManager;
    [SerializeField] GameObject stageManager;
    // アイテム画像
    [SerializeField] Sprite smartPhoneSpr;
    [SerializeField] Sprite apronSpr;

    private ItemManager im;
    private Animator animator_player1;
    internal bool called = false;         // スマートフォン使用フラグ
    internal bool isWearingApron = false;    // エプロン着用フラグ
    private void Start()
    {
        im = itemManager.GetComponent<ItemManager>();
        animator_player1 = this.GetComponent<Animator>();
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
        // スマートフォンアイテム使用
        if (img_item.sprite == smartPhoneSpr)
        {
            // アイテム使用処理
            img_item.sprite = null;
            im.UsedItem(); 

            clickCancelPnl.SetActive(true);

            // Brotherにスマートフォンを渡すアニメーション再生
            animator_player1.Play("PlayerPass");
            called = true;

        }
        // エプロンアイテム使用
        else if (img_item.sprite == apronSpr)
        {
            // アイテム使用処理
            img_item.sprite = null;
            im.UsedItem();

            // Playerにエプロン着用させる
            animator_player1.Play("PlayerInAApronHope");
            isWearingApron = true;

        }
    }
}
