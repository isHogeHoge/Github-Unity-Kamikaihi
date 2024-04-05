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
    [SerializeField] GameObject rope2;
    [SerializeField] GameObject bird; 
    [SerializeField] GameObject player;
    [SerializeField] GameObject knot;  // ロープの結び目
    [SerializeField] Vector3 endPos;   // Birdの移動先座標

    private StageManager sm;
    private RectTransform rect;            // BirdのRectTransform
    private const float moveSpeed = 500f;   // Birdの移動スピード
    private bool moving = false;            // Bird移動フラグ

    void Start()
    {
        sm = this.GetComponent<StageManager>();
        rect = bird.GetComponent<RectTransform>();

        // ステージ初期位置から右に1ページ分だけ移動できるように設定
        this.GetComponent<StageScrollCnt>().maxCountR = 1;

    }

    private void Update()
    {
        // Bird移動
        if (moving)
        {
            rect.anchoredPosition = Vector3.MoveTowards(rect.anchoredPosition, endPos, moveSpeed * Time.deltaTime);
            // endPosまで移動したら、移動終了
            if (rect.anchoredPosition.x == endPos.x)
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
        // ロープの切れ目を結んでいるかいないかで再生するビデオを選択
        if (rope2.GetComponent<Image>().enabled)
        {
            GameObject videoPlayer = null;
            // 結び目⚪︎
            if (knot.activeSelf == false)
            {
                videoPlayer = over1VP;
            }
            // 結び目×
            else
            {
                videoPlayer = over2VP;
            }

            // ゲームオーバービデオの再生 → ゲームオーバー処理
            await sm.PlayVideos(endVideo, videoPlayer, this.GetCancellationTokenOnDestroy());
            sm.GameOver(this.GetCancellationTokenOnDestroy()).Forget();
        }
        // Playerとロープが繋がっていないなら
        else
        {
            // ゲームオーバーアニメーション再生
            player.GetComponent<Animator>().Play("PlayerFall1");
        }
    }
    // 鳥をクリックした時、移動開始
    public void BirdMove()
    {
        Animator animator = bird.GetComponent<Animator>();
        animator.Play("BirdFly");
        moving = true;
    }
    // -------------------------------

}
