using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class AnimaController_27 : MonoBehaviour
{
    [SerializeField] GameObject lButton;
    [SerializeField] GameObject playersSushi;
    [SerializeField] GameObject friend1sSushi;
    [SerializeField] GameObject friend2sSushi;
    [SerializeField] GameObject brother;
    [SerializeField] GameObject brothersSushi;
    [SerializeField] GameObject stageManager;
    [SerializeField] GameObject stagePanel_UI; // スクロールさせるUI
    [SerializeField] GameObject stagePanel;    // スクロールさせるゲームオブジェクト
    [SerializeField] Sprite shrimpWithWasabi; // えび寿司(わさびあり)

    // ---------- Player ----------
    // ゲームオーバーアニメーション終了時
    private void PlayBrotherClearAnima()
    {
        brother.GetComponent<Animator>().Play("BrotherClear");
    }
    // ゲームクリアニメーション終了時
    private void isPlayBrotherOverAnima()
    {
        // Brotherの寿司がえび寿司(わさびあり)かそれ以外かでアニメーションを切り替える
        // アニメーション再生終了時にゲームクリア処理を行う
        if (brothersSushi.GetComponent<SushiController>().sushiSpr == shrimpWithWasabi)
        {
            brother.GetComponent<Animator>().Play("BrotherOver");
        }
        else
        {
            brother.GetComponent<Animator>().Play("BrotherStart_Clear");
        }
    }
    // ゲームオーバー(orクリア)アニメーション終了後
    // 右側のページにスクロールする
    private void ScrollStagePnl_Right()
    {
        stagePanel_UI.GetComponent<StageScrollCnt>().ScrollStagePnl("RIGHT");
        stagePanel.GetComponent<StageScrollCnt>().ScrollStagePnl("RIGHT");
        lButton.GetComponent<Image>().enabled = false;
    }
    // --------------------------------

    // ------- Player,Friend1(2) --------
    // Eatアニメーション開始時、手に取った寿司を非表示
    private void InActivePickedUpSushi(string someone)
    {
        switch (someone)
        {
            case "Player":
                playersSushi.GetComponent<SpriteRenderer>().enabled = false;
                break;
            case "Friend1":
                friend1sSushi.GetComponent<SpriteRenderer>().enabled = false;
                break;
            case "Friend2":
                friend2sSushi.GetComponent<SpriteRenderer>().enabled = false;
                break;
            default:
                Debug.Log($"{someone}は無効な文字列です");
                break;
        }
    }
    // -----------------------------------
}
