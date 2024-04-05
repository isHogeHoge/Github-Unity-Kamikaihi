using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class LEFT : IDirection
{
    private const float distanceX = 0.51f; // マス目間の距離X

    // Player停止アニメーション再生(左)
    public void PlayStopAnima(Animator animator)
    {
        animator.Play("PlayerStop_LEFT");
    }
    // Player移動アニメーション再生(左)
    public void PlayMoveAnima(Animator animator)
    {
        animator.Play("PlayerMove_LEFT");
    }
    // Playerキックアニメーション再生(左)
    public void PlayKickAnima(Animator animator)
    {
        animator.Play("PlayerKick_LEFT");
    }
    // player,slimeの移動先ポジション設定
    public Vector3 SetTargetPos(GameObject obj)
    {
        // 移動させるのがPlayerの場合、移動先に壁がないか調べる
        if (obj.tag == "Player")
        {
            // Playerの足元座標を取得
            float playerHeight = obj.GetComponent<SpriteRenderer>().bounds.size.y;
            Vector3 playerPos = new Vector3(obj.transform.position.x, obj.transform.position.y - playerHeight / 2, obj.transform.position.z);
            // 移動先にあるオブジェクトを全て取得
            RaycastHit2D[] hit2ds = Physics2D.RaycastAll(playerPos, new Vector3(-1, 0, 0), distanceX);
            foreach (var i in hit2ds)
            {
                // 移動先に壁があれば、Playerを移動させない
                if (i.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    return obj.transform.position;
                }
            }

        }
        // (移動させるのがSlime、またはPlayerの移動先に壁がない場合)1マス先の移動先ポジションを返す
        return obj.transform.position + new Vector3(-1, 0, 0) * distanceX;
    }
    // キックしたスライムの移動先に障害物(スライム,壁)がないか調べる
    public bool ObstacleExistsToDistination(GameObject col_slime)
    {
        Vector3 col_slimePos = col_slime.transform.position;

        // キックしたスライムの左隣にオブジェクトがないか調べる(rayを+左1マス先へ発射)
        RaycastHit2D[] hit2ds = Physics2D.RaycastAll(col_slimePos, new Vector3(-1, 0, 0), distanceX);
        foreach (var i in hit2ds)
        {
            GameObject obj = i.transform.gameObject;
            // rayに当たったオブジェクトがキックしたスライム以外なら、スライムを移動させない(true)
            if (obj != col_slime)
            {
                // スライムが揺れるアニメーション再生
                col_slime.GetComponent<Animator>().Play("SlimeSway");
                return true;
            }

        }
        // rayに当たったオブジェクトがないなら、スライムを1マス分移動させる(false)
        return false;
    }

    // Playerの進行方向1マス先にあるスライムを取得する(なければnull)
    public GameObject GetSlimeInFrontOfPlayer(GameObject player)
    {
        // Playerの足元座標を取得
        float playerHeight = player.gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
        Vector3 playerPos = new Vector3(player.transform.position.x, player.transform.position.y - playerHeight / 2, player.transform.position.z);
        // Playerの左隣にあるオブジェクトがSlimeなら取得
        RaycastHit2D[] hit2ds = Physics2D.RaycastAll(playerPos, new Vector3(-1, 0, 0), distanceX);
        foreach (var i in hit2ds)
        {
            if (i.transform.tag == "Enemy")
            {
                return i.transform.gameObject;
            }
        }
        return null;
    }
}
