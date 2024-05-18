using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeeksMovementCnt : MonoBehaviour
{
    // ----- 移動先ポジション -----
    // CDを止めに行く時1→2→3→4と移動する
    // 部屋に戻る時3→2→1→初期位置と移動する
    [SerializeField] GameObject targetPos1; // geek移動先ポジション1
    [SerializeField] GameObject targetPos2; // geek移動先ポジション2
    [SerializeField] GameObject targetPos3; // geek移動先ポジション3
    [SerializeField] GameObject targetPos4; // geek移動先ポジション4
    // ---------------------------
    [SerializeField] GameObject geekInTopRightRoom;
    [SerializeField] GameObject stagePanel;

    private StageScrollCnt scrollCnt;
    private Animator animator_Geek;
    private Animator animator_GeekInTopRightRoom;
    private SpriteRenderer sr_Geek;
    private SpriteRenderer sr_GeekInTopRightRoom;
    private List<Vector3> targetPos = new List<Vector3>(); // geek移動先ポジション(判定用)
    private List<Vector3> targetPos_R = new List<Vector3>(); // geek移動先ポジション(右側ページ)
    private List<Vector3> targetPos_L = new List<Vector3>(); // geek移動先ポジション(左側ページ)
    private Vector3 stagePnlPos_PageR; // (右側ページ)stagePanelの座標
    internal bool isGoing = false;     // CDを止めに移動中フラグ
    private bool isGoingBack = false; // 自身の部屋へ移動中フラグ
    private int index_targetPos = 1;     // 移動先ポジション指定用変数
    // index_targetPosをtargetPosリストの範囲内に納めるためのプロパティ
    private int Index_targetPos {
        get
        {
            return index_targetPos;
        }
        set
        {
            if (value <= 0)
            {
                index_targetPos = 0;
            }
            else if (value >= targetPos.Count - 1)
            {
                index_targetPos = targetPos.Count - 1;
            }
            else
            {
                index_targetPos = value;
            }
        }
    }
    void Start()
    {
        scrollCnt = stagePanel.GetComponent<StageScrollCnt>();
        animator_Geek = this.GetComponent<Animator>();
        animator_GeekInTopRightRoom = geekInTopRightRoom.GetComponent<Animator>();
        sr_Geek = this.GetComponent<SpriteRenderer>();
        sr_GeekInTopRightRoom = geekInTopRightRoom.GetComponent<SpriteRenderer>();

        stagePnlPos_PageR = new Vector3(-4.6f, 0f, 0f);
        // geekの移動先候補を代入していく
        targetPos_L = new List<Vector3>
        {
            this.transform.position,   // 初期位置
            targetPos1.transform.position,
            targetPos2.transform.position,
            targetPos3.transform.position,
            targetPos4.transform.position
        };
        for(var i = 0; i < targetPos_L.Count; i++)
        {
            // 右側ページ移動の際、stagePanel(親オブジェクト)がずれる分だけ調節
            targetPos_R.Add(targetPos_L[i] + stagePnlPos_PageR);
        }
    }

    void Update()
    {
        // ポーズ中ならUpdateを抜ける
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        // ------ CDを止めに行く処理 -------
        if (isGoing)
        {
            GeekMoveToTargetPos();
            if (this.transform.position == targetPos[index_targetPos] && !scrollCnt.isStageScrolling())
            {
                // CDPlayerがある部屋まで到着した時(targetPos4到着時)、音楽を止めるアニメーション再生
                if (index_targetPos == targetPos.Count - 1)
                {
                    sr_GeekInTopRightRoom.enabled = true;
                    animator_GeekInTopRightRoom.enabled = true;
                    animator_GeekInTopRightRoom.Play("GeekPauseTheMusic",0,0);

                    sr_Geek.enabled = false;
                    isGoing = false;
                    return;
                }

                // 移動先ポジションを更新
                Index_targetPos++;
            }
            
        }
        // --------------------------------

        // ------ 部屋へ戻る処理 --------
        if (isGoingBack)
        {
            GeekMoveToTargetPos();
            if (this.transform.position == targetPos[index_targetPos] && !scrollCnt.isStageScrolling())
            {
                // 自身の部屋の前まで移動したら、移動終了
                if (index_targetPos == 0)
                {
                    this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    animator_Geek.SetBool("GoInFlag", true);
                    isGoingBack = false;
                    return;
                }

                // 移動先ポジションを更新
                Index_targetPos--;

            }

        }
        // ------------------------------

    }
    // Geek移動処理
    private void GeekMoveToTargetPos()
    {
        float speed = 5f;
        this.transform.position = Vector3.MoveTowards(this.transform.position, targetPos[index_targetPos], speed * Time.deltaTime);
    }

    // 現在の位置から、自身の部屋まで引き返す
    internal void GeekGoBack()
    {
        if (isGoingBack)
        {
            return;
        }

        // 現在の向きを180度回転させる
        float geekRotY = this.transform.rotation.y;
        if(geekRotY == 0f)
        {
            geekRotY = 180f;
        }
        else
        {
            geekRotY = 0f;
        }
        this.transform.rotation = Quaternion.Euler(0f, geekRotY, 0f);

        // 部屋へ引き返す移動に変更
        Index_targetPos--;
        isGoing = false;
        isGoingBack = true;
    }

    // 階段の折り返し地点で(targetPos2)、向きを変える
    // 行き
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (isGoing)
        {
            if (col.transform.CompareTag("Turn"))
            {
                this.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
        }
    }
    // 帰り
    private void OnTriggerExit2D(Collider2D col)
    {
        if (isGoingBack)
        {
            if (col.transform.CompareTag("Turn"))
            {
                this.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
        }
    }

    // --- スクロールボタンに登録する ---
    // ページ移動の際、stagePanel(親オブジェクト)がずれる分だけtargetPosを再設定する
    public void ResetTargetPos_ScrollR()
    {
        targetPos = targetPos_R;
    }
    public void ResetTargetPos_ScrollL()
    {
        targetPos = targetPos_L;
    }
    // ----------------------------------------
}
