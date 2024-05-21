using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClabController : MonoBehaviour
{
    [SerializeField] Vector2 targetPos; // 目的地点
    private Vector2 startPos;
    private RectTransform rect_clab;

    void Start()
    {
        rect_clab = this.GetComponent<RectTransform>();
        // 移動開始地点の設定
        startPos = rect_clab.anchoredPosition;
    }

    void Update()
    {
        // ポーズ中ならUpdateを抜ける
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        const float speed = 250f;
        rect_clab.anchoredPosition = Vector3.MoveTowards(rect_clab.anchoredPosition, targetPos, speed * Time.deltaTime);        
        // 目的地点に到着した時
        if(rect_clab.anchoredPosition == targetPos)
        {
            // 初期位置までワープ
            rect_clab.anchoredPosition = startPos;
        }
    }
}
