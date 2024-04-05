using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrollCnt : MonoBehaviour
{
    [SerializeField] float scrollSpeed; // スクロールスピード

    private BackgroundScrollCnt bgsc;
    private Vector3 rightTop;　　// 画面右上座標
    private Vector3 leftBottom; // 画面左下座標

    private void Start()
    {
        bgsc = this.GetComponent<BackgroundScrollCnt>();

        rightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        leftBottom = Camera.main.ScreenToWorldPoint(Vector3.zero);
    }

    private void Update()
    {
        // ポーズ中またはこのスクリプトが非アクティブならUpdateを抜ける
        if (Mathf.Approximately(Time.timeScale, 0f) || !bgsc)
        {
            return;
        }

        // Y軸方向にスクロール
        transform.Translate(0, scrollSpeed * Time.deltaTime, 0);
        // 上スクロール(+y軸方向)
        if(scrollSpeed > 0f)
        {
            // 背景の下辺がカメラの上辺を超えたら、背景のをカメラの下辺に移動(無限スクロール)
            if (transform.position.y > rightTop.y * 2)
            {
                transform.position = new Vector3(transform.position.x, leftBottom.y * 2, 0);
            }
        }
        // 下スクロール(-y軸方向)
        else
        {
            // 背景の上辺がカメラの下辺を超えたら、背景をカメラの上辺に移動(無限スクロール)
            if (transform.position.y < leftBottom.y * 2)
            {
                transform.position = new Vector3(transform.position.x, rightTop.y * 2, 0);
            }
        }
        

    }
    

}
