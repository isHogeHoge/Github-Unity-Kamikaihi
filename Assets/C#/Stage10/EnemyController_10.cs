using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController_10 : MonoBehaviour
{
    [SerializeField] Animator animator_MEnemy;
    [SerializeField] Animator animator_player;
    [SerializeField] GameObject ball;

    // LEnemyがボールをキャッチした後
    private void PlayMEnemyTurnLAnima()
    {
        // (MEnemyが)LEnemyの方を向きボールを受け取るアニメーション再生
        animator_MEnemy.GetComponent<Animator>().Play("MEnemyTurnL");
    }
    // REnemyがボールをキャッチした後
    private void PlayMEnemyTurnRAnima()
    {
        // (MEnemyが)REnemyの方を向きボールを受け取るアニメーション再生
        animator_MEnemy.GetComponent<Animator>().Play("MEnemyTurnR");
    }

    // MEnemyがボールを投げるモーション終了後
    // ボールがPlayerの方に向かっていくアニメーション再生
    private void BallIsThrowedAtPlayer()
    {
        ball.GetComponent<Animator>().Play("IsThrowedByMEnemy");
        ball.GetComponent<Image>().enabled = true;
    }
    // Playerがボールにヒットするアニメーション再生(ゲームオーバー)
    private void PlayPlayerHitAnima()
    {
        animator_player.GetComponent<Animator>().Play("PlayerHit");
    }
}
