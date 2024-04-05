using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChildController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject brokenWatermelon; // 木刀で割られたスイカ
    [SerializeField] GameObject watermelonSeeds;
    [SerializeField] GameObject rButton;
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

        // スイカアイテム使用
        if (col.GetComponent<Image>().sprite == watermelonSpr)
        {
            // アイテム消費処理
            col.GetComponent<Image>().sprite = null;
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
                brokenWatermelon.GetComponent<Animator>().Play("BrokenWatermelonActive");
            }

        }
    }

    // ------------ Animation ------------
    // スイカのタネをPlayerに飛ばすアニメーション開始時
    // スイカのタネを表示
    private void ActiveWatermelonSeeds()
    {
        if (!watermelonSeeds.GetComponent<Image>().enabled)
        {
            watermelonSeeds.GetComponent<Image>().enabled = true;
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
        rButton.GetComponent<Image>().enabled = false;
        player.GetComponent<Animator>().Play("PlayerOver4");
        wasPlayed_SpitOutAnima = true;
    }
    // ---------------------------------------
}
