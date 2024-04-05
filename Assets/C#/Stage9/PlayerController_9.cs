using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController_9 : MonoBehaviour
{
    // -------- VideoPlayer -------
    [SerializeField] GameObject clearVP;    
    [SerializeField] GameObject over3VP;
    [SerializeField] GameObject over4VP;
    // ----------------------------
    [SerializeField] GameObject rope2;
    [SerializeField] GameObject knot; 　// ロープの結び目
    [SerializeField] GameObject GameEndVideo;
    [SerializeField] GameObject itemManager;
    [SerializeField] GameObject stageManager;
    [SerializeField] Sprite snakeItemSpr;
    [SerializeField] Sprite ropeItemSpr;

    private ItemManager im;  
    private StageManager sm; 

    void Start()
    {
        im = itemManager.GetComponent<ItemManager>();
        sm = stageManager.GetComponent<StageManager>();
    }

    // 接触判定(Item)
    private async void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        // 蛇アイテム使用
        if (col.GetComponent<Image>().sprite == snakeItemSpr)
        {
            // アイテム使用処理
            col.GetComponent<Image>().sprite = null;
            im.UsedItem();

            // ゲーム操作を禁止
            sm.CantGameControl();

            // Playerがロープと繋がっているなら
            // ロープの切れ目を結んでいるかいないかで再生するビデオを選択
            if (rope2.GetComponent<Image>().enabled)
            {
                GameObject videoPlayer = null;
                // 結び目⚪︎
                if (knot.GetComponent<Image>().enabled)
                {
                    videoPlayer = over4VP;
                }
                // 結び目×
                else
                {
                    videoPlayer = over3VP;
                }

                // ゲームオーバービデオ再生 → ゲームオーバー処理
                await sm.PlayVideos(GameEndVideo, videoPlayer, this.GetCancellationTokenOnDestroy());
                sm.GameOver(this.GetCancellationTokenOnDestroy()).Forget();
            }
            // Playerがロープと繋がっていないなら
            else
            {
                // ゲームオーバーアニメーション再生
                this.GetComponent<Animator>().Play("PlayerFall2");
            }
        }
        // ロープアイテム使用
        else if (col.GetComponent<Image>().sprite == ropeItemSpr)
        {
            // アイテム使用処理
            col.GetComponent<Image>().sprite = null;
            im.UsedItem();

            // ゲーム操作を禁止
            sm.CantGameControl();
            // ゲームクリアビデオ再生 → ゲームクリア処理
            await sm.PlayVideos(GameEndVideo,clearVP, this.GetCancellationTokenOnDestroy());
            sm.GameClear(9, this.GetCancellationTokenOnDestroy()).Forget();
        }


    }
}
