using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainController : MonoBehaviour
{
    [SerializeField] GameObject rainCloud;            
    [SerializeField] GameObject rainPrefab;           

    private const float RainRangeX = 0.5f;           // 雨の生成範囲
    private float rainCloud_width;                  // rainCloudの幅
    private Vector3 pos;                            // 雨の発射位置

    void Start()
    {
        // rainCloudの幅を代入
        rainCloud_width = rainCloud.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    
    private void FixedUpdate()
    {
        // ポーズ中ならUpdateを抜ける
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }        

        // 雨粒Prefabの座標Xをランダムに設定
        float posX = Random.Range(transform.position.x - RainRangeX, transform.position.x + RainRangeX);
        // RainCloudのX座標を代入
        float rainCloudX = rainCloud.transform.position.x;

        // 生成座標ががRainCloudの右端座標Xを超えたら、Prefabを生成しない
        if (posX > rainCloudX + (rainCloud_width / 2.0f))
        {
            return;
        }

        // 雨粒Prefabの生成
        pos = new Vector3(posX, transform.position.y, transform.position.z);
        Instantiate(rainPrefab, pos, Quaternion.identity);

    }

}
