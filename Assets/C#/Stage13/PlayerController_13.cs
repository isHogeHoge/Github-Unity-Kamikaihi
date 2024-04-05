using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerController_13 : MonoBehaviour
{
    [SerializeField] GameObject friend1;
    [SerializeField] GameObject friend2;
    [SerializeField] GameObject stageManager;

    // ---------- Animation ----------
    // ゲームオーバーアニメーション終了時
    private void GameOver()
    {
        // friend1,2のアニメーション切り替え
        friend1.GetComponent<Animator>().Play("Friend1Clear");
        friend2.GetComponent<Animator>().Play("Friend2Clear");
        // ゲームオーバー処理
        stageManager.GetComponent<StageManager>().GameOver(this.GetCancellationTokenOnDestroy()).Forget();
    }

    // ゲームクリアアニメーション終了時
    private void GameClear()
    {
        // friend1,2のアニメーション切り替え
        friend1.GetComponent<Animator>().Play("Friend1Over");
        friend2.GetComponent<Animator>().Play("Friend2Over");
        // ゲームクリア処理
        stageManager.GetComponent<StageManager>().GameClear(13, this.GetCancellationTokenOnDestroy()).Forget();
    }
    // ---------------------------------
}
