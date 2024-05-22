using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class EnemyAnimaCnt_21 : MonoBehaviour
{
    [SerializeField] Button smallEnemybtn; // SmallEnemyを風呂に落とすボタン
    [SerializeField] Button treasureBtn;
    [SerializeField] Image img_goBtn;     
    [SerializeField] Image img_goBackBtn; 
    [SerializeField] Animator animator_player;
    [SerializeField] SpriteRenderer sr_casket;
    [SerializeField] SpriteRenderer sr_signBoard; // お湯風呂トラップの看板
    [SerializeField] GameObject rock;
    [SerializeField] GameObject clickCancelPnl;
    [SerializeField] GameObject stageManager;
    [SerializeField] Sprite openedCasket2Spr; // 棺桶(空)の画像

    private StageManager sm;
    private bool wasPlayed = false;  // "SmallEnemyFellDown"アニメーションが再生済みか

    private void Start()
    {
        sm = stageManager.GetComponent<StageManager>();
    }

    // +++++++ SmallEnemy +++++++
    // 小さいアヌビス出現時
    private void ChangeCasketSpr()
    {
        // 棺桶を空の画像に
        sr_casket.sprite = openedCasket2Spr;
    }

    // 橋になるアニメーション開始時
    private void Enable_DorpSmallEnemy()
    {
        // トラップがお湯風呂なら、SmallEnemyを落とせるように
        if (sr_signBoard.enabled)
        {
            smallEnemybtn.enabled = true;
        }
    }
    private void Enable_ClickGoBtn()
    {
        // 初回の再生のみ以下の処理を行う
        if (wasPlayed)
        {
            return;
        }
        // Playerのアニメーションを初期状態 & 進行可能に
        animator_player.Play("PlayerStop1");
        img_goBtn.enabled = true;
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
        animator_player.Play("PlayerIsAttacked");

        // ゲームオーバー処理
        sm.GameOver(this.GetCancellationTokenOnDestroy()).Forget();
    }
    // ++++++++++++++++++++++++++++++++

    // ++++++++ BigEnemy ++++++++
    // Playerを叩くアニメーション終了時
    private void isGameOver()
    {
        // Playerが宝を取得していなかったら、ゲームオーバー
        if (treasureBtn.enabled)
        {
            sm.GameOver(this.GetCancellationTokenOnDestroy()).Forget();
        }
    }

    // 岩から逃げるアニメーション開始時
    private void CanGameControl()
    {
        // ゲーム操作を可能に
        clickCancelPnl.SetActive(false);
        img_goBackBtn.enabled = true;
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
