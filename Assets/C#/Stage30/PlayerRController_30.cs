using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class PlayerRController_30 : MonoBehaviour
{
    [SerializeField] GameObject standBtn;
    [SerializeField] GameObject backBtn;
    [SerializeField] GameObject rButton;
    [SerializeField] GameObject lButton;
    [SerializeField] GameObject fadePanel;
    [SerializeField] GameObject cardScanner;
    [SerializeField] GameObject treasure;
    [SerializeField] GameObject clearImg;
    [SerializeField] GameObject stageManager;

    // ----------- Animation ------------
    // レーザー(緑)を飛び越えた後
    // ゲーム操作可能に
    private void CanGameControl()
    {
        // 画面スクロール&カードスキャンを可能に
        rButton.SetActive(true);
        lButton.SetActive(true);
        cardScanner.GetComponent<BoxCollider2D>().enabled = true;
        // 宝を取得可能に
        treasure.GetComponent<BoxCollider2D>().enabled = true;
    }

    // 宝をゲットした後、引き返すor台を動かし脱出可能に
    private void CanGoBackOrEscape()
    {
        backBtn.SetActive(true);
        standBtn.GetComponent<Button>().enabled = true;
    }

    // 引き返すアニメーション再生中、画面を左側にスクロールする
    private void ScrollStagePnl_Left()
    {
        stageManager.GetComponent<StageManager_30>().ScrollStagePnl("LEFT");
    }

    // 脱出口に入るアニメーション終了時、場面切り替え→脱出口からでるアニメーション再生
    private async void PlayGetOutOfTheHoleAnima()
    {
        // ------ 場面切り替え処理 -----
        await fadePanel.GetComponent<FadeInAndOut>().FadeIn(this.GetCancellationTokenOnDestroy());
        clearImg.GetComponent<SpriteRenderer>().enabled = true;
        await fadePanel.GetComponent<FadeInAndOut>().FadeOut(this.GetCancellationTokenOnDestroy());
        // ---------------------------
        this.GetComponent<Animator>().Play("PlayerGetOutOfTheHole");

    }

    // 逮捕アニメーション開始時
    private void isActiveTreasureUnderPlayersFeet()
    {
        // 直前のアニメーションの座標を反映
        this.GetComponent<Animator>().applyRootMotion = true;
        // 宝をゲットした後なら、足元に宝を表示
        if (this.transform.GetChild(0).gameObject.activeSelf)
        {
            this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    // --------------------------------------
}
