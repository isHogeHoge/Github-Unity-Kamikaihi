using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager_24 : MonoBehaviour
{
    [SerializeField] GameObject stagePanel_UI; // スクロールさせるUI
    [SerializeField] GameObject stagePanel;    // スクロールさせるゲームオブジェクト

    private StageScrollCnt scrollCnt_UI;
    private StageScrollCnt scrollCnt;
    void Start()
    {
        scrollCnt = stagePanel.GetComponent<StageScrollCnt>();
        scrollCnt_UI = stagePanel_UI.GetComponent<StageScrollCnt>();
        // ステージ初期位置から右に1ページ分だけ移動できるように設定
        scrollCnt_UI.maxCountR = 1;
        scrollCnt.maxCountR = 1;
    }

    // 右側のページに移動
    internal void ScrollStagePnl_Right()
    {
        scrollCnt_UI.ScrollStagePnl("RIGHT");
        scrollCnt.ScrollStagePnl("RIGHT");
    }

}
