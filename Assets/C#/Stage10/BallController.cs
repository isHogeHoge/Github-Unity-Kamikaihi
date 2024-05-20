using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    [SerializeField] Animator animator_LEnemy;
    [SerializeField] Animator animator_MEnemy;
    [SerializeField] Animator animator_REnemy;
    [SerializeField] Animator animator_friend;
    [SerializeField] Animator animator_player;

    // Playerが投げたボールのアニメーション終了後
    // Enemyがボールをキャッチするアニメーション再生
    private void PlayEnemyCatchAnima(string someone) // ボールをキャッチするEnemy
    {
        switch (someone)
        {
            case "LEnemy":
                animator_LEnemy.Play("LEnemyCatch");
                break;
            case "MEnemy":
                animator_MEnemy.Play("MEnemyCatch");
                break;
            case "REnemy":
                animator_REnemy.Play("REnemyCatch");
                break;
            default:
                Debug.Log("無効な文字列です");
                break;
        }
    }

    // Friendが投げたボールのアニメーション再生中
    // Enemyがボールにヒットするアニメーション再生
    private void PlayEnemiesHitAnima(string someone) // ボールにヒットしたEnemy
    {
        switch (someone)
        {
            case "LEnemy":
                animator_LEnemy.Play("LEnemyHit");
                break;
            case "MEnemy":
                animator_MEnemy.Play("MEnemyHit");
                break;
            case "REnemy":
                animator_REnemy.Play("REnemyHit");
                break;
            default:
                Debug.Log("無効な文字列です");
                break;
        }
    }

    // Friendが投げたボールがバウンドした後
    private void PlayGameClearAnima()
    {
        // ゲームクリアアニメーション再生
        animator_friend.Play("FriendStop");
        animator_player.Play("PlayerClear");

    }
    
}


