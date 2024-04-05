using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class ClerkAnimaCnt_28 : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject ramenM;
    [SerializeField] Sprite slimeRamenSpr;
    [SerializeField] Sprite garlickyRamenSpr;

    // 料理を提供するアニメーション開始時
    private void ActiveRamenM()
    {
        // Playerの前にラーメンを表示
        ramenM.GetComponent<SpriteRenderer>().enabled = true;
    }
    // 料理を提供するアニメーション終了時
    private void PlayerEatARamen()
    {
        // スライムラーメンなら即座にゲームオーバー
        if (ramenM.GetComponent<SpriteRenderer>().sprite == slimeRamenSpr)
        {
            player.GetComponent<Animator>().SetBool("DeadFlag", true);
        }
        // それ以外ならPlayerがラーメンを食べるアニメーション再生
        else
        {
            // ニンニクラーメンならラーメンを食べた後ゲームオーバー
            if (ramenM.GetComponent<SpriteRenderer>().sprite == garlickyRamenSpr)
            {
                player.GetComponent<Animator>().SetBool("OverFlag", true);
            }

            player.GetComponent<Animator>().Play("PlayerEat1");
        }
    }
}
