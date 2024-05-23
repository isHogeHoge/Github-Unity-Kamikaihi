using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerLController_30 : MonoBehaviour
{
    [SerializeField] GameObject clickCancelPnl;
    [SerializeField] Image img_switchOfLaserBtn; // レーザー(赤・緑)を切り替えるボタン
    [SerializeField] Image img_greenPanel;       // 暗視スコープ装着中パネル
    [SerializeField] GameObject redLasers;
    [SerializeField] SpriteRenderer sr_greenLaser;
    [SerializeField] BoxCollider2D boxCol_greenLaser;
    [SerializeField] GameObject playerR;
    [SerializeField] GameObject itemManager;
    [SerializeField] GameObject stageManager;
    // アイテム画像
    [SerializeField] Sprite nightScopeSpr;　　 // 暗視スコープ

    private ItemManager im;
    private StageManager_30 sm_30;
    private Animator animator_playerL;
    private Vector3 rightTop;　　　　　           // 画面右上座標
    internal bool isMoving = false;               // PlayerL移動フラグ
    private bool canScroll = true;                // (PlayerL移動時)画面スクロール可or不可フラグ
    private bool canUseANightScope = true;        // 暗視スコープ使用可or不可フラグ

    void Start()
    {
        im = itemManager.GetComponent<ItemManager>();
        sm_30 = stageManager.GetComponent<StageManager_30>();
        animator_playerL = this.GetComponent<Animator>();

        // 画面右上の座標を取得(ワールド座標)
        float cameraWidth = Camera.main.rect.width;
        float cameraHeight = Camera.main.rect.height;
        Vector3 cameraPos = Camera.main.WorldToScreenPoint(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0f));
        rightTop = Camera.main.ScreenToWorldPoint(new Vector3(cameraPos.x + (Screen.width * cameraWidth * 0.5f), cameraPos.y + (Screen.height * cameraHeight * 0.5f), 0f));
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
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(sr_greenLaser.transform.position.x, this.transform.position.y, 0f), speed * Time.deltaTime);

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

        Image img_item = col.GetComponent<Image>();
        // 暗視スコープアイテム使用
        if (img_item.sprite == nightScopeSpr && canUseANightScope)
        {
            // アイテム使用処理
            img_item.sprite = null;
            im.UsedItem();

            // 画面を暗転させる
            img_greenPanel.enabled = true;
            // レーザー(赤)はアクティブなら表示、レーザー(緑)は非表示に
            if(sm_30.currentLaser == ActiveLaser.red)
            {
                for (var i = 0; i < redLasers.transform.childCount; i++)
                {
                    redLasers.transform.GetChild(i).GetComponent<Image>().enabled = true;
                }
            }
            sr_greenLaser.enabled = false;
            // レーザー(緑)は視認できないので接触判定をONに
            boxCol_greenLaser.enabled = true;   

            // レーザーを切り替えるボタンを使用可能に
            img_switchOfLaserBtn.enabled = true;

            // アニメーションを暗視スコープ着用中に変更
            animator_playerL.SetBool("isWearing", true);
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
            if (!animator_playerL.GetBool("isWearing"))
            {
                playerR.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }
        // レーザーと接触時、ゲームオーバー
        else if (col.tag == "Laser")
        {
            sm_30.GameOver();
        }
    }

}
