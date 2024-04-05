using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager_24 : MonoBehaviour
{
    [SerializeField] GameObject stagePanel_UI; // スクロールさせるUI
    [SerializeField] GameObject stagePanel;    // スクロールさせるゲームオブジェクト

    void Start()
    {
        // ステージ初期位置から右に1ページ分だけ移動できるように設定
        stagePanel_UI.GetComponent<StageScrollCnt>().maxCountR = 1;
        stagePanel.GetComponent<StageScrollCnt>().maxCountR = 1;
    }

    // 右側のページに移動
    internal void ScrollStagePnl_Right()
    {
        stagePanel_UI.GetComponent<StageScrollCnt>().ScrollStagePnl("RIGHT");
        stagePanel.GetComponent<StageScrollCnt>().ScrollStagePnl("RIGHT");
    }

}
