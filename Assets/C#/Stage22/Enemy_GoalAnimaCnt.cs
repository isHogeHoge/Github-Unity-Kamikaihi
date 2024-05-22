using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Enemy_GoalAnimaCnt : MonoBehaviour
{
    [SerializeField] Animator animator_player;
    [SerializeField] GameObject stageManager;

    // 吹き飛ぶアニメーション終了時
    private void GameClear()
    {
        // ゲームクリア処理
        animator_player.Play("PlayerClear");
        stageManager.GetComponent<StageManager>().GameClear(22, this.GetCancellationTokenOnDestroy()).Forget();
    }
}
