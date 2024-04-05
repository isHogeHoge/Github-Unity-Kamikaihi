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
    [SerializeField] GameObject player;
    [SerializeField] GameObject fadePanel;

    // Enemyに花冠&イチゴアイテムを使用後、揺れている草むらをクリックしたらゲームクリア
    public async void AppearFemaleCentaur()
    {
        // 女ケンタウロス出現
        femaleCentaur.GetComponent<Animator>().enabled = true;
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f), cancellationToken: this.GetCancellationTokenOnDestroy());
        enemy.GetComponent<Animator>().Play("EnemyTurn");
        await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: this.GetCancellationTokenOnDestroy());

        // フェードイン
        await fadePanel.GetComponent<FadeInAndOut>().FadeIn(this.GetCancellationTokenOnDestroy());

        // FemaleCentaur & Enemyの切り替え
        femaleCentaur.GetComponent<Image>().enabled = false;
        enemy.GetComponent<Image>().enabled = false;
        centaurs.GetComponent<Image>().enabled = true;

        // フェードアウト
        await fadePanel.GetComponent<FadeInAndOut>().FadeOut(this.GetCancellationTokenOnDestroy());

        // FemaleCentaur & Enemy退場(ゲームクリア)
        player.GetComponent<Animator>().Play("PlayerSeeOff");
        centaurs.GetComponent<Animator>().enabled = true;
        this.GetComponent<StageManager>().GameClear(6, this.GetCancellationTokenOnDestroy()).Forget();
    }
    
}
