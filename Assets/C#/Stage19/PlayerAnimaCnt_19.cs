using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System;
public class PlayerAnimaCnt_19 : MonoBehaviour
{
    [SerializeField] GameObject pot;     // 鍋
    [SerializeField] GameObject dish;    // 料理
    [SerializeField] GameObject brother;
    [SerializeField] GameObject peekingBrother;
    [SerializeField] GameObject player1; // player移動前
    [SerializeField] GameObject player3; // player移動後
    [SerializeField] GameObject mother;
    [SerializeField] GameObject motherAndPlayer;
    [SerializeField] GameObject clearImg;  // クリア後に表示される画像
    [SerializeField] GameObject chopsticksBtn;
    [SerializeField] GameObject fadePanel;     // フェードイン&アウトパネル
    [SerializeField] GameObject stageManager;
    [SerializeField] Sprite dish2;           // 完成した天ぷら料理の画像

    private Player1Controller_19 player1Cnt;
    private void Start()
    {
        player1Cnt = player1.GetComponent<Player1Controller_19>();
    }

    // +++++ Player1 +++++
    // Brotherにスマホを渡すアニメーション再生中
    private void BrotherGetASmartphone()
    {
        // Brotherがスマホを受け取る
        brother.GetComponent<Animator>().Play("BrotherTurn");
    }
    // +++++++++++++++++++

    // +++++ Player2 +++++
    // 料理の方に歩くアニメーション終了時
    // Player2からPlayer3に切り替える
    private void PlayerChangeFrom2To3()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
        player3.GetComponent<SpriteRenderer>().enabled = true;

        // Playerがエプロンを着用していたら
        if (player1Cnt.isWearingApron)
        {
            // 天ぷらを食べるアニメーション再生(ゲームクリア)
            player3.GetComponent<Animator>().Play("PlayerEat");
        }
        // Playerが電話をかけていたら
        else if (player1Cnt.called)
        {
            // Playerに油が跳ねるアニメーション再生(ゲームオーバー)
            pot.GetComponent<Animator>().Play("PotOverflow");
            player3.GetComponent<Animator>().Play("PlayerOver");
        }
        else
        {
            // 料理に手を伸ばすアニメーション再生(ゲームオーバー)
            player3.GetComponent<Animator>().Play("PlayerReachFor");
        }
    }
    // +++++++++++++++++++

    // +++++ Player3 +++++
    // 天ぷら料理に手を伸ばすアニメーション開始時
    private void PlayMotherTurnAnima()
    {
        // MotherがPlayerに気づく & 吹き出しを表示
        mother.GetComponent<Animator>().Play("MotherTurn");
    }
    // 天ぷら料理に手を伸ばすアニメーション終了時、ゲームオーバーアニメーション再生
    private void PlayMotherAndPlayerAnima()
    {
        // Mother&Playerの切り替え
        mother.SetActive(false);
        this.gameObject.SetActive(false);

        motherAndPlayer.GetComponent<Animator>().enabled = true;
        motherAndPlayer.GetComponent<SpriteRenderer>().enabled = true;
    }

    // 料理を食べているアニメーション中
    // 箸アイテムを取得不可に
    private void InActiveChopsticksBtn()
    {
        chopsticksBtn.GetComponent<Image>().enabled = false;
    }

    // 箸取得後
    // 場面切り替え & Playerが料理するアニメーション再生
    private async void StartCooking()
    {
        // フェードイン
        await fadePanel.GetComponent<FadeInAndOut>().FadeIn(this.GetCancellationTokenOnDestroy());

        // Brotherを切り替える
        brother.GetComponent<SpriteRenderer>().enabled = false;
        peekingBrother.GetComponent<SpriteRenderer>().enabled = true;
        // 天ぷらの画像を変更
        dish.GetComponent<SpriteRenderer>().sprite = dish2;
        // playerが料理するアニメーション再生
        this.GetComponent<Animator>().Play("PlayerCook1");

        // フェードアウト
        await fadePanel.GetComponent<FadeInAndOut>().FadeOut(this.GetCancellationTokenOnDestroy());
    }

    // 天ぷらを皿に置くアニメーション開始時
    private void PlayMotherIsSurprisedAnima()
    {
        mother.GetComponent<Animator>().Play("MotherIsSurprised");
    }
    // 天ぷらを皿に置くアニメーション終了時
    // 場面切り替え → ステージクリア処理
    private async void FadeInAndOut_Clear()
    {
        // フェードイン
        await fadePanel.GetComponent<FadeInAndOut>().FadeIn(this.GetCancellationTokenOnDestroy());
        // クリア後の画像を表示
        clearImg.GetComponent<SpriteRenderer>().enabled = true;
        // フェードアウト
        await fadePanel.GetComponent<FadeInAndOut>().FadeOut(this.GetCancellationTokenOnDestroy());

        // ゲームクリア処理
        await UniTask.Delay(TimeSpan.FromSeconds(2f), cancellationToken: this.GetCancellationTokenOnDestroy());
        await stageManager.GetComponent<StageManager>().GameClear(19, this.GetCancellationTokenOnDestroy());
    }
    // +++++++++++++++++++

}
