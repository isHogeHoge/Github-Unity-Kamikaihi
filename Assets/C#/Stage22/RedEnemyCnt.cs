using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class RedEnemyCnt : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Animator animator_blueEnemy_Goal;
    [SerializeField] GameObject redEnemy_Goal;
    [SerializeField] GameObject stageManager;

    private EnemyController_22 ec_22;
    private PlayerController_22 pc_22;
    private SpriteRenderer sr_redCircle;
    private Animator animator_redEnemy;
    private Vector3 playerPos;

    private void Start()
    {
        ec_22 = this.GetComponent<EnemyController_22>();
        pc_22 = player.GetComponent<PlayerController_22>();
        sr_redCircle = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
        animator_redEnemy = this.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // (ポーズ中)または(すでにPlayer追跡中)ならUpdateを抜ける
        if (Mathf.Approximately(Time.timeScale, 0f) || ec_22.enabled)
        {
            return;
        }

        playerPos = player.transform.position;

        // Playerとの距離が一定以下なら、追跡開始
        if (Vector2.Distance(playerPos, this.transform.position) <= 2.5f)
        {
            animator_redEnemy.Play("EnemyMove_Up");
            ec_22.enabled = true;
            // 足元のサークルを表示
            sr_redCircle.enabled = true;

        }
    }

    // ゴール(赤いサークル)に接触した時、移動ストップ
    private async void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.CompareTag("RedGoal"))
        {
            // 同時にPlayerと接触していたら、ゲームオーバー優先
            if (pc_22.isHitEnemy)
            {
                return;
            }

            // ゴール時のオブジェクトに切り替え
            this.transform.gameObject.SetActive(false);
            redEnemy_Goal.SetActive(true);


            // Playerの移動停止
            pc_22.stopMoving = true;
            player.GetComponent<Animator>().Play("PlayerStop");

            await UniTask.Delay(TimeSpan.FromSeconds(1));

            // 敵(赤・青)が吹き飛ぶアニメーション再生
            redEnemy_Goal.GetComponent<Animator>().enabled = true;
            animator_blueEnemy_Goal.enabled = true;

        }

    }

}
