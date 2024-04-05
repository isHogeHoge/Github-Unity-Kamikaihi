using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class BrotherAnimaCnt_24 : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject garland;  // Brotherのアクセサリー(花輪)
    [SerializeField] GameObject strawberry; // Brotherのアクセサリー(イチゴ)
    [SerializeField] GameObject stageManager;

    // 出現アニメーション終了時
    private void PlayAccessoriesSwayAnima()
    {
        // (Brotherの動きに合わせて)飾りアイテムが揺れるアニメーション再生
        this.transform.GetChild(0).GetComponent<Animator>().Play("AccessoriesSway");

    }

    // 停止アニメーション終了時
    private void isPlayBrotherScareAnima()
    {
        // Brotherに飾りアイテムを1つも使用していなかったら、Playerを驚かすアニメーション再生
        if(!garland.GetComponent<SpriteRenderer>().enabled && !strawberry.GetComponent<SpriteRenderer>().enabled)
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
        player.GetComponent<Animator>().Play("PlayerOver2");
    }

    // Centaurに追いかけられるアニメーション開始時
    private void PlayAccessoriesStopAnima()
    {
        // 飾りアイテムアニメーション停止
        this.transform.GetChild(0).GetComponent<Animator>().Play("AccessoriesStop");
    }
}
