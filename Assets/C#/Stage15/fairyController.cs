using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
public class fairyController : MonoBehaviour
{
    [SerializeField] GameObject flowersEffect;
    [SerializeField] GameObject player;
    [SerializeField] GameObject fairyBtn;
    [SerializeField] GameObject goBtn;
    [SerializeField] GameObject lButton;
    // --------- Animation ----------
    // 出現アニメーション終了後、Playerの方へ移動可能に
    private void AcitvefairyBtn()
    {
        fairyBtn.GetComponent<Image>().enabled = true;
    }

    // (Playerの手前で)停止アニメーション再生時
    private void PlayflowersEffectAnima()
    {
        // Playerの周りにエフェクトを表示
        flowersEffect.GetComponent<Animator>().enabled = true;
    }
    // (Playerの手前で)停止アニメーション終了後
    private void PlayPlayerWearHazmatSuitsAnima()
    {
        // Playerに防護服を着せる
        player.GetComponent<Animator>().Play("PlayerWearHazmatSuits");

    }

    // 移動終了時
    private void CanGameControl()
    {
        // ゲーム操作を可能に
        goBtn.GetComponent<Image>().enabled = true;
        lButton.GetComponent<Image>().enabled = true;
        player.GetComponent<BoxCollider2D>().enabled = true;
    }
    // ------------------------------
}
