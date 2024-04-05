using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using System.Threading;
using System;

public enum GameState
{
    playing,
    gameOver,
    gameClear
}

public class StageManager : MonoBehaviour
{
    [SerializeField] GameObject openingVP;     // オープニング動画のVideoPlayer
    [SerializeField] GameObject openingVideo;         
    [SerializeField] GameObject canvas;               
    [SerializeField] GameObject menuPanel;            
    [SerializeField] GameObject gameOverPanel;        
    [SerializeField] GameObject gameClearPanel;       
    [SerializeField] GameObject itemInventory;        
    [SerializeField] GameObject topBorder;            
    [SerializeField] GameObject clickCancelPnl;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject pauseManager;
    // 画面スクロール可能なステージのみ代入
    [SerializeField] GameObject rButton;  // ←スクロールボタン
    [SerializeField] GameObject lButton;  // →スクロールボタン

    private ClearDataManager cdm;
    private StageDataManager sdm;
    private TutorialDataManager tdm;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    private async void Start()
    {
        cdm = this.GetComponent<ClearDataManager>();
        sdm = this.GetComponent<StageDataManager>();
        tdm = this.GetComponent<TutorialDataManager>();
        tdm.Load();

        // オープニング動画の再生
        await PlayOpeningVideo(openingVideo, openingVP, this.GetCancellationTokenOnDestroy());

    }

    // オープニングビデオの再生 & 停止
    private async UniTask PlayOpeningVideo(GameObject video, GameObject videoPlayer, CancellationToken ct)
    {
        Time.timeScale = 0.0f;
        
        VideoPlayer vp = videoPlayer.GetComponent<VideoPlayer>();
        vp.targetTexture.Release();

        video.SetActive(true);
        vp.Play();
        await UniTask.Delay(TimeSpan.FromSeconds(vp.length), true, cancellationToken: ct);
        vp.Stop();
        video.SetActive(false);

        
        // チュートリアル再生済みならゲーム再開
        if (tdm.IsPlayedTutorialVideo())
        {
            // ポーズ中ならポーズ解除後にゲーム再開
            if (pausePanel.activeSelf)
            {
                pauseManager.GetComponent<PauseManager>().timeScale_UnPause = 1.0f;
            }
            else
            {
                Time.timeScale = 1.0f;
            }
        }
    }
    // オープニング動画の再生を待つ
    internal async UniTask WaitForOpeningVideo(CancellationToken ct)
    {
        VideoPlayer vp = openingVP.GetComponent<VideoPlayer>();
        await UniTask.Delay(TimeSpan.FromSeconds(vp.length), true, cancellationToken: ct);
    }

    /// <summary>
    /// (オープニングビデオ以外の)動画の再生&停止
    /// </summary>
    internal async UniTask PlayVideos(GameObject video, GameObject videoPlayer, CancellationToken ct)
    {
        Time.timeScale = 0.0f;
        
        VideoPlayer vp = videoPlayer.GetComponent<VideoPlayer>();
        vp.targetTexture.Release();

        video.SetActive(true);
        vp.Play();
        await UniTask.Delay(TimeSpan.FromSeconds(vp.length), true, cancellationToken: ct);
        vp.Stop();
        
    }

    // ------ Menu画面 ------
    // メニュー画面を表示
    public void ClickMenuButton()
    {
        Time.timeScale = 0.0f;
        menuPanel.SetActive(true);
    }

    // メニュー画面を非表示
    public void ClickMenuCloseBtn()
    {
        menuPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }
    // ----------------------

    /// <summary>
    /// ゲームオーバー画面の表示
    /// </summary>
    internal async UniTask GameOver(CancellationToken ct)
    {
        // UIを非表示にして、ゲームオーバーパネルを表示
        InActiveUI();
        await UniTask.Delay(2000,true,cancellationToken: ct);
        ActiveGameEndPnl(gameOverPanel,ct).Forget();

    }

    /// <summary>
    /// ゲームクリア画面の表示&クリアデータの保存
    /// </summary>
    /// <param name="stageId">クリアしたステージid</param>
    internal async UniTask GameClear(int stageId, CancellationToken ct)
    {
        
        // 新規クリアなら、クリアデータを保存
        cdm.Load();
        foreach (var i in cdm.loadDatas.dataLists)
        {
            if (stageId == i.stageId && !i.isClear)
            {
                // releasedCount(解放済みステージの総数)を+1する
                sdm.PlusReleasedCount();
                cdm.ReWrite(stageId);
            }

        }
        // ゲームクリアパネルを表示
        InActiveUI(); // UIを非アクティブに
        await UniTask.Delay(2000, true, cancellationToken: ct);
        ActiveGameEndPnl(gameClearPanel, ct).Forget();
    }

    // UI(ItemInventory & TopBorder)を非表示にする
    private void InActiveUI()
    {
        topBorder.SetActive(false);
        if (itemInventory)
        {
            itemInventory.SetActive(false);
        }
    }

    // ゲームクリア(ゲームオーバー)パネルを表示する
    private async UniTask ActiveGameEndPnl(GameObject panel,CancellationToken ct)
    {
        Time.timeScale = 0.0f;

        panel.SetActive(true);
        var panel_trans = panel.transform;
        // 子オブジェクトのText(回避失敗(成功))を順に表示していく
        for (var i = 0; i < panel_trans.childCount - 2; i++)
        {
            panel_trans.GetChild(i).gameObject.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), true, cancellationToken: ct);
        }
    }

    // ゲーム操作禁止処理
    public void CantGameControl()
    {
        clickCancelPnl.SetActive(true);
        itemInventory.SetActive(false);
        // ステージがスクロールできる形式なら、スクロールボタンも非アクティブに
        if (rButton && lButton)
        {
            rButton.SetActive(false);
            lButton.SetActive(false);
        }

    }


}
