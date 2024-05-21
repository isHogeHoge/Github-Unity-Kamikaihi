using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.EventSystems;
public class StageManager_16 : MonoBehaviour
{
    [SerializeField] Image img_speechBubble;
    [SerializeField] GameObject brother1;
    [SerializeField] Image img_brother1Btn;
    [SerializeField] Image img_OpenTheWindowBtn;
    [SerializeField] GameObject centaur1;

    private CentaursController centaursCnt;
    private StageScrollCnt scrollCnt;
    private void Start()
    {
        centaursCnt = centaur1.GetComponent<CentaursController>();

        // ステージ初期位置から右・左に1ページ分だけ移動できるように設定
        scrollCnt = this.GetComponent<StageScrollCnt>();
        scrollCnt.maxCountL = -1;
        scrollCnt.maxCountR = 1;
    }

    // ---------- Button -----------
    // Brotherが入っているSinkのドア
    public void ClickBtn_SinksDoor3(GameObject door3Btn)
    {
        if (brother1.activeSelf)
        {
            // Brotherと吹き出しの表示・非表示を切り替える
            img_speechBubble.enabled = !img_speechBubble.enabled;
            Image img_brother1 = brother1.GetComponent<Image>();
            img_brother1.enabled = !img_brother1.enabled;
        }
        // Centaursが去り、Brother出現可能なら
        // Brother出現ボタンをクリックできるように、自身のクリック判定を消す
        if (img_brother1Btn.enabled)
        {
            door3Btn.GetComponent<Image>().enabled = false;
        }

    }

    // カーテンを開けるボタン
    public void ClickBtn_OpenTheCurtain()
    {
        // Centaursが退出可能 & 窓が空いていたら
        if(centaursCnt.canGetOut && !img_OpenTheWindowBtn.enabled)
        {
            // Centaurs退場処理
            centaursCnt.CentaursGetOut(this.GetCancellationTokenOnDestroy()).Forget();
        }
    }

    // 窓を開けるボタン
    public void ClickBtn_OpenTheWindow()
    {
        // Centaursが退出可能なら
        if (centaursCnt.canGetOut)
        {
            // Centaurs退場処理
            centaursCnt.CentaursGetOut(this.GetCancellationTokenOnDestroy()).Forget();
        }
    }
    // -----------------------------------
}
