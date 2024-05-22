using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
public class RockController : MonoBehaviour
{
    [SerializeField] Image img_crackBtn3;
    [SerializeField] GameObject player_Over; // 衝突時のPlayer
    [SerializeField] SpriteRenderer sr_fallingTreasure;
    [SerializeField] GameObject stageManager;

    private RockController rockCnt;
    private Vector3 targetPos;      
    private float passedTimes = 0f; // 経過時間
    private float speed = 3f;      // 移動スピード
    internal bool isMoving = true;

    void Start()
    {
        rockCnt = this.GetComponent<RockController>();
        // ゴールの設定
        targetPos = new Vector3(-5f, this.transform.position.y, this.transform.position.z);
    }

    void Update()
    {
        // ポーズ中またはこのスクリプトが非アクティブならUpdateを抜ける
        if (Mathf.Approximately(Time.timeScale, 0f) || !rockCnt.enabled)
        {
            return;
        }

        passedTimes += Time.deltaTime;
        // 3秒以上経過で移動開始
        if(passedTimes >= 3f && isMoving)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, targetPos, speed * Time.deltaTime);
        }
    }

    // 接触判定
    private void OnTriggerEnter2D(Collider2D col)
    {
        // Playerと接触時
        if (col.tag == "Player")
        {
            // ゲーム操作をできないようにする
            stageManager.GetComponent<StageManager_21>().CantGameControl();

            // 岩の移動をストップ
            speed = 0f;

            // 岩と接触した地点の座標(X)を代入
            float playerPosX = col.transform.position.x;
            // Playerが岩に轢かれている状態に
            player_Over.transform.position = new Vector3(playerPosX, player_Over.transform.position.y, player_Over.transform.position.z);
            player_Over.GetComponent<SpriteRenderer>().enabled = true;
            col.gameObject.SetActive(false);
            
            sr_fallingTreasure.enabled = false;

            // ゲームオーバー処理
            stageManager.GetComponent<StageManager>().GameOver(this.GetCancellationTokenOnDestroy()).Forget();
        }
        // 画面内に侵入した時
        else if (col.tag == "Enter")
        {
            // BigEnemyを出現できないようにする
            img_crackBtn3.enabled = false;
        }
        
    }

    
}
