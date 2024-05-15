using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class AspectKeeper : MonoBehaviour
{
    [SerializeField] Camera targetCamera; // 対象とするカメラ
    [SerializeField] Vector2 aspectVec = new Vector2(1080, 2340); // 目的解像度

    private void Update()
    {
        // 目的アスペクト比にするための倍率を求める
        float screenAspect = Screen.width / (float)Screen.height; // 画面のアスペクト比
        float targetAspect = aspectVec.x / aspectVec.y; // 目的のアスペクト比
        float magRate = targetAspect / screenAspect;

        // カメラのViewportを調整する
        Rect viewportRect = new Rect(0, 0, 1, 1);
        // 横幅を調整
        if(magRate < 1)
        {
            viewportRect.width = magRate;
            viewportRect.x = 0.5f - viewportRect.width * 0.5f; // 中央寄せ
        }
        // (横幅が1を超えたら)縦幅を調整
        else
        {
            viewportRect.height = 1 / magRate;
            viewportRect.y = 0.5f - viewportRect.height * 0.5f; // 中央寄せ
        }
        targetCamera.rect = viewportRect;

    }
}
