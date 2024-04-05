using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager_12And20 : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject speechBubble;

    private Animator animator;  // Playerのアニメーター
    private bool gotCandyItem = false;  // キャンディーアイテム取得フラグ

    private void Start()
    {
        animator = player.GetComponent<Animator>();
    }

    // -----------  Button -----------
    // (Player)帽子を脱ぐアニメーション再生ボタン
    public void ClickBtn_TakeOffAHat()
    {
        // 帽子を脱ぐアニメーション再生
        if (gotCandyItem)
        {
            animator.Play("PlayerTakeOffAHat2"); // 頭上にキャンディー有
        }
        else
        {
            animator.Play("PlayerTakeOffAHat1"); // 頭上にキャンディー無
        }
        
    }
    // (Player)帽子をかぶるアニメーション再生ボタン
    public void ClickBtn_PutOnAHat()
    {
        // 帽子をかぶるアニメーション再生
        if (gotCandyItem)
        {
            animator.Play("PlayerPutOnAHat2"); // 頭上にキャンディー有
        }
        else
        {
            animator.Play("PlayerPutOnAHat1"); // 頭上にキャンディー無
        }
    }
    // キャンディーアイテム
    public void ClickCandyItemBtn()
    {
        // キャンディー取得済みに
        gotCandyItem = true;
        // 帽子をかぶるアニメーション再生
        animator.Play("PlayerPutOnAHat2");
    }
    // -----------------------------------


}
