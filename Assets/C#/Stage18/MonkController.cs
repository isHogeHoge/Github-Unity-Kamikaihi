using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkController : MonoBehaviour
{
    [SerializeField] GameObject trio;
    [SerializeField] Animator animator_player;
    [SerializeField] Animator animator_friend1;
    [SerializeField] Animator animator_friend2;
    [SerializeField] SpriteRenderer sr_sb_down;
    [SerializeField] Animator animator_sb_down;
    [SerializeField] Animator animator_sb_up;
    [SerializeField] GameObject stageManager;
    
    private StageManager_18 sm_18;
    private Animator animator_monk;
    internal bool isMoving = true;
    internal Vector3 targetPos; // 移動先ポジション
    internal float moveSpeed = 0.5f;  // 自身の移動スピード

    void Start()
    {
        animator_monk = this.GetComponent<Animator>();
        sm_18 = stageManager.GetComponent<StageManager_18>();

        // 移動先の位置に、現在の位置を代入
        targetPos = this.transform.position;
    }

    void Update()
    {
        // ポーズ中ならUpdateを抜ける
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        // 出現中の吹き出し前まで移動
        if (isMoving)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, targetPos, moveSpeed * Time.deltaTime);

            // (targetPosまでの)変位がマイナスなら、左を向く
            if (this.transform.position.x > targetPos.x)
            {
                this.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                animator_monk.Play("MonkWalkL");
            }
            // (targetPosまでの)変位がプラスなら、右を向く
            else if (this.transform.position.x < targetPos.x)
            {
                this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                animator_monk.Play("MonkWalkR");
            }
        }

        // ゲームオーバーアニメーション再生処理
        if (this.transform.position == targetPos && sm_18.gameState == GameState.gameOver)
        {
            isMoving = false;
            // アニメーションの座標変更を反映する
            animator_monk.applyRootMotion = false;

            // 僧侶のX座標がplayerと等しいなら
            if (this.transform.position.x == animator_player.transform.position.x)
            {
                // 僧侶がplayer→friend2→friend1の順番に頭を叩くアニメーション再生
                animator_monk.Play("MonkHitPlayer~");
            }
            // friend1と等しいなら
            else if(this.transform.position.x == animator_friend1.transform.position.x)
            {
                // 僧侶がfriend1→player→friend2の順番に頭を叩くアニメーション再生
                animator_monk.Play("MonkHitFriend1~");
            }
            // friend2と等しいなら
            else
            {
                // 僧侶がfriend2→player→friend1の順番に頭を叩くアニメーション再生
                animator_monk.Play("MonkHitFriend2~");
            }

        }
    }

    // ----------- Animation ------------
    // "MonkStop"アニメーション再生中
    // 自身の吹き出しアニメーション再生
    private void PlaySpeechBubbleAnima()
    {
        // 初期アニメーションに移行しないようにする
        animator_sb_down.SetBool("isStart", false);
        animator_sb_up.SetBool("isStart", false);

        // 吹き出し(下)のアニメーション再生
        animator_sb_down.Play("Monk'sSB_Down",0,0);
        sr_sb_down.enabled = true;
        // 吹き出し(上下)アニメーションのスピードを変更
        animator_sb_down.SetFloat("Speed", 0.8f);
        animator_sb_up.SetFloat("Speed", 0.7f);
    }

    // "MonkOver"アニメーション開始時
    // Player,Friend1,Friend2のゲームクリアアニメーションを再生
    private void PlayTrioClearAnima()
    {
        for (var i = 0; i < trio.transform.childCount; i++)
        {
            Transform someone = trio.transform.GetChild(i);
            someone.GetComponent<Animator>().Play($"{someone.name}Clear");
        }
    }

    // "MonkHit"アニメーション再生中
    // アニメーションのスピードを変える
    private void ChangeSpeed(float changedSpeed)
    {
        animator_monk.SetFloat("Speed", changedSpeed);
    }
    // Playerを最初に叩く
    private void HitPlayer_First()
    {
        animator_player.Play("PlayerIsHit1");
        animator_friend1.Play("Friend1MakeFunOf");
        animator_friend2.Play("Friend2MakeFunOf");
    }
    // Playerを2番目以降に叩く
    private void HitPlayer_Next()
    {
        animator_player.Play("PlayerIsHit2");
    }
    // Friend1を最初に叩く
    private void HitFriend1_First()
    {
        animator_friend1.Play("Friend1IsHit1");
        animator_player.Play("PlayerMakeFunOfFriend1");
        animator_friend2.Play("Friend2MakeFunOf");
    }
    // Friend1を2番目以降に叩く
    private void HitFriend1_Next()
    {
        animator_friend1.Play("Friend1IsHit2");
    }
    // Friend2を最初に叩く
    private void HitFriend2_First()
    {
        animator_friend2.Play("Friend2IsHit1");
        animator_player.Play("PlayerMakeFunOfFriend2");
        animator_friend1.Play("Friend1MakeFunOf");
    }
    // Friend2を2番目以降に叩く
    private void HitFriend2_Next()
    {
        animator_friend2.Play("Friend2IsHit2");
    }
    // ----------------------------------
}
