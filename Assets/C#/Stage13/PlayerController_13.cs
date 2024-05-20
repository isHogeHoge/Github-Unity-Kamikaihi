using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerController_13 : MonoBehaviour
{
    [SerializeField] Animator animator_friend1;
    [SerializeField] Animator animator_friend2;
    [SerializeField] GameObject stageManager;

    private StageManager sm;
    private void Start()
    {
        sm = stageManager.GetComponent<StageManager>();
    }
    // ---------- Animation ----------
    // ゲームオーバーアニメーション終了時
    private void GameOver()
    {
        // friend1,2のアニメーション切り替え
        animator_friend1.Play("Friend1Clear");
        animator_friend2.Play("Friend2Clear");
        // ゲームオーバー処理
        sm.GameOver(this.GetCancellationTokenOnDestroy()).Forget();
    }

    // ゲームクリアアニメーション終了時
    private void GameClear()
    {
        // friend1,2のアニメーション切り替え
        animator_friend1.Play("Friend1Over");
        animator_friend2.Play("Friend2Over");
        // ゲームクリア処理
        sm.GameClear(13, this.GetCancellationTokenOnDestroy()).Forget();
    }
    // ---------------------------------
}
