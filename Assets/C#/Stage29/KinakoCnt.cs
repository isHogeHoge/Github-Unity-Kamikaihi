using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinakoCnt : MonoBehaviour
{
    private Transform slime; // 対象のスライム(親オブジェクト)のTransform

    private void Start()
    {
        slime = this.transform.parent;
    }

    // きな粉を振りかけるアニメーション開始時、スライムの当たり判定をなくす
    private void InActiveSlimesCollider()
    {
        slime.GetComponent<BoxCollider2D>().enabled = false;
        slime.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
    }
    // きな粉をふりかけるアニメーション終了時
    private void PlayFadeOutAnima()
    {
        // スライム&スライム上のきな粉をフェードアウト
        slime.GetComponent<Animator>().Play("SlimeFadeOut");
        slime.GetChild(1).GetComponent<Animator>().enabled = true;
    }

    // フェードアウト後スライムを非アクティブに
    private void InActiveSlime()
    {
        slime.gameObject.SetActive(false);
    }
}
