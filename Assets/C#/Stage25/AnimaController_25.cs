using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class AnimaController_25 : MonoBehaviour
{
    [SerializeField] GameObject cdPlayerBtn;
    [SerializeField] GameObject Btn_OpenGirlfriendsDoor;
    [SerializeField] GameObject Btn_OpenBoyfriendsDoor;
    [SerializeField] GameObject Btn_OpenGeeksDoor;
    [SerializeField] GameObject Btn_CloseGeeksDoor;
    [SerializeField] GameObject Btn_OpenCDPlayersDoor;
    [SerializeField] GameObject player;
    [SerializeField] GameObject cake;
    [SerializeField] GameObject musicalNotes;
    [SerializeField] GameObject geek;
    [SerializeField] GameObject speechBubble;  // Grilfriendの吹き出し
    [SerializeField] GameObject stageManager;
    [SerializeField] Sprite cake1Spr;   // プレーンケーキの画像
    private static int playCount_musicalNotes = 0; // 音符アニメーション再生回数
    private static bool isRuning = false;   // geekがCDを止めに行くアニメーション再生中フラグ

    // オブジェクトが破棄された時
    private void OnDestroy()
    {
        isRuning = false;
        playCount_musicalNotes = 0;
    }

    // 音楽(音符)停止処理
    internal void StopTheMusic()
    {
        playCount_musicalNotes = 0;
        musicalNotes.GetComponent<Animator>().enabled = false;
        musicalNotes.GetComponent<SpriteRenderer>().enabled = false;
    }
    // +++++ MusicalNotes +++++
    // 音符アニメーション終了後
    private void isPlayGeekPauseTheMusicAnima()
    {
        playCount_musicalNotes++;
        // 音符アニメーションが6回以上再生されていて、geekが走っていないなら
        if(playCount_musicalNotes >= 6 && !isRuning)
        {
            // geekがCDを止めに行くアニメーション再生
            geek.GetComponent<Animator>().enabled = true;
            geek.GetComponent<Animator>().Play("GeekGoOut");
            geek.GetComponent<SpriteRenderer>().enabled = true;
            playCount_musicalNotes = 0;
        }
    }
    // +++++++++++++++++++++++

    // +++++ Geek +++++
    // "GoOut"アニメーション開始時
    // 部屋の扉を開ける
    private void OpenTheGeeksDoor()
    {
        isRuning = true;
        Btn_OpenGeeksDoor.GetComponent<Button>().enabled = true;
        Btn_OpenGeeksDoor.GetComponent<Button>().onClick.Invoke();

    }
    // "GoOut"アニメーション終了時
    // 移動開始
    private void StartGeekMoving()
    {
        this.GetComponent<GeeksMovementCnt>().isGoing = true;
    }

    // "GoIn"アニメーション終了時
    // 部屋の扉を閉める
    private void CloseTheGeeksDoor()
    {
        isRuning = false;
        Btn_CloseGeeksDoor.GetComponent<Button>().enabled = true;
        Btn_CloseGeeksDoor.GetComponent<Button>().onClick.Invoke();
    }
    // 自身を非表示 & アニメーション停止
    private void InActiveGeeksImgAndAnima()
    {
        // "GoInFlag"がtrueのままだと、再びCDを止めに行く際にアニメーションが"GoOut"→"GoIn"と遷移してしまう
        this.GetComponent<Animator>().SetBool("GoInFlag", false);
        this.GetComponent<Animator>().enabled = false;
        this.GetComponent<SpriteRenderer>().enabled = false;
    }
    
    // ++++++++++++++++

    // ++++ GeekInTheTopRightRoom ++++
    // アニメーション開始時
    // 音楽の再生&停止を禁止
    private void InActiveCDPlayerBtn()
    {
        cdPlayerBtn.GetComponent<Image>().enabled = false;
    }
    // CDPlayer部屋の扉を開ける
    private void OpenTheCDPlayersDoor()
    {
        Btn_OpenCDPlayersDoor.GetComponent<Button>().onClick.Invoke();
    }
    // アニメーション終了時
    // 移動再開
    private void RestartGeekMoving()
    {
        geek.GetComponent<SpriteRenderer>().enabled = true;
        geek.GetComponent<GeeksMovementCnt>().GeekGoBack();
    }
    // 音楽の再生&停止を可能に
    private void ActiveCDPlayerBtn()
    {
        cdPlayerBtn.GetComponent<Image>().enabled = true;
    }
    // +++++++++++++++++++++++++++++++++

    // +++++ Girlfriend +++++
    // 料理完成アニメーション開始時、吹き出しを表示する
    private void ActiveSpeechBubble()
    {
        speechBubble.GetComponent<SpriteRenderer>().enabled = true;
    }
    // 料理完成アニメーション終了時、吹き出しを再度表示させないように
    private void InActiveSpeechBubble()
    {
        speechBubble.SetActive(false);
    }
    // 移動アニメーション開始時、自身の扉を開ける
    private void OpenTheGirlfriendsDoor()
    {
        Btn_OpenGirlfriendsDoor.GetComponent<Button>().onClick.Invoke();
    }
    // 移動アニメーション終了時、Boyfriendの扉を開ける
    private void OpenTheBoyfriendsDoor()
    {
        Btn_OpenBoyfriendsDoor.GetComponent<Button>().onClick.Invoke();
    }
    // 衝突アニメーション開始時、ケーキを手から落とす
    private void DropTheCake()
    {
        cake.GetComponent<Animator>().enabled = true;
    }
    // ++++++++++++++++++++++

    // +++++ Cake +++++
    // Cake落下アニメーション終了時
    private void PlayPlayerIsCoveredWithACakeAnima()
    {
        // Girlfriendが持っているケーキをPlayerが被るアニメーション再生
        // プレーンケーキ
        if(this.GetComponent<SpriteRenderer>().sprite == cake1Spr)
        {
            player.GetComponent<Animator>().Play("PlayerIsCoveredWithACake1");
        }
        // チョコレートケーキ
        else
        {
            player.GetComponent<Animator>().Play("PlayerIsCoveredWithACake2");
        }
    }
    // ++++++++++++++++

    // +++++ Enemy +++++
    // Playerを攫うアニメーション開始時
    private void InActivePlayer()
    {
        player.GetComponent<SpriteRenderer>().enabled = false;
    }
    // 衝突アニメーション開始時、衝突したキャラクターのHitアニメーション再生
    private void PlayerCharactersHitAnima()
    {
        if (stageManager.GetComponent<StageManager_25>().hitChar)
        {
            stageManager.GetComponent<StageManager_25>().hitChar.GetComponent<Animator>().SetBool("HitFlag", true);
        }
    }
    // +++++++++++++++++

}
