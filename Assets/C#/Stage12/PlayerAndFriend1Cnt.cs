using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Numerics;
using UnityEngine;

public class PlayerAndFriend1Cnt : MonoBehaviour
{
    [SerializeField] GameObject friend1;
    [SerializeField] GameObject player;
    [SerializeField] GameObject stageManager;

    // アニメーション終了時、ゲームオーバー
    private void GameOver()
    {
        // Player&Friend1の切り替え
        this.GetComponent<SpriteRenderer>().enabled = false;
        friend1.GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponent<SpriteRenderer>().enabled = true;

        // ゲームオーバーアニメーション再生
        player.GetComponent<Animator>().Play("PlayerOver_12");
        friend1.GetComponent<Animator>().Play("Friend1TeasePlayer");

        // ゲームオーバー処理
        stageManager.GetComponent<StageManager>().GameOver(this.GetCancellationTokenOnDestroy()).Forget();
    }
}
