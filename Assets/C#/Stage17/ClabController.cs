using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClabController : MonoBehaviour
{
    [SerializeField] Vector2 targetPos; // 目的地点
    private Vector2 startPos;   // 移動開始地点
    private RectTransform rect;

    void Start()
    {
        rect = this.GetComponent<RectTransform>();
        // 移動開始地点の設定
        startPos = this.rect.anchoredPosition;
    }

    void Update()
    {
        // ポーズ中ならUpdateを抜ける
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        const float speed = 250f;
        this.rect.anchoredPosition = Vector3.MoveTowards(this.rect.anchoredPosition, targetPos, speed * Time.deltaTime);        
        // 目的地点に到着した時
        if(this.rect.anchoredPosition == targetPos)
        {
            // 初期位置までワープ
            this.rect.anchoredPosition = startPos;
        }
    }
}
