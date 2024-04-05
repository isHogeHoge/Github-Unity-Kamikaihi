using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.EventSystems;
public class StageManager_16 : MonoBehaviour
{
    [SerializeField] GameObject speechBubble;
    [SerializeField] GameObject brother1;
    [SerializeField] GameObject brother1Btn;
    [SerializeField] GameObject btn_OpenTheWindow;
    [SerializeField] GameObject centaur1;

    private CentaursController centaursCnt;
    private void Start()
    {
        // ステージ初期位置から右・左に1ページ分だけ移動できるように設定
        this.GetComponent<StageScrollCnt>().maxCountL = -1;
        this.GetComponent<StageScrollCnt>().maxCountR = 1;

        // スクリプトの取得
        centaursCnt = centaur1.GetComponent<CentaursController>();
    }

    // ---------- Button -----------
    // Brotherが入っているSinkのドア
    public void ClickBtn_SinksDoor3(GameObject door3Btn)
    {
        if (brother1.activeSelf)
        {
            // Brotherと吹き出しの表示・非表示を切り替える
            speechBubble.GetComponent<Image>().enabled = !speechBubble.GetComponent<Image>().enabled;
            brother1.GetComponent<Image>().enabled = !brother1.GetComponent<Image>().enabled;
        }
        // Centaursが去り、Brother出現可能なら
        // Brother出現ボタンをクリックできるように、自身のクリック判定を消す
        if (brother1Btn.GetComponent<Image>().enabled)
        {
            door3Btn.GetComponent<Image>().enabled = false;
        }

    }

    // カーテンを開けるボタン
    public void ClickBtn_OpenTheCurtain()
    {
        // Centaursが退出可能 & 窓が空いていたら
        if(centaursCnt.canGetOut && !btn_OpenTheWindow.GetComponent<Image>().enabled)
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
