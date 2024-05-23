using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;
using UnityEngine.UI;
using TMPro;
public class StageManager_31 : MonoBehaviour
{
    [SerializeField] RectTransform rect_stagePanel;
    [SerializeField] Image img_playersLife;
    [SerializeField] GameObject tutorialText_U;
    [SerializeField] GameObject tutorialText_D;
    [SerializeField] GameObject fartherClearImg;
    [SerializeField] GameObject middleClearImg;
    [SerializeField] Image img_player_clear;
    [SerializeField] GameObject closerClearImg;
    [SerializeField] TextMeshProUGUI scoreText_Playing;
    [SerializeField] TextMeshProUGUI scoreText_Clear;
    [SerializeField] GameObject moveBtn;
    [SerializeField] Image img_titleBtn;
    [SerializeField] GameObject topBorder;
    [SerializeField] GameObject player;

    internal GameState gameState = GameState.playing;
    CancellationTokenSource cts;
    internal int totalScore = 0;
    private float scrollSpeed = 200f;
    private Vector3 endPos;     // stagePanelのゴール座標
    private bool canSkip = true;　// チュートリアルスキップ可or不可フラグ
    
    
    private async void Start()
    {
        endPos = new Vector3(0f, -16000f, 0f);
        // チュートリアル再生
        await PlayTutorial();
    }
    // チュートリアル再生処理
    private async UniTask PlayTutorial()
    {
        cts = new CancellationTokenSource();
        try
        {
            // ---- チュートリアルテキスト表示 ----
            await DisplayTutorialText(cts.Token, tutorialText_U);
            await DisplayTutorialText(cts.Token, tutorialText_D);
            // ---- Player&移動ボタン表示 ----
            player.GetComponent<SpriteRenderer>().enabled = true;
            await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: cts.Token);
            moveBtn.SetActive(true);
            img_playersLife.enabled = true;
        }
        // 画面タップ(スキップ)時に呼ばれる処理 
        catch
        {
            // チュートリアルテキスト,Player,移動ボタンを全て表示
            tutorialText_U.SetActive(true);
            tutorialText_D.SetActive(true);
            player.GetComponent<SpriteRenderer>().enabled = true;
            moveBtn.SetActive(true);
            img_playersLife.enabled = true;

            // チュートリアルテキストのアニメーションを最後までスキップ
            Animator animator_tutorialText_U = tutorialText_U.GetComponent<Animator>();
            Animator animator_tutorialText_D = tutorialText_D.GetComponent<Animator>();
            animator_tutorialText_U.Play(animator_tutorialText_U.GetCurrentAnimatorStateInfo(0).fullPathHash, 0, 1);
            animator_tutorialText_D.Play(animator_tutorialText_D.GetCurrentAnimatorStateInfo(0).fullPathHash, 0, 1);
            

        }
    }
    // チュートリアルテキスト表示処理
    private async UniTask DisplayTutorialText(CancellationToken ct,GameObject tutorialText)
    {
        tutorialText.SetActive(true);
        await UniTask.Delay(TimeSpan.FromSeconds(2.5f), cancellationToken: ct);
    }

    private void Update()
    {
        // ポーズ中ならUpdateを抜ける
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        // オープニング動画再生後、画面タップでチュートリアルをスキップ
        if (Input.GetMouseButtonDown(0) && canSkip)
        {
            cts.Cancel();
            canSkip = false;
        }

        // オブジェクトのスクロール
        if (rect_stagePanel.gameObject.activeSelf)
        {
            rect_stagePanel.anchoredPosition = Vector3.MoveTowards(rect_stagePanel.anchoredPosition, endPos, scrollSpeed * Time.deltaTime);
            // Playerが最後までオブジェクトに衝突しなかったら(stagePanelが最後までスクロールしたら)ステージクリア処理
            if (rect_stagePanel.anchoredPosition.y == endPos.y)
            {
                GameClear(this.GetCancellationTokenOnDestroy()).Forget();
            }
        }
    }

    // スコア加算処理
    internal void AddScore(int score)
    {
        if (!scoreText_Playing.enabled)
        {
            scoreText_Playing.enabled = true;
        }
        totalScore += score;
        scoreText_Playing.text = $"SCORE {totalScore}";
    }

    // ゲーム操作禁止処理
    internal void CantGameControl()
    {
        scrollSpeed = 0f;
        topBorder.SetActive(false);
        img_playersLife.enabled = false;
        scoreText_Playing.gameObject.SetActive(false);
        moveBtn.SetActive(false);
    }

    // ゲームオーバー処理
    internal void GameOver()
    {
        CantGameControl();
        gameState = GameState.gameOver;
        player.GetComponent<Animator>().Play("PlayerOver");
        this.GetComponent<StageManager>().GameOver(this.GetCancellationTokenOnDestroy()).Forget();
    }

    // ゲームクリア処理
    private async UniTask GameClear(CancellationToken ct)
    {
        // ステージをクリア済みに
        this.GetComponent<ClearDataManager>().ReWrite(31);

        // ゲーム操作を禁止に
        CantGameControl();
        gameState = GameState.gameClear;

        // Playerをクリア時の座標まで移動させる
        Vector3 targetPos = new Vector3(0, -0.5f, 0);
        float speed = 1.5f;
        player.transform.position = Vector3.MoveTowards(player.transform.position, targetPos,speed * Time.deltaTime);
        if(player.transform.position.y == targetPos.y)
        {
            // Playerを切り替える
            player.GetComponent<SpriteRenderer>().enabled = false;
            img_player_clear.enabled = true;

            // キャラクター画像 → 合計点数 → タイトル移行ボタンを順に表示
            closerClearImg.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: ct);
            middleClearImg.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: ct);
            fartherClearImg.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: ct);

            scoreText_Clear.text = $"SCORE {totalScore}";
            scoreText_Clear.enabled = true;
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: ct);

            img_titleBtn.enabled = true;

            rect_stagePanel.gameObject.SetActive(false);
        }
    }

    // 「タイトルへ」ボタンクリック時、タイトル遷移
    public void ClickTitleBtn()
    {
        TitleAndStageSelectScrollCnt.LoadTitle();
    }


}
