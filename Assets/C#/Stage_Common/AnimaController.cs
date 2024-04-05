using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class AnimaController : MonoBehaviour
{
    [SerializeField] GameObject stageManager;

    // ゲームオーバー処理
    private void GameOver()
    {
        stageManager.GetComponent<StageManager>().GameOver(this.GetCancellationTokenOnDestroy()).Forget();
    }
    // ゲームクリア処理
    private void GameClear(int stageNum)
    {
        stageManager.GetComponent<StageManager>().GameClear(stageNum, this.GetCancellationTokenOnDestroy()).Forget();
    }
}
