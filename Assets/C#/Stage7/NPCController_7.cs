using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NPCController_7 : MonoBehaviour
{
    [SerializeField] GameObject gUmbrella;
    [SerializeField] GameObject rUmbrella;
    [SerializeField] GameObject yUmbrella;
    [SerializeField] GameObject stageManager;  
    [SerializeField] Vector2 endPos;       // NPC移動終了ポシション

    private Animator animator;          

    void Start()
    {
        animator = this.GetComponent<Animator>();
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

        // playerが緑の傘を取得していたら
        if (!gUmbrella.GetComponent<SpriteRenderer>().enabled)
        {
            // 赤&黄色の傘取得
            animator.SetBool("R&YFlag", true);
            rUmbrella.GetComponent<SpriteRenderer>().enabled = false;
            yUmbrella.GetComponent<SpriteRenderer>().enabled = false;
        }
        // playerが赤の傘を取得していたら
        else if (!rUmbrella.GetComponent<SpriteRenderer>().enabled)
        {
            // 緑&黄色の傘取得
            animator.SetBool("G&YFlag", true);
            gUmbrella.GetComponent<SpriteRenderer>().enabled = false;
            yUmbrella.GetComponent<SpriteRenderer>().enabled = false;
        }
        // playerが黄色の傘を取得していたら
        else if (!yUmbrella.GetComponent<SpriteRenderer>().enabled)
        {
            // 赤&緑の傘取得
            animator.SetBool("R&GFlag", true);
            rUmbrella.GetComponent<SpriteRenderer>().enabled = false;
            gUmbrella.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
