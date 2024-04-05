using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimaCnt_28 : MonoBehaviour
{
    [SerializeField] GameObject btn_BoardOfUsualRamen;
    [SerializeField] GameObject btn_BoardOfGarlickyRamen;
    [SerializeField] GameObject btn_BoardOfSlimeRamen;
    [SerializeField] GameObject signBoard;
    [SerializeField] GameObject hunter;
    [SerializeField] GameObject clerk;

    private int playCount_EatAnima = 0; // "PlayerEat"アニメーションの再生回数

    // 席についた後、注文をできるようにする
    private void CanOrder()
    {
        clerk.GetComponent<Animator>().Play("ClerkWalk");
        btn_BoardOfUsualRamen.GetComponent<Button>().enabled = true;
        btn_BoardOfGarlickyRamen.GetComponent<Button>().enabled = true;
        btn_BoardOfSlimeRamen.GetComponent<Button>().enabled = true;
    }

    // 料理を待つアニメーション開始時、Clerkが料理を作るアニメーション再生
    private void PlayClerkCookAnima()
    {
        clerk.GetComponent<Animator>().Play("ClerkCook");
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
            this.GetComponent<Animator>().Play("PlayerStandUp");
        }

    }

    // 店を出るアニメーション開始時
    private void isHunterGoBackOrGoIn()
    {
        // 店の前に「準備中」の看板があれば、Hunterが帰りゲームクリア
        if (signBoard.GetComponent<SpriteRenderer>().enabled)
        {
            hunter.GetComponent<SpriteRenderer>().enabled = false;
        }
        // 店の前に「準備中」の看板がなければ、Hunterが入ってきてゲームオーバー
        else
        {
            hunter.GetComponent<Animator>().Play("HunterGoIn1");
            this.GetComponent<Animator>().SetBool("OverFlag", true);
        }
    }

}
