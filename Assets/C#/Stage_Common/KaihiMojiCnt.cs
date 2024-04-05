using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaihiMojiCnt : MonoBehaviour
{
    [SerializeField] GameObject retryBtn;     // 「リトライ」ボタン
    [SerializeField] GameObject otherStageBtn;  // 「他のステージ」ボタン

    // 「回避成功(失敗)」の文字表示後、「リトライ」&「他のステージ」ボタン表示
    private void EndCharAnima()
    {
        retryBtn.SetActive(true);
        otherStageBtn.SetActive(true);
    }
}
