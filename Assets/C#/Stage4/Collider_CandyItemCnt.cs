using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.Video;
using Cysharp.Threading.Tasks;

public class Collider_CandyItemCnt : MonoBehaviour
{
    [SerializeField] GameObject eraserBtn;
    [SerializeField] GameObject friend;
    [SerializeField] GameObject candyOnTheGround;
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

        // キャンディーアイテム使用
        if (col.GetComponent<Image>().sprite == candySpr)
        {
            // アイテム使用処理
            col.GetComponent<Image>().sprite = null;
            im.UsedItem();                           
            
            // ゲーム操作を禁止にする
            sm.CantGameControl();

            // friendにキャンディーを取らせる
            friend.GetComponent<Animator>().Play("FriendPeek");
            candyOnTheGround.GetComponent<SpriteRenderer>().enabled = true;

            // 黒板消しが落ちるアニメーション(クリア時)再生フラグON
            eraserBtn.GetComponent<Animator>().SetBool("isClear", true);

        }

    }
}
