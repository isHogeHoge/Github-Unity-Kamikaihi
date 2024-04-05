using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.UI;
using System;

public class FadeInAndOut : MonoBehaviour
{
    [SerializeField] float fadeTime = 1.2f; // フェードイン(アウト)にかかる時間
    private Image image;

    private void Start()
    {
        image = this.GetComponent<Image>();
    }

    // フェードイン
    internal async UniTask FadeIn(CancellationToken ct)
    {
        // fadePanelをアクティブに
        this.gameObject.SetActive(true);

        //const float fadeTime = 1.5f;　　　　// フェードインにかかる時間（秒)
        int loopCount = 10;　　　　　　　// ループ回数（フェードインまでに何回処理を行うか)
        float waitTime = fadeTime / loopCount; // ウェイト時間算出 (１ループでどれくらい時間を刻むのか)
        float alpha_interval = 255.0f / loopCount; // 色の間隔を算出(１ループで透明度をいくら上げるのか)

        // 色を徐々に変えるループ
        for (float alpha = 0.0f; alpha <= 255.0f; alpha += alpha_interval)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(waitTime),cancellationToken: ct); // 待ち時間

            // Alpha値を少しずつ上げる
            Color color = image.color;
            color.a = alpha / 255.0f;
            image.color = color;
        }
    }

    // フェードアウト
    internal async UniTask FadeOut(CancellationToken ct)
    {
        //const float fadeTime = 1.5f;　　　　// フェードアウトにかかる時間（秒)
        int loopCount = 10;　　　　　　　 // ループ回数（フェードアウトまでに何回処理を行うか)
        float waitTime = fadeTime / loopCount; // ウェイト時間算出 (１ループでどれくらい時間を刻むのか)
        float alpha_interval = 255.0f / loopCount; // 色の間隔を算出(１ループで透明度をいくら上げるのか)

        // 色を徐々に変えるループ
        for (float alpha = 255.0f; alpha >= 0.0f; alpha -= alpha_interval)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(waitTime), cancellationToken: ct); // 待ち時間

            // Alpha値を少しずつ下げる
            Color color = image.color;
            color.a = alpha / 255.0f;
            image.color = color;
        }

        // fadePanelを非アクティブに
        this.gameObject.SetActive(false);
    }

    // フェードイン&アウト
    internal async UniTask FadeInOut(CancellationToken ct)
    {
        await FadeIn(ct);
        await FadeOut(ct);        
    }

    // ex)FadeInOutメソッドを呼び出したい場合、呼び出し元にFadeInOut(this.GetCancellationTokenOnDestroy()).Forget()を記入
}
