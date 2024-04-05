using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject pausePanel; // ポーズ画面
    [SerializeField] GameObject pauseText;  // ポーズ画面のテキスト

    private GameObject audioPlayerBGM;       
    private AudioSource bgm;                 
    private float stopTime;                  // ゲーム非操作時の経過時間
    internal float timeScale_UnPause = 0f;    // ポーズ解除後のTimeScale
    private bool isPausing = false;

    void Start()
    {
        // Inspectorから取得できない(DontDestoryオブジェクト)ため、Findメソッドで取得
        audioPlayerBGM = GameObject.Find("AudioPlayerBGM");
        bgm = audioPlayerBGM.GetComponent<AudioSource>();
    }

    private void Update()
    {
        // ポーズ中ならメソッドを抜ける
        if (isPausing)
        {
            return;
        }

        //+++ 1分間操作がない時、pause画面を表示 +++
        stopTime += Time.unscaledDeltaTime;
        if (Mathf.Floor(stopTime) == 60f)
        {
            PauseApp();
        }

        bool isClicked = Input.GetMouseButtonDown(0);
        if (isClicked) 
        {
            stopTime = 0.0f;
        }
        // +++++++++++++++++++++++++++++++++++++++
    }

    // 「閉じる」ボタンを押した時、ボーズ解除
    public void ClickResumeButton()
    {
        UnPauseApp();
        pauseText.SetActive(true);
    }

    // アプリのバックグラウンド処理
    private void OnApplicationPause(bool pause)
    {
        // アプリがバックグラウンドに行った時、初回のみポーズ処理
        if (pause && !isPausing) 
        {
            pauseText.SetActive(false);
            PauseApp();
        }
    }

    // ポーズ時の処理
    private void PauseApp()
    {
        // ポーズ解除後のTimeScaleを記録
        timeScale_UnPause = Time.timeScale;
        Time.timeScale = 0.0f;
        pausePanel.SetActive(true); 
        bgm.Pause();
        isPausing = true;
    }

    // ポーズ解除時の処理
    private void UnPauseApp()
    {
        stopTime = 0.0f;
        // 直前のTimeScaleを反映
        Time.timeScale = timeScale_UnPause;
        pausePanel.SetActive(false); 
        bgm.UnPause();
        isPausing = false;
    }
    
}
