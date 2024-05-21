using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class StageManager_18 : MonoBehaviour
{
    [SerializeField] Button catBtn; // cat出現ボタン
    [SerializeField] GameObject trio; //player,friend1,friend2の親オブジェクト
    [SerializeField] GameObject monk;
    [SerializeField] SpriteRenderer sr_cat;
    [SerializeField] GameObject TriosSBCnt; // TriosSBCntクラスがアタッチされているオブジェクト
    [SerializeField] GameObject MonksSBCnt; // MonksSBCntクラスがアタッチされているオブジェクト

    private StageManager sm;
    private float passedTimes = 0;       // 吹き出し出現停止からの経過時間
    private bool isCountUp = false;       // 経過時間測定フラグ
    internal GameState gameState = GameState.playing;

    // オブジェクトDestroy時
    private void OnDestroy()
    {
        // 登録したInvokeをすべてキャンセル
        CancelInvoke();
    }
    private async void Start()
    {
        // オープニング動画の再生を待つ
        sm = this.GetComponent<StageManager>();
        await sm.WaitForOpeningVideo(this.GetCancellationTokenOnDestroy());

        // 15,25,40秒後に(trioの)吹き出しの出現を3秒間ストップ
        Invoke(nameof(StopAppearing_TriosSB), 15f);
        Invoke(nameof(StopAppearing_TriosSB), 25f);
        Invoke(nameof(StopAppearing_TriosSB), 40f);
        // 30秒後に猫出現可能に
        Invoke(nameof(CanAppearCat), 30f);
        // 60秒経過でゲームオーバー
        Invoke(nameof(TimeOver), 60f);

    }

    void Update()
    {
        // ゲーム中以外ならUpdateを抜ける
        if(Mathf.Approximately(Time.timeScale, 0f) || gameState != GameState.playing)
        {
            return;
        }

        if (isCountUp)
        {
            // 時間の計測
            passedTimes += Time.deltaTime;
            if (passedTimes >= 3f)
            {
                // 3秒経過したら、吹き出しの出現再開
                passedTimes = 0f;
                TriosSBCnt.SetActive(true);
                isCountUp = false;

            }
        }
    }
    
    // (Player,Friend1,Friend2の)吹き出し出現一時停止メソッド
    private void StopAppearing_TriosSB()
    {
        TriosSBCnt.SetActive(false);
        // 3秒後に吹き出しの出現開始
        isCountUp = true;
    }

    // 猫出現可能メソッド
    private void CanAppearCat()
    {
        if(gameState != GameState.playing)
        {
            return;
        }
        sr_cat.enabled = true;
        catBtn.enabled = true;
    }

    // タイムオーバーメソッド
    private void TimeOver()
    {
        if (gameState != GameState.playing)
        {
            return;
        }
        // ゲーム操作をできないようにする
        sm.CantGameControl();
        // 吹き出しの出現を停止
        InActiveSpeechBubble();

        // Player,Friend1,Friend2のタイムオーバーアニメーション再生
        for (var i = 0; i < trio.transform.childCount; i++)
        {
            Transform someone = trio.transform.GetChild(i);
            someone.GetComponent<Animator>().Play($"{someone.name}TimeOver1");
        }
    }

    // 吹き出し出現を停止(ゲームオーバーorクリア時)
    internal void InActiveSpeechBubble()
    {
        TriosSBCnt.SetActive(false);
        MonksSBCnt.SetActive(false);

        for (var i = 0; i < trio.transform.childCount; i++)
        {
            Transform someone = trio.transform.GetChild(i);
            // (player,friend1,friend2の)吹き出しを非表示
            for (var n = 0; n < someone.transform.childCount; n++)
            {
                someone.transform.GetChild(n).gameObject.SetActive(false);
            }
        }
    }

    // -------- Button --------
    // player,friend1,friend2の吹き出し
    public void ClickTriosSBBtn(GameObject someone)
    {
        // クリックされた吹き出しを初期状態に
        for (var i = 0; i < someone.transform.childCount; i++)
        {
            someone.transform.GetChild(i).GetComponent<Animator>().SetBool("isStart", true);
            someone.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    // 猫
    public void ClickCatBtn()
    {
        // ゲーム操作をできないようにする
        gameState = GameState.gameClear;
        sm.CantGameControl();
        // 吹き出しの出現を停止
        InActiveSpeechBubble();

        // 僧侶の移動ストップ(ゲームクリア)
        monk.GetComponent<MonkController>().isMoving = false;
        monk.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        monk.GetComponent<Animator>().Play("MonkStop");
    }
    // ---------------------------

}
