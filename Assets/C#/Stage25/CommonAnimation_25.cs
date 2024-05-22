using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class CommonAnimation_25 : MonoBehaviour
{
    [SerializeField] Image img_cdPlayerBtn;
    [SerializeField] Button Btn_OpenGirlfriendsDoor;
    [SerializeField] Button Btn_OpenBoyfriendsDoor;
    [SerializeField] Button Btn_OpenGeeksDoor;
    [SerializeField] Button Btn_CloseGeeksDoor;
    [SerializeField] Button Btn_OpenCDPlayersDoor;
    [SerializeField] GameObject player;
    [SerializeField] GameObject cake;
    [SerializeField] GameObject musicalNotes;
    [SerializeField] GameObject geek;
    [SerializeField] GameObject speechBubble;  // Grilfriendの吹き出し
    [SerializeField] GameObject stageManager;
    [SerializeField] Sprite cake1Spr;   // プレーンケーキの画像

    private GeeksMovementCnt gmc;
    private SpriteRenderer sr_geek;
    private Animator animator_geek;
    private static int playCount_musicalNotes = 0; // 音符アニメーション再生回数
    private static bool isRuning = false;   // geekがCDを止めに行くアニメーション再生中フラグ
    private void Start()
    {
        gmc = geek.GetComponent<GeeksMovementCnt>();
        sr_geek = geek.GetComponent<SpriteRenderer>();
        animator_geek = geek.GetComponent<Animator>();
    }

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
            animator_geek.enabled = true;
            animator_geek.Play("GeekGoOut");
            sr_geek.enabled = true;
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
        Btn_OpenGeeksDoor.enabled = true;
        Btn_OpenGeeksDoor.onClick.Invoke();

    }
    // "GoOut"アニメーション終了時
    // 移動開始
    private void StartGeekMoving()
    {
        gmc.isGoing = true;
    }

    // "GoIn"アニメーション終了時
    // 部屋の扉を閉める
    private void CloseTheGeeksDoor()
    {
        isRuning = false;
        Btn_CloseGeeksDoor.enabled = true;
        Btn_CloseGeeksDoor.onClick.Invoke();
    }
    // 自身を非表示 & アニメーション停止
    private void InActiveGeeksImgAndAnima()
    {
        animator_geek.SetBool("GoInFlag", false);
        animator_geek.enabled = false;
        sr_geek.enabled = false;
    }
    
    // ++++++++++++++++

    // ++++ GeekInTheTopRightRoom ++++
    // アニメーション開始時
    // 音楽の再生&停止を禁止
    private void InActiveCDPlayerBtn()
    {
        img_cdPlayerBtn.enabled = false;
    }
    // CDPlayer部屋の扉を開ける
    private void OpenTheCDPlayersDoor()
    {
        Btn_OpenCDPlayersDoor.onClick.Invoke();
    }
    // アニメーション終了時
    // 移動再開
    private void RestartGeekMoving()
    {
        sr_geek.enabled = true;
        gmc.GeekGoBack();
    }
    // 音楽の再生&停止を可能に
    private void ActiveCDPlayerBtn()
    {
        img_cdPlayerBtn.enabled = true;
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
        Btn_OpenGirlfriendsDoor.onClick.Invoke();
    }
    // 移動アニメーション終了時、Boyfriendの扉を開ける
    private void OpenTheBoyfriendsDoor()
    {
        Btn_OpenBoyfriendsDoor.onClick.Invoke();
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
        // プレーンケーキ
        if(cake.GetComponent<SpriteRenderer>().sprite == cake1Spr)
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
