using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System;

public class StageManager_6 : MonoBehaviour
{
    [SerializeField] GameObject centaurs;
    [SerializeField] GameObject femaleCentaur;
    [SerializeField] GameObject enemy;
    [SerializeField] Animator animator_player;
    [SerializeField] GameObject fadePanel;

    private FadeInAndOut fadeCnt;

    private void Start()
    {
        fadeCnt = fadePanel.GetComponent<FadeInAndOut>();
    }

    // Enemyに花冠&イチゴアイテムを使用後、揺れている草むらをクリックしたらゲームクリア
    public async void AppearFemaleCentaur()
    {
        // 女ケンタウロス出現
        femaleCentaur.GetComponent<Animator>().enabled = true;
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f), cancellationToken: this.GetCancellationTokenOnDestroy());
        enemy.GetComponent<Animator>().Play("EnemyTurn");
        await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: this.GetCancellationTokenOnDestroy());

        // フェードイン
        await fadeCnt.FadeIn(this.GetCancellationTokenOnDestroy());

        // FemaleCentaur & Enemyの切り替え
        femaleCentaur.GetComponent<Image>().enabled = false;
        enemy.GetComponent<Image>().enabled = false;
        centaurs.GetComponent<Image>().enabled = true;

        // フェードアウト
        await fadeCnt.FadeOut(this.GetCancellationTokenOnDestroy());

        // FemaleCentaur & Enemy退場(ゲームクリア)
        animator_player.Play("PlayerSeeOff");
        centaurs.GetComponent<Animator>().enabled = true;
        this.GetComponent<StageManager>().GameClear(6, this.GetCancellationTokenOnDestroy()).Forget();
    }
    
}
