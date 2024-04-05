using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Threading.Tasks;
using System;
using Cysharp.Threading.Tasks;
using System.Threading;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject openingVP;    // オープニング動画のVideoPlayer
    [SerializeField] GameObject tutorialVP;   // チュートリアル動画のVideoPlayer  
    [SerializeField] GameObject tutorialPanel;
    [SerializeField] GameObject tutorialVideo;
    [SerializeField] GameObject okButton;             
    [SerializeField] GameObject replayButton;         
    [SerializeField] GameObject closeButton;
    [SerializeField] GameObject stageManager;

    private const float fadeTime = 2.0f;　　　　// フェードアウトにかかる時間（秒)
    private TutorialDataManager tdm;

    private async void Start()
    {
        // オープニング動画の再生を待つ
        await stageManager.GetComponent<StageManager>().WaitForOpeningVideo(this.GetCancellationTokenOnDestroy());

        // チュートリアル動画未再生なら、再生する
        tdm = this.GetComponent<TutorialDataManager>();
        tdm.Load();
        if (!tdm.IsPlayedTutorialVideo())
        {
            // チュートリアルパネルのフェードアウト
            tutorialPanel.SetActive(true);
            await FadeOut(this.GetCancellationTokenOnDestroy());
            // チュートリアル動画再生
            await PlayTutorialVideo(this.GetCancellationTokenOnDestroy());
        }
    }

    // チュートリアル動画再生
    private async UniTask PlayTutorialVideo(CancellationToken ct)
    {
        // チュートリアルビデオの表示
        tutorialVP.SetActive(true);
        tutorialVideo.SetActive(true);

        VideoPlayer vp = tutorialVP.GetComponent<VideoPlayer>();
        vp.targetTexture.Release();
        vp.Play();
        await UniTask.Delay(TimeSpan.FromSeconds(vp.length), true, cancellationToken: ct);

        // 動画再生後にボタン表示
        okButton.SetActive(true);
        replayButton.SetActive(true);
    }

    // チュートリアル画面のフェードアウト処理
    private async UniTask FadeOut(CancellationToken ct)
    {
        Image image = tutorialPanel.GetComponent<Image>();
        // フェード後の色 & 最初の透明度を設定
        image.color = new Color((101.0f / 255.0f), (101.0f / 255.0f), (101.0f / 255.0f), (0.0f / 255.0f));

        
        int loopCount = 10;　　　　// ループ回数（フェードアウトまでに何回処理を行うか)
        float waitTime = fadeTime / loopCount; // ウェイト時間算出 (１ループでどれくらい時間を刻むのか)
        float alpha_interval = 255.0f / loopCount; // 色の間隔を算出(１ループで透明値をいくら上げるのか)

        // 色を徐々に変えるループ
        for (float alpha = 0.0f; alpha <= 141.0f; alpha += alpha_interval)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(waitTime),true,cancellationToken: ct); // 待ち時間

            // 透明値を少しずつ上げる
            Color color = image.color;
            color.a = alpha / 255.0f;
            image.color = color;
        }
    }

    // OkButton,CloseButtonをクリックした時
    public void ClickOKButton()
    {
        tutorialPanel.SetActive(false);
        tutorialVP.SetActive(false);
        tdm.ReWrite(); // チュートリアルを再生済みに

        Time.timeScale = 1.0f;
    }
    // ReplayButtonをクリックした時、もう一度チュートリアル動画再生
    public void ClickReplayButton()
    {
        tutorialVP.SetActive(false); // 一度非アクティブにする(連続再生に対応)
        closeButton.SetActive(true); // 2回目の再生からは、常に閉じるボタンを表示
        okButton.SetActive(false);
        replayButton.SetActive(false);
        PlayTutorialVideo(this.GetCancellationTokenOnDestroy()).Forget();
    }

}
