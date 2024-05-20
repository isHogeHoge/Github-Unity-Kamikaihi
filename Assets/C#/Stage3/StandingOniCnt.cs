using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
public class StandingOniCnt : MonoBehaviour
{
    [SerializeField] GameObject stageManager;   
    [SerializeField] SpriteRenderer sr_player;        
    [SerializeField] Sprite playerClearSpr;

    private StageManager sm;

    private void Start()
    {
        sm = stageManager.GetComponent<StageManager>(); 
    }

    // ゲームクリア処理
    // "OniWalk"アニメーション再生後に実行
    private void GameClear()
    {
        sr_player.sprite = playerClearSpr;
        sm.GameClear(3, this.GetCancellationTokenOnDestroy()).Forget();
    }

    // ゲームオーバー処理
    // "OniStandup_Over"アニメーション再生後に実行
    private void GameOver()
    {
        sm.GameOver(this.GetCancellationTokenOnDestroy()).Forget();
    }
}
