using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController_22 : MonoBehaviour
{
    [SerializeField] GameObject player;

    private EnemyController_22 ec_22;
    private Animator animator;   // 自身のアニメーター
    private Rigidbody2D rbody2d; // 自身のRigidbody2D
    private Vector3 playerPos;
    private Vector3 enemyPos; 
    private const float speed = 1.2f;

    void Start()
    {
        ec_22 = this.GetComponent<EnemyController_22>();
        animator = this.GetComponent<Animator>();
        rbody2d = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // ポーズ中ならUpdateを抜ける
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        // Playerと自身の座標を代入
        playerPos = player.transform.position;
        enemyPos = this.transform.position;

        // Playerの座標に応じて移動アニメーション再生
        // --- 横方向移動 ---
        if (Mathf.Abs(playerPos.y - enemyPos.y) <= 0.5f)
        {
            // 左
            if (playerPos.x < enemyPos.x)
            {
                animator.Play("EnemyMove_Left");
            }
            // 右
            else if (playerPos.x > enemyPos.x)
            {
                animator.Play("EnemyMove_Right");
            }
        }
        // --- 上方向移動 ---
        else if (playerPos.y > enemyPos.y)
        {
            // 上
            if (Mathf.Abs(playerPos.x - enemyPos.x) <= 0.5f)
            {
                animator.Play("EnemyMove_Up");
            }
            // 左上
            else if (playerPos.x < enemyPos.x)
            {
                animator.Play("EnemyMove_TopLeft");
            }
            // 右上
            else if (playerPos.x > enemyPos.x)
            {
                animator.Play("EnemyMove_TopRight");
            }
        }
        // --- 下方向移動 ---
        else if (playerPos.y < enemyPos.y)
        {
            // 下
            if (Mathf.Abs(playerPos.x - enemyPos.x) <= 0.5f)
            {
                animator.Play("EnemyMove_Down");
            }
            // 左下
            else if (playerPos.x < enemyPos.x)
            {
                animator.Play("EnemyMove_BottomLeft");
            }
            // 右下
            else if (playerPos.x > enemyPos.x)
            {
                animator.Play("EnemyMove_BottomRight");
            }
        }
        
    }
    
    private void FixedUpdate()
    {
        // ポーズ中またはこのスクリプトが非アクティブUpdateを抜ける
        if (Mathf.Approximately(Time.timeScale, 0f) || !ec_22.enabled)
        {
            return;
        }

        // Playerとの距離が一定以下なら、Playerを追従する
        if (Vector2.Distance(playerPos,this.transform.position) <= 3f)
        {
            rbody2d.MovePosition(Vector3.MoveTowards(this.transform.position, playerPos, speed * Time.deltaTime));
        }
    }


}
