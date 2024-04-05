using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerLController_30 : MonoBehaviour
{
    [SerializeField] GameObject clickCancelPnl;
    [SerializeField] GameObject switchOfLaserBtn; // レーザー(赤・緑)を切り替えるボタン
    [SerializeField] GameObject greenPanel;       // (暗視スコープ装着中)画面を緑色にするためのパネル
    [SerializeField] GameObject redLasers;
    [SerializeField] GameObject greenLaser;
    [SerializeField] GameObject playerR;
    [SerializeField] GameObject itemManager;
    [SerializeField] GameObject stageManager;
    // アイテム画像
    [SerializeField] Sprite nightScopeSpr;　　 // 暗視スコープ

    private StageManager_30 sm_30;
    //private float targetPosX = 4.3f;             // 移動先座標X
    private Vector3 rightTop;　　　　　           // 画面右上座標
    internal bool isMoving = false;               // PlayerL移動フラグ
    private bool canScroll = true;                // (PlayerL移動時)画面スクロール可or不可フラグ
    private bool canUseANightScope = true;        // 暗視スコープ使用可or不可フラグ

    void Start()
    {
        sm_30 = stageManager.GetComponent<StageManager_30>();
        // 画面右上の座標を取得
        rightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    }

    void Update()
    {
        // ポーズ中ならUpdateを抜ける
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        // レーザー(緑)まで移動
        if (isMoving)
        {
            float speed = 0.7f;
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(greenLaser.transform.position.x, this.transform.position.y, 0f), speed * Time.deltaTime);

            // 画面右端まで移動したら、右側のページにスクロール
            if (this.transform.position.x >= rightTop.x)
            {
                if (canScroll)
                {
                    sm_30.ScrollStagePnl("RIGHT");
                    canScroll = false;
                }
            }
        }
    }

    // 接触判定(Item)
    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        // 暗視スコープアイテム使用
        if (col.GetComponent<Image>().sprite == nightScopeSpr && canUseANightScope)
        {
            // アイテム使用処理
            col.GetComponent<Image>().sprite = null;
            itemManager.GetComponent<ItemManager>().UsedItem();

            // 画面を暗転させる
            greenPanel.GetComponent<Image>().enabled = true;
            // レーザー(赤)はアクティブなら表示、レーザー(緑)は非表示に
            if(stageManager.GetComponent<StageManager_30>().currentLaser == ActiveLaser.red)
            {
                for (var i = 0; i < redLasers.transform.childCount; i++)
                {
                    redLasers.transform.GetChild(i).GetComponent<Image>().enabled = true;
                }
            }
            greenLaser.GetComponent<SpriteRenderer>().enabled = false;
            // レーザー(緑)は視認できないので接触判定をONに
            greenLaser.GetComponent<BoxCollider2D>().enabled = true;   

            // レーザーを切り替えるボタンを使用可能に
            switchOfLaserBtn.GetComponent<Image>().enabled = true;

            // アニメーションを暗視スコープ着用中に変更
            this.GetComponent<Animator>().SetBool("isWearing", true);
        }

    }

    // 接触判定
    private void OnTriggerEnter2D(Collider2D col)
    {
        // PlayerRに切り替わる地点まで到達後
        if (col.tag == "Change")
        {
            // 暗視スコープ使用&取得不可能に
            canUseANightScope = false;
            this.GetComponent<PlayerLBtnCnt>().canClickPlayerL= false;

            // 暗視スコープを装備していないならPlayerRに変更→レーザー(緑)を乗り越えるアニメーション再生
            if (!this.GetComponent<Animator>().GetBool("isWearing"))
            {
                playerR.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }
        // レーザーと接触時、ゲームオーバー
        else if (col.tag == "Laser")
        {
            stageManager.GetComponent<StageManager_30>().GameOver();
        }
    }

}
