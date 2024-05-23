using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    [SerializeField] GameObject clickCancelPnl;
    [SerializeField] Animator animator_player;

    private bool isOver = false;   // ゲームオーバーフラグ

    private void Update()
    {
        // ポーズ中またはゲームオーバーならUpdateを抜ける
        if (Mathf.Approximately(Time.timeScale, 0f)　|| isOver)
        {
            return;
        }

        // アクティブなスライムがあるか調べる
        for(var i = 0; i < this.transform.childCount; i++)
        {
            GameObject slime = this.transform.GetChild(i).gameObject;
            if (slime.activeSelf)
            {
                return;
            }
        }
        // slimeがすべて非アクティブなら、ゲームオーバー処理
        clickCancelPnl.SetActive(true);
        animator_player.Play("PlayerIsPickedup");
        isOver = true;

    }

}
