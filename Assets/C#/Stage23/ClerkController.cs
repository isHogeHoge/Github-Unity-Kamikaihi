using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ClerkController : MonoBehaviour
{
    [SerializeField] Button coin1Btn; // 自身の足元にあるコイン取得ボタン
    [SerializeField] Animator animator_player;
    
    private Animator animator_clerk; 
    private List<float> speeds_readAnima; // 新聞を読むアニメーションの再生スピード候補
    private List<float> speeds_turnAnima; // 右を向くアニメーションの再生スピード候補
    private float playCount_readAnima = 0;   // 新聞を読むアニメーションの再生回数

    void Start()
    {
        animator_clerk = this.GetComponent<Animator>();
        // 新聞を読むアニメーションの再生スピードを0.25,0.5,1のいずれかに
        speeds_readAnima = new List<float> { 0.25f, 0.5f, 1f};
        // 左を向くアニメーションの再生スピードを0.5,1,2のいずれかに
        speeds_turnAnima = new List<float> { 0.5f, 1f, 2f };
    }

    // 新聞を読むアニメーション開始時
    private void SelectThisAnimationSpeed()
    {
        // このアニメーションの再生スピードをランダムに選出
        animator_clerk.SetFloat("Speed", speeds_readAnima[Random.Range(0, speeds_readAnima.Count)]);
    }

    // 新聞を読むアニメーション終了時
    // アニメーションの再生回数に応じて、向く方向(右・左)を設定
    private void isTurnRightorLeft()
    {
        // このアニメーション再生回数を+1
        playCount_readAnima++;

        // このアニメーションが10回再生されていたら、左を向くアニメーション再生 & 足元のコイン取得可能に
        if(playCount_readAnima == 10)
        {
            animator_clerk.Play("ClerkTurnLeft");
            coin1Btn.enabled = true;

            playCount_readAnima = 0;
        }
        // 10回未満なら、右を向くアニメーション再生
        else
        {
            // アニメーションの再生スピードをランダムに選出
            animator_clerk.SetFloat("Speed", speeds_turnAnima[Random.Range(0, speeds_turnAnima.Count)]);
            animator_clerk.Play("ClerkTurnRight");
        }
    }

    // 左を向くアニメーション終了時
    private void InActiveCoin1Btn()
    {
        // 足元のコイン取得不可能に
        coin1Btn.enabled = false;
    }

    // "ClerkIsSurprised"アニメーション終了時
    private void PlayPlayerOverAnima()
    {
        // Playerゲームオーバーアニメーション再生
        animator_player.Play("PlayerOver2");
    }

}
