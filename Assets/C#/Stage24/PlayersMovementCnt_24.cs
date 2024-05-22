using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayersMovementCnt_24 : MonoBehaviour
{
    [SerializeField] GameObject friend2;
    [SerializeField] GameObject player;
    [SerializeField] GameObject friend1;
    [SerializeField] SpriteRenderer sr_hidingFriend1; // 墓石に隠れているFriend1
    [SerializeField] Animator animator_ghost;
    [SerializeField] Animator animator_brother;

    private StageManager sm;
    private StageManager_24 sm_24;
    private PlayerController_24 pc_24;
    private Animator animator_player;
    private Animator animator_friend1;
    private Animator animator_friend2;
    private SpriteRenderer sr_friend1;
    private float targetPosX_friend2 = -0.97f;  // Friend2に驚かされるポジション
    private float targetPosX_friend1 = 3.4f;   // Friend1に驚かされるポジション
    private float targetPosX_brother = 4.4f;    // Brotherに驚かされるポジション
    private float targetPosX_goal = 7.7f;  // PlayerのゴールX座標
    private const float stagePnlPosX_ScrollR = -4.6f; // (右側へスクロール時)stagePanelのX座標
    private Vector3 targetPos;         // Playerの移動先ポジション
    private Vector3 rightTop;　　　　　 // 画面右上座標
    private bool isMoving = false;    // Player移動フラグ
    private bool canScroll = true;   // 画面スクロール可能フラグ
    private bool isGoal = false;   // Playerゴール到達フラグ

    void Start()
    {
        sm = this.GetComponent<StageManager>();
        sm_24 = this.GetComponent<StageManager_24>();
        pc_24 = player.GetComponent<PlayerController_24>();
        animator_player = player.GetComponent<Animator>();
        animator_friend1 = friend1.GetComponent<Animator>();
        animator_friend2 = friend2.GetComponent<Animator>();
        sr_friend1 = friend1.GetComponent<SpriteRenderer>();

        // 画面右上の座標を取得(ワールド座標)
        float cameraWidth = Camera.main.rect.width;
        float cameraHeight = Camera.main.rect.height;
        Vector3 cameraPos = Camera.main.WorldToScreenPoint(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0f));
        rightTop = Camera.main.ScreenToWorldPoint(new Vector3(cameraPos.x + (Screen.width * cameraWidth * 0.5f), cameraPos.y + (Screen.height * cameraHeight * 0.5f), 0f));
    }

    void Update()
    {
        // ポーズ中ならUpdateを抜ける
        if (Mathf.Approximately(Time.timeScale, 0f) || isGoal)
        {
            return;
        }

        //  ------- Player移動処理 -------
        if (isMoving)
        {
            float speed = 0.7f;
            player.transform.position = Vector3.MoveTowards(player.transform.position, targetPos, speed * Time.deltaTime);
        }

        // Playerが画面右端まで移動したら、初回のみ画面を右側にスクロールする
        if (player.transform.position.x == rightTop.x)
        {
            if (canScroll)
            {
                sm_24.ScrollStagePnl_Right();

                // stagePanelをスクロールした分だけ、移動先座標(X)も更新する
                targetPosX_friend1 += stagePnlPosX_ScrollR;
                targetPosX_brother += stagePnlPosX_ScrollR;
                targetPosX_goal += stagePnlPosX_ScrollR;

                // 次の移動先をFriend1に驚かされるポジションに更新
                targetPos = new Vector3(targetPosX_friend1, player.transform.position.y, player.transform.position.z);

                canScroll = false;
                return;
            }
            
        }

        //  +++++ 移動終了時処理 +++++
        if (player.transform.position.x != targetPos.x)
        {
            return;
        }

        // Friend2に驚かされるポジションまで移動後
        if (player.transform.position.x == targetPosX_friend2)
        {
            // ゲームオーバー
            if (friend2.activeSelf)
            {
                animator_friend2.enabled = true;
            }
            // 移動中にFriend2へ蜘蛛アイテムを使用しているなら、移動続行
            else
            {
                targetPos = new Vector3(rightTop.x, player.transform.position.y, player.transform.position.z);
                return;
            }
        }
        // Friend1に驚かされるポジションまで移動後
        else if (player.transform.position.x == targetPosX_friend1)
        {
            // Friend1出現
            sr_hidingFriend1.enabled = false;
            animator_friend1.enabled = true;
            sr_friend1.enabled = true;

            // もしPlayerがカッパのマスクをつけていたら
            if (pc_24.wearingMask)
            {
                // Friend1がPlayerを見て気絶するアニメーション再生
                animator_friend1.SetBool("FaintFlag", true);
                // 次の移動先をBrotherに驚かされるポジションに設定
                targetPos = new Vector3(targetPosX_brother, player.transform.position.y, player.transform.position.z);
            }
        }
        // Brotherに驚かされるポジションまで移動後
        else if(player.transform.position.x == targetPosX_brother)
        {
            // Brother出現
            animator_brother.enabled = true;
            // 次の移動先をゴールに設定
            targetPos = new Vector3(targetPosX_goal, player.transform.position.y, player.transform.position.z);
        }
        // ゴールまで移動後
        else if(player.transform.position.x == targetPosX_goal)
        {
            // 幽霊出現&ゲームクリア処理
            animator_ghost.enabled = true;
            sm.GameClear(24, this.GetCancellationTokenOnDestroy()).Forget();
            isGoal = true;
        }

        // Playerの移動ストップ
        isMoving = false;
        animator_player.SetBool("StopFlag", true);
        // +++++++++++++++++++++++++
        // ----------------------------------
    }

    // Player移動開始メソッド
    internal void PlayerMove()
    {
        // 移動開始
        animator_player.SetBool("StopFlag", false);
        animator_player.SetBool("WalkFlag", true);
        isMoving = true;
    }

    // 「進む」ボタンをクリックした時
    public void ClickGoBtn()
    {
        // 移動先ポジションの設定
        if (friend2.activeSelf)
        {
            targetPos = new Vector3(targetPosX_friend2, player.transform.position.y, player.transform.position.z);
        }
        else
        {
            targetPos = new Vector3(rightTop.x, player.transform.position.y, player.transform.position.z);
        }
        PlayerMove();
    }

    

}
