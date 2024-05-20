using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StageManager_10 : MonoBehaviour
{
    [SerializeField] GameObject mob;
    [SerializeField] GameObject ball;
    [SerializeField] Image img_ball;
    [SerializeField] Animator animator_ball;
    [SerializeField] Animator animator_player;
    [SerializeField] GameObject parentOfBall;

    private StageManager sm;
    private void Start()
    {
        sm = this.GetComponent<StageManager>();
    }

    // ---------- Button ------------
    // Enemy
    public void ClickEnemyBtn(string someone) // クリックしたEnemy
    {
        // ゲーム操作をできないようにする
        sm.CantGameControl(); 

        // Playerがボールを投げるアニメーション再生
        animator_player.GetComponent<Animator>().Play("PlayerThrow");

        // (ボールを投げるモーション終了時)クリックしたEnemyの方にボールアニメーション再生
        switch (someone)
        {
            case "LEnemy":
                animator_ball.SetBool("AtLEnemy", true);
                break;
            case "MEnemy":
                animator_ball.SetBool("AtMEnemy", true);
                break;
            case "REnemy":
                animator_ball.SetBool("AtREnemy", true);
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
        sm.CantGameControl();

        // ボールがPlayerの手前に映るようにLayerを変更
        ball.transform.SetParent(parentOfBall.transform);

        // Playerがボールを落とすアニメーション再生(ゲームオーバー)
        animator_player.GetComponent<Animator>().Play("PlayerDropABall");
        img_ball.enabled = true;
        animator_ball.Play("BallFall");
        
    }
    // --------------------------------
}
