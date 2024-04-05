using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    [SerializeField] GameObject lEnemy;
    [SerializeField] GameObject mEnemy;
    [SerializeField] GameObject rEnemy;
    [SerializeField] GameObject player;
    [SerializeField] GameObject friend;

    // Playerが投げたボールのアニメーション終了後
    // Enemyがボールをキャッチするアニメーション再生
    private void PlayEnemyCatchAnima(string someone) // ボールをキャッチするEnemy
    {
        switch (someone)
        {
            case "LEnemy":
                lEnemy.GetComponent<Animator>().Play("LEnemyCatch");
                break;
            case "MEnemy":
                mEnemy.GetComponent<Animator>().Play("MEnemyCatch");
                break;
            case "REnemy":
                rEnemy.GetComponent<Animator>().Play("REnemyCatch");
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
                lEnemy.GetComponent<Animator>().Play("LEnemyHit");
                break;
            case "MEnemy":
                mEnemy.GetComponent<Animator>().Play("MEnemyHit");
                break;
            case "REnemy":
                rEnemy.GetComponent<Animator>().Play("REnemyHit");
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
        friend.GetComponent<Animator>().Play("FriendStop");
        player.GetComponent<Animator>().Play("PlayerClear");

    }
    
}


