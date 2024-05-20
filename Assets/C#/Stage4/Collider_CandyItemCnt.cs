using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collider_CandyItemCnt : MonoBehaviour
{
    [SerializeField] Animator animator_eraserBtn;
    [SerializeField] Animator animator_friend;
    [SerializeField] SpriteRenderer sr_candyOnTheGround;
    [SerializeField] GameObject stageManager;
    [SerializeField] GameObject itemManager;
    [SerializeField] Sprite candySpr;

    private StageManager sm;
    private ItemManager im;    

    void Start()
    {
        sm = stageManager.GetComponent<StageManager>();
        im = itemManager.GetComponent<ItemManager>();
    }

    // 接触判定
    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        Image img_item = col.GetComponent<Image>();
        // キャンディーアイテム使用
        if (img_item.sprite == candySpr)
        {
            // アイテム使用処理
            img_item.sprite = null;
            im.UsedItem();                           
            
            // ゲーム操作を禁止にする
            sm.CantGameControl();

            // friendにキャンディーを取らせる
            animator_friend.Play("FriendPeek");
            sr_candyOnTheGround.enabled = true;

            // 黒板消しが落ちるアニメーション(クリア時)再生フラグON
            animator_eraserBtn.SetBool("isClear", true);

        }

    }
}
