using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkController : MonoBehaviour
{
    [SerializeField] GameObject trio;
    [SerializeField] GameObject player;
    [SerializeField] GameObject friend1;
    [SerializeField] GameObject friend2;
    [SerializeField] GameObject stageManager;

    private Animator animator;
    private StageManager_18 sm_18;
    internal bool isMoving = true;
    internal Vector3 targetPos; // 移動先ポジション
    internal float moveSpeed = 0.5f;  // 自身の移動スピード

    void Start()
    {
        animator = this.GetComponent<Animator>();
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
                animator.Play("MonkWalkL");
            }
            // (targetPosまでの)変位がプラスなら、右を向く
            else if (this.transform.position.x < targetPos.x)
            {
                this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                animator.Play("MonkWalkR");
            }
        }

        // ゲームオーバーアニメーション再生処理
        if (this.transform.position == targetPos && sm_18.gameState == GameState.gameOver)
        {
            isMoving = false;
            // アニメーションの座標変更を反映する
            animator.applyRootMotion = false;

            // 僧侶のX座標がplayerと等しいなら
            if (this.transform.position.x == player.transform.position.x)
            {
                // 僧侶がplayer→friend2→friend1の順番に頭を叩くアニメーション再生
                animator.Play("MonkHitPlayer~");
            }
            // friend1と等しいなら
            else if(this.transform.position.x == friend1.transform.position.x)
            {
                // 僧侶がfriend1→player→friend2の順番に頭を叩くアニメーション再生
                animator.Play("MonkHitFriend1~");
            }
            // friend2と等しいなら
            else
            {
                // 僧侶がfriend2→player→friend1の順番に頭を叩くアニメーション再生
                animator.Play("MonkHitFriend2~");
            }

        }
    }

    // ----------- Animation ------------
    // "MonkStop"アニメーション再生中
    // 自身の吹き出しアニメーション再生
    private void PlaySpeechBubbleAnima()
    {
        // 吹き出しのAnimatorを取得
        Animator animator_Down = this.transform.GetChild(0).GetComponent<Animator>();
        Animator animator_Up = this.transform.GetChild(1).GetComponent<Animator>();

        // 初期アニメーションに移行しないようにする
        animator_Down.SetBool("isStart", false);
        animator_Up.SetBool("isStart", false);

        // 吹き出し(下)のアニメーション再生
        animator_Down.Play("Monk'sSB_Down",0,0);
        this.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        // 吹き出し(上下)アニメーションのスピードを変更
        animator_Down.SetFloat("Speed", 0.8f);
        animator_Up.SetFloat("Speed", 0.7f);
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
        animator.SetFloat("Speed", changedSpeed);
    }
    // Playerを最初に叩く
    private void HitPlayer_First()
    {
        player.GetComponent<Animator>().Play("PlayerIsHit1");
        friend1.GetComponent<Animator>().Play("Friend1MakeFunOf");
        friend2.GetComponent<Animator>().Play("Friend2MakeFunOf");
    }
    // Playerを2番目以降に叩く
    private void HitPlayer_Next()
    {
        player.GetComponent<Animator>().Play("PlayerIsHit2");
    }
    // Friend1を最初に叩く
    private void HitFriend1_First()
    {
        friend1.GetComponent<Animator>().Play("Friend1IsHit1");
        player.GetComponent<Animator>().Play("PlayerMakeFunOfFriend1");
        friend2.GetComponent<Animator>().Play("Friend2MakeFunOf");
    }
    // Friend1を2番目以降に叩く
    private void HitFriend1_Next()
    {
        friend1.GetComponent<Animator>().Play("Friend1IsHit2");
    }
    // Friend2を最初に叩く
    private void HitFriend2_First()
    {
        friend2.GetComponent<Animator>().Play("Friend2IsHit1");
        player.GetComponent<Animator>().Play("PlayerMakeFunOfFriend2");
        friend1.GetComponent<Animator>().Play("Friend1MakeFunOf");
    }
    // Friend2を2番目以降に叩く
    private void HitFriend2_Next()
    {
        friend2.GetComponent<Animator>().Play("Friend2IsHit2");
    }
    // ----------------------------------
}
