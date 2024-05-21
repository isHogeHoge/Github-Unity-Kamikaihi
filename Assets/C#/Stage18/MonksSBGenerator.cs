using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonksSBGenerator : MonoBehaviour
{
    [SerializeField] GameObject monk;
    [SerializeField] SpriteRenderer sr_sb_down;
    [SerializeField] Animator animator_sb_down;
    [SerializeField] Animator animator_sb_up;
    private float passedTimes = 0f; // 経過時間(吹き出し出現で0にリセット)

    void Update()
    {
        // ポーズ中ならUpdateを抜ける
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        passedTimes += Time.deltaTime;
        // 10秒経過で吹き出し出現
        if(passedTimes >= 10f)
        {
            // "isStart"フラグを初期状態にリセットし、タップされるまで吹き出しが表示されるようにする
            animator_sb_down.SetBool("isStart", false);
            animator_sb_up.SetBool("isStart", false);

            // 僧侶の吹き出し(下)アニメーションを再生
            animator_sb_down.Play("Monk'sSB_Down",0,0);
            sr_sb_down.enabled = true;

            passedTimes = 0f;
        }
    }
}
