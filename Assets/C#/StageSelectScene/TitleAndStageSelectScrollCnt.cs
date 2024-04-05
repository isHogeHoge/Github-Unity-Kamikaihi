using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.ComponentModel;
using UnityEngine.SceneManagement;

public class TitleAndStageSelectScrollCnt : MonoBehaviour
{
    [SerializeField] GameObject rButton;              // 左スクロールボタン
    [SerializeField] GameObject lButton;              // 右スクロールボタン
    [SerializeField] GameObject topBorder;            
    [SerializeField] GameObject bottomBorder;         
    [SerializeField] GameObject stagesPanel;          
    [SerializeField] GameObject audioPlayerSE;        
    [SerializeField] AudioClip audioClipSE1;          // ボタンSE(StartButton以外)
    [SerializeField] AudioClip audioClipSE2;          // StartButtonのSE
    [SerializeField] TextMeshProUGUI pagesUIText;     // 「nページ」テキスト
    
    private AudioSource se;                           
    private Button btR;                               // 右側スクロールボタンのButtonコンポーネント
    private Button btL;                               // 左側スクロールボタンのButtonコンポーネント
    private TextMeshProUGUI pageText_tmpUGUI;         // pagesUITextのTextMeshProUGUIコンポーネント
    private int defPage = 1;                          // 現在のページ
    private static Vector3 page1Pos;                   // Page1でのStagesPanelのポジション
    private Vector3 targetPos;                        // stagesPanelの移動先ポジション
    private Vector3 leftBottom;                       // 画面左下座標
    private Vector3 rightTop;　　　　　　　　　　　　　　 // 画面右下座標
    private float cameraWidth;                        // カメラの横幅
    private const float scrollXSpeed = 14.0f;         // stagesPanelのスクロールスピード(X軸)
    private static bool isLoaded = false;             // シーンがすでにロードされているかを判定するフラグ
    private bool isScrolling = false;
    

    private void Start()
    {
        // シーンがすでに一度ロードされていたら
        if (isLoaded) 
        {
            // Page1から表示 & UIの表示
            stagesPanel.transform.position = page1Pos;
            topBorder.SetActive(true);
            bottomBorder.SetActive(true);
        }
        // ロードされていなかったら、ロード済みに設定する
        isLoaded = true;

        se = audioPlayerSE.GetComponent<AudioSource>();

        btL = lButton.GetComponent<Button>();
        btR = rButton.GetComponent<Button>();

        pageText_tmpUGUI = pagesUIText.GetComponent<TextMeshProUGUI>();

        leftBottom = Camera.main.ScreenToWorldPoint(Vector3.zero);
        rightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        cameraWidth = rightTop.x - leftBottom.x;
        
    }

    private void Update()
    {
        // ポーズ中ならUpdateを抜ける
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        // 現在のページ数を更新
        pageText_tmpUGUI.text = $"ページ{defPage}";

        // +++ stagePanelを左(右)にスクロールさせる +++
        if (isScrolling)
        {
            stagesPanel.transform.position = Vector3.MoveTowards(stagesPanel.transform.position, targetPos, scrollXSpeed * Time.deltaTime);
            if (stagesPanel.transform.position == targetPos)
            {
                // スクロールボタンを再度クリック可能に
                isScrolling = false;
            }
        }
        // ++++++++++++++++++++++++++++++++++++++++++

        // +++ 現在のページに応じて、R(L)Buttonを表示・非表示にする
        switch (defPage)
        {
            case 1: // プレイヤーがページ1にいる時
                page1Pos = stagesPanel.transform.position;
                btL.interactable = false;    // 左ボタンを無効化し、ページ1より前のページに移行できないようにする
                break;

            case 6: // プレイヤーがページ6にいる時
                btR.interactable = false;   // 右ボタンを無効化し、ページ6より先のページに移行できないようにする
                break;

            default:
                btL.interactable = true;
                btR.interactable = true;
            break;

        }
        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++        
    }


    // 「スタート」ボタンを押した時、ステージ選択画面(右側ページ)に移動
    public void ClickStartButton()
    {
        se.PlayOneShot(audioClipSE2);
        // (stagesPanelの)移動先を、現在位置 - カメラの横幅分に設定
        targetPos = stagesPanel.transform.position + new Vector3(-1, 0, 0) * cameraWidth;
        isScrolling = true;
        topBorder.SetActive(true);
        bottomBorder.SetActive(true);
    }
    /// <summary>
    /// 画面スクロール
    /// </summary>
    /// <param name="dir">移動先のページが右側なら"RIGHT"左側なら"LEFT"を代入</param>
    public void ClickRLButton(string dir)
    {
        // スクロール中はメソッドを抜ける
        if (isScrolling)
        {
            return;
        }

        // 右・左側のページにスクロール
        switch (dir)
        {
            // 右側
            case "RIGHT":
                // (stagesPanelの)移動先を、現在位置 - カメラの横幅分に設定
                targetPos = stagesPanel.transform.position + new Vector3(-1, 0, 0) * cameraWidth;
                defPage++;
                break;
            // 左側
            case "LEFT":
                // (stagesPanelの)移動先を、現在位置 + カメラの横幅分に設定
                targetPos = stagesPanel.transform.position + new Vector3(1, 0, 0) * cameraWidth;
                defPage--;
                break;

        }
        se.PlayOneShot(audioClipSE1);
        isScrolling = true;
    }

    // タイトル画面に遷移
    public static void LoadTitle()
    {
        isLoaded = false;
        SceneManager.LoadScene(0);
    }

}
