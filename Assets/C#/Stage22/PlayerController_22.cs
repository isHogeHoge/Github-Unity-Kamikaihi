using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;


public class PlayerController_22 : MonoBehaviour
{
    [SerializeField] GameObject topBorder;
    [SerializeField] GameObject blueEnemy; 
    [SerializeField] GameObject redEnemy;
    [SerializeField] GameObject stageManager;

    private CancelClickOutOfScreen cancelClickCnt;
    private Animator animator_player;   
    private Rigidbody2D rbody_player; 
    private Vector3 targetPos; 
    private const float speed = 2f;  // 移動スピード
    private bool canPlayMoveAnima = false; // 移動アニメーション再生可能フラグ
    internal bool stopMoving = false; // 移動停止フラグ
    internal bool isHitEnemy = false;  // 敵との接触フラグ
    

    void Start()
    {
        cancelClickCnt = stageManager.GetComponent<CancelClickOutOfScreen>();
        animator_player = this.GetComponent<Animator>();
        rbody_player = this.GetComponent<Rigidbody2D>();

        // Playerの現在位置を最初の移動先座標に設定
        targetPos = this.transform.position;
    }

    private void Update()
    {
        // ゲーム中でないならUpdateを抜ける
        if (Mathf.Approximately(Time.timeScale, 0f) || stopMoving)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && cancelClickCnt.isWithinTheGameScreen())
        {
            // --- Playerの移動先座標の取得 ---
            Vector3 mousePos = Input.mousePosition;
            //スクリーン座標をワールド座標に変換(移動先座標の設定)
            targetPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
            // ------------------------------

            // ++++ ゲーム画面外でのクリックを無効に　++++
            // クリックした場所にあるゲームオブジェクトを全て取得
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll((Vector2)ray.origin, (Vector2)ray.direction, Mathf.Infinity);

            // TopBorder上のクリックを無効に
            foreach (var i in hits)
            {
                if (i.collider.transform.gameObject == topBorder)
                {
                    targetPos = this.transform.position;
                    return;
                }
            }
            // ++++++++++++++++++++++++++++++++++++++++

            // 移動アニメーション再生に移る
            canPlayMoveAnima = true;

        }


        // ++++ (targetPosまで)移動アニメーションの再生 ++++
        if (canPlayMoveAnima)
        {
            Vector3 playerPos = this.transform.position;

            // targetPosに応じて移動アニメーション再生
            // --- 横方向移動 ---
            if (Mathf.Abs(targetPos.y - playerPos.y) <= 0.5f)
            {
                // 左
                if (targetPos.x < playerPos.x)
                {
                    animator_player.Play("PlayerMove_Left");
                }
                // 右
                else if (targetPos.x > playerPos.x)
                {
                    animator_player.Play("PlayerMove_Right");
                }
            }
            // --- 上方向移動 ---
            else if (targetPos.y > playerPos.y)
            {
                // 上
                if (Mathf.Abs(targetPos.x - playerPos.x) <= 0.5f)
                {
                    animator_player.Play("PlayerMove_Up");
                }
                // 左上
                else if (targetPos.x < playerPos.x)
                {
                    animator_player.Play("PlayerMove_TopLeft");
                }
                // 右上
                else if (targetPos.x > playerPos.x)
                {
                    animator_player.Play("PlayerMove_TopRight");
                }
            }
            // --- 下方向移動 ---
            else if (targetPos.y < playerPos.y)
            {
                // 下
                if (Mathf.Abs(targetPos.x - playerPos.x) <= 0.5f)
                {
                    animator_player.Play("PlayerMove_Down");
                }
                // 左下
                else if (targetPos.x < playerPos.x)
                {
                    animator_player.Play("PlayerMove_BottomLeft");
                }
                // 右下
                else if (targetPos.x > playerPos.x)
                {
                    animator_player.Play("PlayerMove_BottomRight");
                }
            }
            
            // 移動中、アニメーション(方向)が変わらないようにする
            canPlayMoveAnima = false;
        }
        
        // ++++++++++++++++++++++++++++++++++++++++++++++

    }

    private void FixedUpdate()
    {
        // ゲーム中でないならUpdateを抜ける
        if (Mathf.Approximately(Time.timeScale, 0f) || stopMoving)
        {
            return;
        }

        // 移動先座標まで一定のスピードで移動
        rbody_player.MovePosition(Vector3.MoveTowards(this.transform.position, targetPos, speed * Time.deltaTime));

    }

    // 障害物と衝突時、移動ストップ(滑らないようにする)
    private void OnCollisionEnter2D(Collision2D col)
    {
        targetPos = this.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // 敵と接触した時
        if (col.gameObject.CompareTag("Enemy"))
        {
            isHitEnemy = true;
            stopMoving = true;
            targetPos = this.transform.position;
            animator_player.Play("PlayerOver");

            // 敵と反発しないようにする
            col.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            rbody_player.bodyType = RigidbodyType2D.Kinematic;

            // ゲームオーバー処理
            stageManager.GetComponent<StageManager>().GameOver(this.GetCancellationTokenOnDestroy()).Forget();
            
        }
        // 公園出口付近に近づいたら、初回のみ敵(青)出現
        else if (col.gameObject.CompareTag("Appear"))
        {
            blueEnemy.GetComponent<SpriteRenderer>().enabled = true;
            blueEnemy.GetComponent<Animator>().enabled = true;

            col.GetComponent<BoxCollider2D>().enabled = false;
        }

    }

}
