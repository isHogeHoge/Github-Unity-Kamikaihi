using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendController_10 : MonoBehaviour
{
    [SerializeField] GameObject magicCircle;
    [SerializeField] GameObject ball;

    private int count = 0; // "FriendStop"アニメーション再生回数

    // Friend召喚後、魔法陣アニメーション終了
    private void StopMagicCircleAnima()
    {
        magicCircle.GetComponent<Animator>().Play("MagicCircleFinish");
    }

    // "FriendStop"アニメーションが3回再生されていたら、ボールを投げるアニメーション再生
    private void FriendThrowABall()
    {
        count++;
        if(count == 3)
        {
            this.GetComponent<Animator>().Play("FriendThrow");
        }
    }

    // ボールを投げるモーション終了後
    // ボールがEnemyの方へ向かっていくアニメーション再生
    private void BallIsThrowedAtEnemies()
    {
        ball.GetComponent<Animator>().Play("IsThrowedByFriend");
        ball.GetComponent<Image>().enabled = true;
    }
}
