using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class ClerkAnimaCnt_28 : MonoBehaviour
{
    [SerializeField] Animator animator_player;
    [SerializeField] SpriteRenderer sr_ramenM;
    [SerializeField] Sprite slimeRamenSpr;
    [SerializeField] Sprite garlickyRamenSpr;

    // 料理を提供するアニメーション開始時
    private void ActiveRamenM()
    {
        // Playerの前にラーメンを表示
        sr_ramenM.enabled = true;
    }
    // 料理を提供するアニメーション終了時
    private void PlayerEatARamen()
    {
        // スライムラーメンなら即座にゲームオーバー
        if (sr_ramenM.sprite == slimeRamenSpr)
        {
            animator_player.SetBool("DeadFlag", true);
        }
        // それ以外ならPlayerがラーメンを食べるアニメーション再生
        else
        {
            // ニンニクラーメンならラーメンを食べた後ゲームオーバー
            if (sr_ramenM.sprite == garlickyRamenSpr)
            {
                animator_player.SetBool("OverFlag", true);
            }

            animator_player.Play("PlayerEat1");
        }
    }
}
