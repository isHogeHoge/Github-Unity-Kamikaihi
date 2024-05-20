using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager_12And20 : MonoBehaviour
{
    [SerializeField] Animator animator_player;
    private bool gotCandyItem = false;  // キャンディーアイテム取得フラグ

    // -----------  Button -----------
    // (Player)帽子を脱ぐアニメーション再生ボタン
    public void ClickBtn_TakeOffAHat()
    {
        // 帽子を脱ぐアニメーション再生
        if (gotCandyItem)
        {
            animator_player.Play("PlayerTakeOffAHat2"); // 頭上にキャンディー有
        }
        else
        {
            animator_player.Play("PlayerTakeOffAHat1"); // 頭上にキャンディー無
        }
        
    }
    // (Player)帽子をかぶるアニメーション再生ボタン
    public void ClickBtn_PutOnAHat()
    {
        // 帽子をかぶるアニメーション再生
        if (gotCandyItem)
        {
            animator_player.Play("PlayerPutOnAHat2"); // 頭上にキャンディー有
        }
        else
        {
            animator_player.Play("PlayerPutOnAHat1"); // 頭上にキャンディー無
        }
    }
    // キャンディーアイテム
    public void ClickCandyItemBtn()
    {
        // キャンディー取得済みに
        gotCandyItem = true;
        // 帽子をかぶるアニメーション再生
        animator_player.Play("PlayerPutOnAHat2");
    }
    // -----------------------------------


}
