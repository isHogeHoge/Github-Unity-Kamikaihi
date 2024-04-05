using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager_19 : MonoBehaviour
{
    [SerializeField] GameObject player1; // 移動前
    [SerializeField] GameObject player2; // 移動中

    // 料理(TempuraBtn)クリック時
    public void ClickTempuraBtn()
    {
        // Playerが料理の方に向かうアニメーション再生
        // エプロン着用⚪︎
        if (player1.GetComponent<Player1Controller_19>().isWearingApron)
        {
            player2.GetComponent<Animator>().Play("PlayerInAApronWalk");
        }
        // エプロン着用×
        else
        {
            player2.GetComponent<Animator>().Play("PlayerWalk");
        }
    }
}
