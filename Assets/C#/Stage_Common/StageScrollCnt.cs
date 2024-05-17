using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageScrollCnt : MonoBehaviour
{
    [SerializeField] GameObject clickCancelPnl;      
    [SerializeField] GameObject rButton;             // RButton(右側ページにスクロール)
    [SerializeField] GameObject lButton;             // LButton(左側ページにスクロール)
    [SerializeField] GameObject stagePanel;

    private Image rBtn_Image;
    private Image lBtn_Image;
    private Vector3 targetPos;                        // stagePanelの移動先ポジション
    private Vector3 leftBottom;                       // 画面左下座標
    private Vector3 rightTop;　　　　　　　　　　　　　　 // 画面右上座標
    private float gameScreen_width;                  // ゲーム画面の横幅
    private const float scrollSpeed = 15f;          // stagePanelのスクロールスピード
    private bool isScrolling = false;                  // stagePanelスクロール中フラグ
    private bool isActive_CancelPnl = false;           // clickCancelPnlのアクティブor非アクティブフラグ
    internal int maxCountL = 0;                      // ステージ初期位置からLButtonをクリックできる回数(-)
    internal int maxCountR = 0;                      // ステージ初期位置からRButtonをクリックできる回数(+)

    // ステージ初期パネルから右(左)に何ページ移動しているか
    // RButtonクリック(右側ページ移動)で+1、LButtonクリック(左側ページ移動)で-1
    private int _clickCount;                                          
    // _clickCountをmaxCountL ~ maxCountRの範囲に納めるためのプロパティ
    private int ClickCount {
        get
        {
            return _clickCount;
        }
        set
        {
            if(value <= maxCountL)
            {
                _clickCount = maxCountL;
            }
            else if(value >= maxCountR)
            {
                _clickCount = maxCountR;
            }
            else
            {
                _clickCount = value;
            }
        }
    }

    private void Start()
    {
        rBtn_Image = rButton.GetComponent<Image>();
        lBtn_Image = lButton.GetComponent<Image>();

        // ゲーム画面の横幅取得
        float cameraWidth = Camera.main.rect.width;
        float cameraHeight = Camera.main.rect.height;
        leftBottom = Camera.main.ScreenToWorldPoint(Vector3.zero);
        rightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * cameraWidth, Screen.height * cameraHeight, 0));
        gameScreen_width = rightTop.x - leftBottom.x;
    }

    private void Update()
    {
        // +++ stagePanelを右(左)にスクロールさせる +++
        if (isScrolling)
        {
            stagePanel.transform.position = Vector3.MoveTowards(stagePanel.transform.position, targetPos, scrollSpeed * Time.deltaTime);
            // 右(左)側のページまで移動したら、スクロール終了
            if (stagePanel.transform.position == targetPos)
            {
                // LButtonクリック回数の上限に達していたら、RButtonだけアクティブにする
                if (_clickCount == maxCountL)
                {
                    rBtn_Image.enabled = true;
                }
                // RButtonクリック回数の上限に達していたら、LButtonだけアクティブにする
                else if (_clickCount == maxCountR)
                {
                    lBtn_Image.enabled = true;
                }
                //　それ以外なら、両方のボタンをアクティブにする
                else
                {
                    rBtn_Image.enabled = true;
                    lBtn_Image.enabled = true;
                }

                // スクロール前clickCancelPnlが非アクティブなら、画面タップ可能に
                /*
                 * このスクリプトが複数のstagePanelにアタッチされている & スクロール前のclickCancelPnlが非アクティブ(false)の時
                 * ScrollStagePnlメソッドの実行にズレがあるため、isActive_CancelPnlが1つのスクリプト以外trueになってしまう
                 * そこでisActive_CancelPnlがfalesのスクリプトが1つでもあれば、スクロール前のclickCancelPnlの状態(false)を正しく反映できるようにしている
                 */
                if (!isActive_CancelPnl)
                {
                    clickCancelPnl.SetActive(false);
                }
                
                isScrolling = false;
            }
        }

        // ++++++++++++++++++++++++++++++++++++++++++
    }

    /// <summary>
    /// 画面スクロール
    /// </summary>
    /// <param name="dir">移動先のページが右側なら"RIGHT"左側なら"LEFT"を代入</param>
    public void ScrollStagePnl(string dir)
    {
        if (dir == "RIGHT")
        {
            ClickCount++;
            // (stagePanelの)移動先を、現在位置 - カメラの横幅分に設定(-X方向に移動)
            targetPos = stagePanel.transform.position + new Vector3(-1, 0, 0) * gameScreen_width;
        }
        else if (dir == "LEFT")
        {
            ClickCount--;
            // (stagePanelの)移動先を、現在位置 + カメラの横幅分に設定(X方向に移動)
            targetPos = stagePanel.transform.position + new Vector3(1, 0, 0) * gameScreen_width;
        }

        // スクロール前のclickCancelPnlの状態を代入
        isActive_CancelPnl = clickCancelPnl.activeSelf;
        // 画面タップ & 連続スクロール禁止に
        clickCancelPnl.SetActive(true);
        rBtn_Image.enabled = false;
        lBtn_Image.enabled = false;

        isScrolling = true;
    }

    // スクロール中判定メソッド
    internal bool isStageScrolling()
    {
        return isScrolling;
    }
}
