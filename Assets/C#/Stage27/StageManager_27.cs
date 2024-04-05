using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageManager_27 : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject triosSushi;
    [SerializeField] GameObject stagePanel_UI; // スクロールさせるUI
    [SerializeField] GameObject stagePanel;    // スクロールさせるゲームオブジェクト
    [SerializeField] Sprite shrimpWithOutWasabi; // えび寿司(わさび抜き)画像

    private RotateFoodsCnt rfc;

    void Start()
    {
        // ステージ初期位置から右に1ページ分だけ移動できるように設定
        stagePanel_UI.GetComponent<StageScrollCnt>().maxCountR = 1;
        stagePanel.GetComponent<StageScrollCnt>().maxCountR = 1;

        rfc = this.GetComponent<RotateFoodsCnt>();

    }

    // 「食べる」ボタンをクリックした時
    public void ClickEatBtn()
    {
        // Playerが手に取る寿司を取得
        GameObject sushiInFrontOfPlayer = triosSushi.transform.GetChild(rfc.indexOfFoods[0]).gameObject;
        // Playerが手に取る寿司が、えび寿司(わさび抜き)ならクリア
        if(sushiInFrontOfPlayer.GetComponent<SushiController>().sushiSpr == shrimpWithOutWasabi)
        {
            player.GetComponent<Animator>().SetBool("ClearFlag", true);
        }
    }
}
