using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Playerがアラームを鳴らした時orレーザーを踏んだ時アクティブ
public class PoliceController : MonoBehaviour
{
    [SerializeField] GameObject playerR;
    [SerializeField] GameObject stageManager;

    private StageManager_30 sm_30;
    private Animator animator_police;
    private Animator animator_player;
    private GameObject player; // アクティブなPlayer
    private Vector3 targetPos;
    private bool isMoving = true;

    void Start()
    {
        sm_30 = stageManager.GetComponent<StageManager_30>();
        animator_police = this.GetComponent<Animator>();

        // 自身が追いかけるPlayerを設定
        player = sm_30.GetActivePlayer();
        animator_player = player.GetComponent<Animator>();
        // 移動先をPlayerの左隣に設定
        targetPos = new Vector3(player.transform.position.x - 0.7f, this.transform.position.y, 0f);
    }

    void Update()
    {
        // ポーズ中ならUpdateを抜ける
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        if (isMoving)
        {
            // Playerの横まで移動
            float speed = 1.5f;
            this.transform.position = Vector3.MoveTowards(this.transform.position, targetPos, speed * Time.deltaTime);
            // 移動終了時、Playerを逮捕するアニメーション再生(ゲームオーバー)
            if (this.transform.position.x >= targetPos.x)
            {
                // Playerが画面右側にいるならスクロール
                if (player == playerR)
                {
                    sm_30.ScrollStagePnl("RIGHT");
                }
                animator_player.SetBool("isArrested", true);
                animator_police.Play("PoliceStop");
                isMoving = false;
            }
        }
        

    }
}
