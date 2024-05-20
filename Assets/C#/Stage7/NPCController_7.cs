using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NPCController_7 : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr_RUmbrella;
    [SerializeField] SpriteRenderer sr_YUmbrella;
    [SerializeField] SpriteRenderer sr_GUmbrella;
    [SerializeField] GameObject stageManager;  
    [SerializeField] Vector2 endPos;       // NPC移動終了ポシション

    private Animator animator_npc;          

    void Start()
    {
        animator_npc = this.GetComponent<Animator>();
    }

    private void Update()
    {
        // ポーズ中ならUpdateを抜ける
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        // endPosまで移動
        float moveSpeed = 5f;
        this.transform.position = Vector3.MoveTowards(this.transform.position, endPos, moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // アニメーション切り替えコライダーと接触時
        if (col.tag != "Change")
        {
            return;
        }

        // playerが赤の傘を取得していたら
        if (!sr_RUmbrella.enabled)
        {
            // 緑&黄色の傘取得
            animator_npc.SetBool("G&YFlag", true);
            sr_GUmbrella.enabled = false;
            sr_YUmbrella.enabled = false;
        }
        // playerが黄色の傘を取得していたら
        else if (!sr_YUmbrella.enabled)
        {
            // 赤&緑の傘取得
            animator_npc.SetBool("R&GFlag", true);
            sr_RUmbrella.enabled = false;
            sr_GUmbrella.enabled = false;
        }
        // playerが緑の傘を取得していたら
        else if (!sr_GUmbrella.enabled)
        {
            // 赤&黄色の傘取得
            animator_npc.SetBool("R&YFlag", true);
            sr_RUmbrella.enabled = false;
            sr_YUmbrella.enabled = false;
        }
    }
}
