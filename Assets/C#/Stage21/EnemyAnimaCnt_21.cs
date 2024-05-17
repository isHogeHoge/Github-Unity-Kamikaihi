using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class EnemyAnimaCnt_21 : MonoBehaviour
{
    [SerializeField] GameObject smallEnemybtn; // SmallEnemyを風呂に落とすボタン
    [SerializeField] GameObject goBtn;     
    [SerializeField] GameObject goBackBtn; 
    [SerializeField] GameObject treasureBtn;
    [SerializeField] GameObject player;
    [SerializeField] GameObject casket;
    [SerializeField] GameObject signBoard; // お湯風呂トラップの看板
    [SerializeField] GameObject rock;
    [SerializeField] GameObject clickCancelPnl;
    [SerializeField] GameObject stageManager;
    [SerializeField] Sprite openedCasket2Spr; // 棺桶(空)の画像

    private bool wasPlayed = false;  // "SmallEnemyFellDown"アニメーションが再生済みか

    // +++++++ SmallEnemy +++++++
    // 小さいアヌビス出現時
    private void ChangeCasketSpr()
    {
        // 棺桶を空の画像に
        casket.GetComponent<SpriteRenderer>().sprite = openedCasket2Spr;
    }

    // 橋になるアニメーション開始時
    private void CanDorpSmallEnemy()
    {
        // トラップがお湯風呂だったら、SmallEnemyを落とせるように
        if (signBoard.GetComponent<SpriteRenderer>().enabled)
        {
            smallEnemybtn.GetComponent<Button>().enabled = true;
        }
    }
    private void CanClickGoBtn()
    {
        // 初回の再生のみ以下の処理を行う
        if (wasPlayed)
        {
            return;
        }
        // Playerのアニメーションを初期状態 & 進行可能に
        player.GetComponent<Animator>().Play("PlayerStop1");
        goBtn.GetComponent<Image>().enabled = true;
    }
    // 橋になるアニメーション終了時
    private void GetwasPlayedFlgTure()
    {
        // このアニメーションを再生済みに
        if (!wasPlayed)
        {
            wasPlayed = true;
        }
    }

    // "SmallEnemyCollapse"アニメーション終了時
    private void GameOver()
    {
        // playerを襲うアニメーション再生
        this.GetComponent<Animator>().Play("SmallEnemyAttack");
        player.GetComponent<Animator>().Play("PlayerIsAttacked");

        // ゲームオーバー処理
        stageManager.GetComponent<StageManager>().GameOver(this.GetCancellationTokenOnDestroy()).Forget();
    }
    // ++++++++++++++++++++++++++++++++

    // ++++++++ BigEnemy ++++++++
    // Playerを叩くアニメーション終了時
    private void isGameOver()
    {
        // Playerが宝を取得していなかったら、ゲームオーバー
        if (treasureBtn.GetComponent<Button>().enabled)
        {
            stageManager.GetComponent<StageManager>().GameOver(this.GetCancellationTokenOnDestroy()).Forget();
        }
    }

    // 岩から逃げるアニメーション開始時
    private void CanGameControl()
    {
        // ゲーム操作を可能に
        clickCancelPnl.SetActive(false);
        goBackBtn.GetComponent<Image>().enabled = true;
    }
    // 岩から逃げるアニメーション終了時
    private void RestartMovementOfRock()
    {
        // 岩の移動再開
        rock.GetComponent<RockController>().isMoving = true;
    }
    private void InActiveBigEnemy()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
    }
    // ++++++++++++++++++++++++++++++++

}
