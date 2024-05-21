using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using System.Threading;
using System;
public class WatermelonAnimaCnt : MonoBehaviour
{
    [SerializeField] Image img_cutWatermelon;
    [SerializeField] GameObject clearImg;
    [SerializeField] GameObject fadePanel;
    [SerializeField] GameObject stageManager;

    private FadeInAndOut fadeCnt;
    private void Start()
    {
        fadeCnt = fadePanel.GetComponent<FadeInAndOut>();
    }

    // スイカが揺れるアニメーション終了時
    // 場面切り替え & ゲームクリア
    private async void GameClear()
    {
        // スイカが割れる
        this.GetComponent<Image>().enabled = false;
        img_cutWatermelon.enabled = true;

        // フェードイン
        await fadeCnt.FadeIn(this.GetCancellationTokenOnDestroy());

        // クリア時の画像を表示
        clearImg.SetActive(true);

        // フェードアウト
        await fadeCnt.FadeOut(this.GetCancellationTokenOnDestroy());

        // ゲームクリア処理
        await stageManager.GetComponent<StageManager>().GameClear(17, this.GetCancellationTokenOnDestroy());
    }

}
