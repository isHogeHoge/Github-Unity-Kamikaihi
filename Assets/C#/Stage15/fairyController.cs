using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
public class fairyController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Animator animator_flowersEffect;
    [SerializeField] Image img_fairyBtn;
    [SerializeField] Image img_goBtn;
    [SerializeField] Image img_LButton;

    // --------- Animation ----------
    // 出現アニメーション終了後、Playerの方へ移動可能に
    private void AcitvefairyBtn()
    {
        img_fairyBtn.enabled = true;
    }

    // (Playerの手前で)停止アニメーション再生時
    private void PlayflowersEffectAnima()
    {
        // Playerの周りにエフェクトを表示
        animator_flowersEffect.enabled = true;
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
        img_goBtn.enabled = true;
        img_LButton.enabled = true;
        player.GetComponent<BoxCollider2D>().enabled = true;
    }
    // ------------------------------
}
