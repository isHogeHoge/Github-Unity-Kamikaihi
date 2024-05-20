using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public class StageManager_9 : MonoBehaviour
{
    // ------- VideoPlayer -------
    [SerializeField] GameObject over1VP;
    [SerializeField] GameObject over2VP;
    // ---------------------------
    [SerializeField] GameObject endVideo;
    [SerializeField] Image img_rope2;
    [SerializeField] GameObject bird; 
    [SerializeField] Animator animator_player;
    [SerializeField] Image img_knot;  // ロープの結び目
    [SerializeField] Vector3 endPos;   // Birdの移動先座標

    private StageManager sm;
    private RectTransform rect_bird;            
    private const float moveSpeed = 500f;   // Birdの移動スピード
    private bool moving = false;            // Bird移動フラグ

    void Start()
    {
        sm = this.GetComponent<StageManager>();
        rect_bird = bird.GetComponent<RectTransform>();

        // ステージ初期位置から右に1ページ分だけ移動できるように設定
        this.GetComponent<StageScrollCnt>().maxCountR = 1;
    }

    private void Update()
    {
        // Bird移動
        if (moving)
        {
            rect_bird.anchoredPosition = Vector3.MoveTowards(rect_bird.anchoredPosition, endPos, moveSpeed * Time.deltaTime);
            // endPosまで移動したら、移動終了
            if (rect_bird.anchoredPosition.x == endPos.x)
            {
                moving = false;
            }
        }
        
    }
    
    // --------- Button ----------
    // Playerをクリックした時
    public async void ClickPlayerBtn()
    {
        // ゲーム操作を禁止
        sm.CantGameControl();

        // Playerとロープが繋がっているなら
        
        if (img_rope2.enabled)
        {
            GameObject videoPlayer = null;
            // ロープの切れ目を結んでいるかいないかで再生するビデオを選択
            // 結び目⚪︎
            if (img_knot.enabled)
            {
                videoPlayer = over2VP;
            }
            // 結び目×
            else
            {
                videoPlayer = over1VP;
            }

            // ゲームオーバービデオの再生 → ゲームオーバー処理
            await sm.PlayVideos(endVideo, videoPlayer, this.GetCancellationTokenOnDestroy());
            sm.GameOver(this.GetCancellationTokenOnDestroy()).Forget();
        }
        // Playerとロープが繋がっていないなら
        else
        {
            // ゲームオーバーアニメーション再生
            animator_player.Play("PlayerFall1");
        }
    }
    // 鳥をクリックした時、移動開始
    public void BirdMove()
    {
        bird.GetComponent<Animator>().Play("BirdFly");
        moving = true;
    }
    // -------------------------------

}
