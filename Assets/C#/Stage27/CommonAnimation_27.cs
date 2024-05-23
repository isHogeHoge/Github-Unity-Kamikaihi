using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class CommonAnimation_27 : MonoBehaviour
{
    [SerializeField] Image img_LButton;
    [SerializeField] SpriteRenderer sr_playersSushi;
    [SerializeField] SpriteRenderer sr_friend1sSushi;
    [SerializeField] SpriteRenderer sr_friend2sSushi;
    [SerializeField] Animator animator_brother;
    [SerializeField] GameObject brothersSushi;
    [SerializeField] GameObject stageManager;
    [SerializeField] GameObject stagePanel_UI; // スクロールさせるUI
    [SerializeField] GameObject stagePanel;    // スクロールさせるゲームオブジェクト
    [SerializeField] Sprite shrimpWithWasabi; // えび寿司(わさびあり)

    // ---------- Player ----------
    // ゲームオーバーアニメーション終了時
    private void PlayBrotherClearAnima()
    {
        animator_brother.Play("BrotherClear");
    }
    // ゲームクリアニメーション終了時
    private void isPlayBrotherOverAnima()
    {
        // Brotherの寿司がえび寿司(わさびあり)かそれ以外かでアニメーションを切り替える
        // アニメーション再生終了時にゲームクリア処理を行う
        if (brothersSushi.GetComponent<SushiController>().sushiSpr == shrimpWithWasabi)
        {
            animator_brother.Play("BrotherOver");
        }
        else
        {
            animator_brother.Play("BrotherStart_Clear");
        }
    }
    // ゲームオーバー(orクリア)アニメーション終了後
    // 右側のページにスクロールする
    private void ScrollStagePnl_Right()
    {
        stagePanel_UI.GetComponent<StageScrollCnt>().ScrollStagePnl("RIGHT");
        stagePanel.GetComponent<StageScrollCnt>().ScrollStagePnl("RIGHT");
        img_LButton.enabled = false;
    }
    // --------------------------------

    // ------- Player,Friend1(2) --------
    // Eatアニメーション開始時、手に取った寿司を非表示
    private void InActivePickedUpSushi(string someone)
    {
        switch (someone)
        {
            case "Player":
                sr_playersSushi.enabled = false;
                break;
            case "Friend1":
                sr_friend1sSushi.enabled = false;
                break;
            case "Friend2":
                sr_friend2sSushi.enabled = false;
                break;
            default:
                Debug.Log($"{someone}は無効な文字列です");
                break;
        }
    }
    // -----------------------------------
}
