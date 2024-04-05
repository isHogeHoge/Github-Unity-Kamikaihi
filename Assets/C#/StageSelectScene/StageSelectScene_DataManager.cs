using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectScene_DataManager : MonoBehaviour
{
    private StageDataManager sdm;              
    private ClearDataManager cdm;                      

    private void Start()
    {
        sdm = this.GetComponent<StageDataManager>();
        // releasedCountに応じてステージを解放する
        sdm.ReleaseStage();

        // ステージ(クリア済み)遷移ボタンの画像を変更
        cdm = this.GetComponent<ClearDataManager>();
        cdm.ChangeBtnImg();
    }

    private void Update()
    {
        // ポーズ中ならUpdateを抜ける
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        // ステージの解放状況に応じて、ステージ遷移ボタンをアクティブor非アクティブにする
        sdm.isActiveStageTransitionBtn();
    }

}
