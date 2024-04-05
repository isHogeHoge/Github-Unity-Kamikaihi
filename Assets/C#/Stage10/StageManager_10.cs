using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StageManager_10 : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject parentOfBall;
    [SerializeField] GameObject ball;
    [SerializeField] GameObject mob;

    // ---------- Button ------------
    // Enemy
    public void ClickEnemyBtn(string someone) // クリックしたEnemy
    {
        // ゲーム操作をできないようにする
        this.gameObject.GetComponent<StageManager>().CantGameControl(); 

        // Playerがボールを投げるアニメーション再生
        player.GetComponent<Animator>().Play("PlayerThrow");

        // (ボールを投げるモーション終了時)クリックしたEnemyの方にボールアニメーション再生
        switch (someone)
        {
            case "LEnemy":
                ball.GetComponent<Animator>().SetBool("AtLEnemy", true);
                break;
            case "MEnemy":
                ball.GetComponent<Animator>().SetBool("AtMEnemy", true);
                break;
            case "REnemy":
                ball.GetComponent<Animator>().SetBool("AtREnemy", true);
                break;
            default:
                Debug.Log("無効な文字列です");
                break;
        }
    }
    // Player
    public void ClickPlayerBtn()
    {
        // ゲーム操作をできないようにする
        this.gameObject.GetComponent<StageManager>().CantGameControl();

        // ボールがPlayerの手前に映るようにLayerを変更
        ball.transform.SetParent(parentOfBall.transform);

        // Playerがボールを落とすアニメーション再生(ゲームオーバー)
        player.GetComponent<Animator>().Play("PlayerDropABall");
        ball.GetComponent<Image>().enabled = true;
        ball.GetComponent<Animator>().Play("BallFall");
        
    }
    // --------------------------------
}
