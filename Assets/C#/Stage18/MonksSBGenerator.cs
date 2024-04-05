using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonksSBGenerator : MonoBehaviour
{
    [SerializeField] GameObject monk;
    private float passedTimes = 0f; // 経過時間(吹き出し出現で0にリセット)

    private Animator animator_Down; // 吹き出し(下)のAnimator
    private Animator animator_Up;   // 吹き出し(上)のAnimator
    private SpriteRenderer sr_Down; // 吹き出し(下)のSpriteRenderer
    private void Start()
    {
        animator_Down = monk.transform.GetChild(0).GetComponent<Animator>();
        animator_Up = monk.transform.GetChild(1).GetComponent<Animator>();
        sr_Down = monk.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
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
            animator_Down.SetBool("isStart", false);
            animator_Up.SetBool("isStart", false);

            // 僧侶の吹き出し(下)アニメーションを再生
            animator_Down.Play("Monk'sSB_Down",0,0);
            sr_Down.enabled = true;

            passedTimes = 0f;
        }
    }
}
