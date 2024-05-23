using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChildController : MonoBehaviour
{
    [SerializeField] Animator animator_player;
    [SerializeField] Animator animator_brokenWatermelon;
    [SerializeField] Image img_watermelonSeeds;
    [SerializeField] Image img_RButton;
    [SerializeField] GameObject stageManager;
    [SerializeField] GameObject itemManager;
    // アイテム画像
    [SerializeField] Sprite watermelonSpr;

    private int count_brokenWatermelon = 0;  // 木刀で割ったスイカの数
    private int count_totalWatermelon = 6; // ステージにあるスイカの総数
    private bool wasPlayed_SpitOutAnima = false;  // スイカのタネを飛ばすアニメーション再生済みでture

    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        Image img_item = col.GetComponent<Image>();
        // スイカアイテム使用
        if (img_item.sprite == watermelonSpr)
        {
            // アイテム消費処理
            img_item.sprite = null;
            itemManager.GetComponent<ItemManager>().UsedItem();

            count_brokenWatermelon++;
            // すべてのスイカを割った時
            if(count_brokenWatermelon == count_totalWatermelon)
            {
                // スイカを食べるアニメーション再生(ゲームオーバー)
                stageManager.GetComponent<StageManager>().CantGameControl();
                this.GetComponent<Animator>().Play("ChildEat");
            }
            // ステージ上にスイカが残っているなら
            else
            {
                // スイカが木刀で割られるアニメーション再生
                animator_brokenWatermelon.Play("BrokenWatermelonActive");
            }

        }
    }

    // ------------ Animation ------------
    // スイカのタネをPlayerに飛ばすアニメーション開始時
    // スイカのタネを表示
    private void ActiveWatermelonSeeds()
    {
        if (!img_watermelonSeeds.enabled)
        {
            img_watermelonSeeds.enabled = true;
        }
    }
    // アニメーション終了後、ゲームオーバー
    // このアニメーションはLoopするので、一度だけゲームオーバー処理を行う
    private void PlayPlayerOverAnima()
    {
        if (wasPlayed_SpitOutAnima)
        {
            return;
        }

        stageManager.GetComponent<StageScrollCnt>().ScrollStagePnl("LEFT");
        img_RButton.enabled = false;
        animator_player.Play("PlayerOver4");
        wasPlayed_SpitOutAnima = true;
    }
    // ---------------------------------------
}
