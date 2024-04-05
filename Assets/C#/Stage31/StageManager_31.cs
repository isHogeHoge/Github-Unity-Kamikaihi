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
    [SerializeField] GameObject stagePanel; 
    [SerializeField] GameObject tutorialText_U;
    [SerializeField] GameObject tutorialText_D;
    [SerializeField] GameObject fartherClearImg;
    [SerializeField] GameObject middleClearImg;
    [SerializeField] GameObject closerClearImg;
    [SerializeField] GameObject scoreText_Playing;
    [SerializeField] GameObject scoreText_Clear;
    [SerializeField] GameObject moveBtn;
    [SerializeField] GameObject titleBtn;
    [SerializeField] GameObject topBorder;
    [SerializeField] GameObject playersLife;
    [SerializeField] GameObject player;       // Player_Playing
    [SerializeField] GameObject player_clear;

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
            playersLife.GetComponent<Image>().enabled = true;
        }
        // 画面タップ(キャンセル)時に呼ばれる処理 
        catch
        {
            // チュートリアル終了時の状態に
            // チュートリアルテキストのアニメーションを最後までスキップ
            tutorialText_U.GetComponent<Animator>().Play(tutorialText_U.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).fullPathHash, 0, 1);
            tutorialText_D.GetComponent<Animator>().Play(tutorialText_D.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).fullPathHash, 0, 1);
            // チュートリアルテキスト,Player,移動ボタンを全て表示
            tutorialText_D.SetActive(true);
            player.GetComponent<SpriteRenderer>().enabled = true;
            moveBtn.SetActive(true);
            playersLife.GetComponent<Image>().enabled = true;

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
        if (stagePanel.activeSelf)
        {
            RectTransform rect = stagePanel.GetComponent<RectTransform>();
            rect.anchoredPosition = Vector3.MoveTowards(rect.anchoredPosition, endPos, scrollSpeed * Time.deltaTime);
            // Playerが最後までオブジェクトに衝突しなかったら(stagePanelが最後までスクロールしたら)ステージクリア処理
            if (rect.anchoredPosition.y == endPos.y)
            {
                GameClear(this.GetCancellationTokenOnDestroy()).Forget();
            }
        }
    }

    // スコア加算処理
    internal void AddScore(int score)
    {
        if (!scoreText_Playing.GetComponent<TextMeshProUGUI>().enabled)
        {
            scoreText_Playing.GetComponent<TextMeshProUGUI>().enabled = true;
        }
        totalScore += score;
        scoreText_Playing.GetComponent<TextMeshProUGUI>().text = $"SCORE {totalScore}";
    }

    // ゲーム操作禁止処理
    internal void CantGameControl()
    {
        scrollSpeed = 0f;
        topBorder.SetActive(false);
        playersLife.GetComponent<Image>().enabled = false;
        scoreText_Playing.SetActive(false);
        moveBtn.SetActive(false);
    }

    // ゲームオーバー処理
    internal void GameOver()
    {
        CantGameControl();
        gameState = GameState.gameOver;
        player.GetComponent<Animator>().Play("PlayerOver");
        GetComponent<StageManager>().GameOver(this.GetCancellationTokenOnDestroy()).Forget();
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
            player_clear.GetComponent<Image>().enabled = true;

            // キャラクター画像 → 合計点数 → タイトル移行ボタンを順に表示
            closerClearImg.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: ct);
            middleClearImg.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: ct);
            fartherClearImg.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: ct);

            scoreText_Clear.GetComponent<TextMeshProUGUI>().text = $"SCORE {totalScore}";
            scoreText_Clear.GetComponent<TextMeshProUGUI>().enabled = true;
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: ct);

            titleBtn.GetComponent<Image>().enabled = true;

            stagePanel.SetActive(false);
        }
    }

    // 「タイトルへ」ボタンクリック時、タイトル遷移
    public void ClickTitleBtn()
    {
        TitleAndStageSelectScrollCnt.LoadTitle();
    }


}
