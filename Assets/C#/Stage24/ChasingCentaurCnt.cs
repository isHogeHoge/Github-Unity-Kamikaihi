using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChasingCentaurCnt : MonoBehaviour
{
    [SerializeField] GameObject itemManager;
    [SerializeField] GameObject stageManager;
    [SerializeField] Vector3 targetPos;

    private PlayersMovementCnt_24 pmc_24;
    internal bool isChasing = false; // Brotherを追いかけるフラグ

    private void Start()
    {
        pmc_24 = stageManager.GetComponent<PlayersMovementCnt_24>();
    }
    private void Update()
    {
        // ポーズ中ならUpdateを抜ける
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        // 移動処理
        if (isChasing)
        {
            float speed = 2f;
            this.transform.position = Vector3.MoveTowards(this.transform.position, targetPos, speed * Time.deltaTime);

            // ゴールまで到着したらPlayerの移動開始
            if (this.transform.position.x == targetPos.x)
            {
                pmc_24.PlayerMove();
                isChasing = false;
            }
        }
        
    }
}
