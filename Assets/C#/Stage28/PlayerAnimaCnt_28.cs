using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimaCnt_28 : MonoBehaviour
{
    [SerializeField] Button btn_BoardOfUsualRamen;
    [SerializeField] Button btn_BoardOfGarlickyRamen;
    [SerializeField] Button btn_BoardOfSlimeRamen;
    [SerializeField] SpriteRenderer sr_signBoard;
    [SerializeField] GameObject hunter;
    [SerializeField] Animator animator_clerk;

    private Animator animator_player;
    private int playCount_EatAnima = 0; // "PlayerEat"アニメーションの再生回数
    private void Start()
    {
        animator_player = this.GetComponent<Animator>();
    }

    // 席についた後、注文をできるようにする
    private void CanOrder()
    {
        animator_clerk.Play("ClerkWalk");
        btn_BoardOfUsualRamen.enabled = true;
        btn_BoardOfGarlickyRamen.enabled = true;
        btn_BoardOfSlimeRamen.enabled = true;
    }

    // 料理を待つアニメーション開始時、Clerkが料理を作るアニメーション再生
    private void PlayClerkCookAnima()
    {
        animator_clerk.Play("ClerkCook");
    }

    // 料理を食べるアニメーション終了時
    private void isHunterAppearAndPlayerStandUp()
    {
        playCount_EatAnima++;
        // このアニメーションが2回再生されていたら、Hunterを出現させる
        if (playCount_EatAnima == 2)
        {
            hunter.GetComponent<SpriteRenderer>().enabled = true;
        }
        // 4回再生されていたら、席を立つアニメーション再生
        else if (playCount_EatAnima == 4)
        {
            animator_player.Play("PlayerStandUp");
        }

    }

    // 店を出るアニメーション開始時
    private void isHunterGoBackOrGoIn()
    {
        // 店の前に「準備中」の看板があれば、Hunterが帰りゲームクリア
        if (sr_signBoard.enabled)
        {
            hunter.GetComponent<SpriteRenderer>().enabled = false;
        }
        // 店の前に「準備中」の看板がなければ、Hunterが入ってきてゲームオーバー
        else
        {
            hunter.GetComponent<Animator>().Play("HunterGoIn1");
            animator_player.SetBool("OverFlag", true);
        }
    }

}
