using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayersMovementCnt_24 : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject friend1;
    [SerializeField] GameObject hidingFriend1; // 墓石に隠れているFriend1
    [SerializeField] GameObject well;
    [SerializeField] GameObject friend2;
    [SerializeField] GameObject brother;
    [SerializeField] GameObject ghost;

    private StageManager sm;
    private StageManager_24 sm_24;
    private PlayerController_24 pc_24;
    private Animator animator_player;
    private Animator animator_friend1;
    private Animator animator_friend2;
    private Animator animator_brother;
    private Animator animator_ghost;
    private SpriteRenderer sr_friend1;
    private SpriteRenderer sr_hidingFriend1;
    private float playerPosX1 = -0.97f;  // playerのX座標(Friend2に驚かされる時)
    private float playerPosX2 = 3.4f;   // playerのX座標(Friend1に驚かされる時)
    private float playerPosX3 = 4.4f;    // playerのX座標(Brotherに驚かされる時)
    private float playerPosX_Goal = 7.7f;  // PlayerのゴールX座標
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
        animator_brother = brother.GetComponent<Animator>();
        animator_ghost = ghost.GetComponent<Animator>();
        sr_friend1 = friend1.GetComponent<SpriteRenderer>();
        sr_hidingFriend1 = hidingFriend1.GetComponent<SpriteRenderer>();
        
        // 画面右上の座標を取得
        rightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
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
                playerPosX2 += stagePnlPosX_ScrollR;
                playerPosX3 += stagePnlPosX_ScrollR;
                playerPosX_Goal += stagePnlPosX_ScrollR;

                // 次の移動先をFriend1に驚かされるポジションに更新
                targetPos = new Vector3(playerPosX2, player.transform.position.y, player.transform.position.z);

                canScroll = false;
                return;
            }
            
        }

        //  +++++ 移動終了時処理 +++++
        if (player.transform.position.x != targetPos.x)
        {
            return;
        }

        // Playerの移動ストップ
        isMoving = false;
        animator_player.SetBool("StopFlag", true);

        // Friend2に驚かされるポジションまで移動後
        if (player.transform.position.x == playerPosX1)
        {
            // Friend2出現
            animator_friend2.enabled = true;
        }
        // Friend1に驚かされるポジションまで移動後
        else if (player.transform.position.x == playerPosX2)
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
                targetPos = new Vector3(playerPosX3, player.transform.position.y, player.transform.position.z);
            }
        }
        // Brotherに驚かされるポジションまで移動後
        else if(player.transform.position.x == playerPosX3)
        {
            // Brother出現
            animator_brother.enabled = true;
            // 次の移動先をゴールに設定
            targetPos = new Vector3(playerPosX_Goal, player.transform.position.y, player.transform.position.z);
        }
        // ゴールまで移動後
        else if(player.transform.position.x == playerPosX_Goal)
        {
            // 幽霊出現&ゲームクリア処理
            animator_ghost.enabled = true;
            sm.GameClear(24, this.GetCancellationTokenOnDestroy()).Forget();
            isGoal = true;
        }
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
        // Friend2に驚かされるポジションまで移動
        if (well.GetComponent<WellController>().isScaredByFriend2)
        {
            targetPos = new Vector3(playerPosX1, player.transform.position.y, player.transform.position.z);
        }
        // Friend1に驚かされるポジションまで移動
        else
        {
            targetPos = new Vector3(rightTop.x, player.transform.position.y, player.transform.position.z);
        }
        PlayerMove();
    }

    

}
