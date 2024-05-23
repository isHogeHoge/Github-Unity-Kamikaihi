using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class PlayerRController_30 : MonoBehaviour
{
    [SerializeField] Button standBtn;
    [SerializeField] GameObject backBtn;
    [SerializeField] GameObject rButton;
    [SerializeField] GameObject lButton;
    [SerializeField] GameObject fadePanel;
    [SerializeField] BoxCollider2D boxCol_cardScanner;
    [SerializeField] BoxCollider2D boxCol_treasure;
    [SerializeField] SpriteRenderer sr_clearImg;
    [SerializeField] GameObject stageManager;

    private FadeInAndOut fadeCnt;
    private Animator animator_playerR;
    private void Start()
    {
        fadeCnt = fadePanel.GetComponent<FadeInAndOut>();
        animator_playerR = this.GetComponent<Animator>();
    }
    // ----------- Animation ------------
    // レーザー(緑)を飛び越えた後
    // ゲーム操作可能に
    private void CanGameControl()
    {
        rButton.SetActive(true);
        lButton.SetActive(true);
        boxCol_cardScanner.enabled = true;
        boxCol_treasure.enabled = true;
    }

    // 宝をゲットした後、引き返すor台を動かし脱出可能に
    private void CanGoBackOrEscape()
    {
        backBtn.SetActive(true);
        standBtn.enabled = true;
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
        await fadeCnt.FadeIn(this.GetCancellationTokenOnDestroy());
        sr_clearImg.enabled = true;
        await fadeCnt.FadeOut(this.GetCancellationTokenOnDestroy());
        // ---------------------------
        animator_playerR.Play("PlayerGetOutOfTheHole");

    }

    // 逮捕アニメーション開始時
    private void isActiveTreasureUnderPlayersFeet()
    {
        // 直前のアニメーションの座標を反映
        animator_playerR.applyRootMotion = true;
        // 宝をゲットした後なら、足元に宝を表示
        if (this.transform.GetChild(0).gameObject.activeSelf)
        {
            this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    // --------------------------------------
}
