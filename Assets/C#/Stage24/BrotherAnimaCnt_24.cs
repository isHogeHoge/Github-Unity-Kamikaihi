using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class BrotherAnimaCnt_24 : MonoBehaviour
{
    [SerializeField] Animator animator_player;
    [SerializeField] Animator animator_accessories;
    [SerializeField] SpriteRenderer sr_garland;  // アクセサリー(花輪)
    [SerializeField] SpriteRenderer sr_strawberry; // アクセサリー(イチゴ)
    [SerializeField] GameObject stageManager;

    // 出現アニメーション終了時
    private void PlayAccessoriesSwayAnima()
    {
        // (Brotherの動きに合わせて)飾りアイテムが揺れるアニメーション再生
        animator_accessories.Play("AccessoriesSway");

    }

    // 停止アニメーション終了時
    private void isPlayBrotherScareAnima()
    {
        // Brotherに飾りアイテムを1つも使用していなかったら、Playerを驚かすアニメーション再生
        if(!sr_garland.enabled && !sr_strawberry.enabled)
        {
            this.GetComponent<Animator>().SetBool("ScareFlag", true);
        }
        
    }

    // Playerを驚かすアニメーション開始時
    private void PlayPlayerOverAnima()
    {
        // アイテム使用不可能に
        this.GetComponent<BoxCollider2D>().enabled = false;
        // Playerゲームオーバーアニメーション再生
        animator_player.Play("PlayerOver2");
    }

    // Centaurに追いかけられるアニメーション開始時
    private void PlayAccessoriesStopAnima()
    {
        // 飾りアイテムアニメーション停止
        animator_accessories.Play("AccessoriesStop");
    }
}
