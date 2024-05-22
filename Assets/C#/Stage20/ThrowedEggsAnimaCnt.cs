using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ThrowedEggsAnimaCnt : MonoBehaviour
{
    [SerializeField] Animator animator_enemy;
    [SerializeField] GameObject crackedEggs;
    [SerializeField] GameObject stageManager;

    // アニメーション終了時、その地点に割れた卵を表示
    private void ActiveCrackedEgg(int num)
    {
        GameObject crackedEgg = crackedEggs.transform.Find($"CrackedEgg{num}").gameObject;
        crackedEgg.GetComponent<SpriteRenderer>().enabled = true;
    }

    // 5個目の卵が投げられた後、ゲームオーバ処理
    private void GameOver()
    {
        // Enemyのアニメーション切り替え
        animator_enemy.SetBool("StopFlag", true);
        // ゲームオーバー処理
        stageManager.GetComponent<StageManager>().GameOver(this.GetCancellationTokenOnDestroy()).Forget();
    }

}
