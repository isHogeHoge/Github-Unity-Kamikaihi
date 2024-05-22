using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager_25 : MonoBehaviour
{
    [SerializeField] Animator animator_player;
    [SerializeField] Animator animator_enemy;
    [SerializeField] GameObject grandmaL;
    [SerializeField] GameObject geekL; 
    [SerializeField] GameObject geekR; 
    [SerializeField] GameObject girlfriendL;
    [SerializeField] GameObject girlfriendR;
    [SerializeField] GameObject boyfriendL;
    [SerializeField] GameObject musicalNotes;
    [SerializeField] GameObject stagePanel_UI; // スクロールさせるUI
    [SerializeField] GameObject stagePanel;    // スクロールさせるゲームオブジェクト

    private GeeksMovementCnt gmc;
    private CommonAnimation_25 ca_25;
    private GirlfriendRCnt_25 girlfriendCnt;
    private SpriteRenderer sr_musicalNotes;
    private Animator animator_musicalNotes;
    private Animator animator_girlfriendR;
    // Enemy&Playerと衝突するキャラクター
    // Enemyの衝突アニメーション開始時にhitCharの衝突アニメーションを再生する
    internal GameObject hitChar = null; 
    void Start()
    {
        gmc = geekR.GetComponent<GeeksMovementCnt>();
        ca_25 = musicalNotes.GetComponent<CommonAnimation_25>();
        girlfriendCnt = girlfriendR.GetComponent<GirlfriendRCnt_25>();
        animator_girlfriendR = girlfriendR.GetComponent<Animator>();
        sr_musicalNotes = musicalNotes.GetComponent<SpriteRenderer>();
        animator_musicalNotes = musicalNotes.GetComponent<Animator>();

        // ステージ初期位置から右に1ページ分だけ移動できるように設定
        stagePanel_UI.GetComponent<StageScrollCnt>().maxCountR = 1;
        stagePanel.GetComponent<StageScrollCnt>().maxCountR = 1;
    }

    // --------- Button -----------
    // Girlfriend部屋の扉
    public void ClickGirlfriendsDoorBtn(string state)
    {
        // GirlfriendRへ使用できるアイテムがないなら、(扉の開閉に関わらず)コライダーを変化させない
        if (girlfriendCnt.usedChocolateItem)
        {
            return;
        }

        // 扉の開閉時にアイテム使用可・不可を切り替える
        switch (state)
        {
            // 扉が開いているなら、GirlfriendRへアイテム使用可(コライダーON)
            case "Open":
                animator_girlfriendR.Play("GirlfriendStop_ColliderOn");
                break;
            // 扉が閉まっているなら、GirlfriendRへアイテム使用不可(コライダーOFF)
            case "Close":
                animator_girlfriendR.Play("GirlfriendStop_ColliderOff");
                break;
            default:
                Debug.Log($"{state}は無効な文字列です");
                break;
        }
    }
    
    // CDPlayer
    public void ClickCDPlayerBtn()
    {
        // 音楽再生中なら、音楽を停止 & Geekを部屋に引き返させる
        if (sr_musicalNotes.enabled)
        {
            ca_25.StopTheMusic();
            // Geekをアイテムとして取得していないなら、部屋に引き返すアニメーション再生
            if (geekR.activeSelf)
            {
                gmc.GeekGoBack();
            }

        }
        // 音楽停止中なら、音楽を鳴らす
        else
        {
            animator_musicalNotes.enabled = true;
            sr_musicalNotes.enabled = true;
        }
    }
    // 「スタート」ボタン
    public void ClickStartBtn()
    {
        // Playerの前にいるキャラクター(Grandma以外)と衝突する
        // Girlfriendと衝突
        if (girlfriendL.GetComponent<SpriteRenderer>().enabled)
        {
            // Girlfriendと衝突(ゲームオーバー)
            animator_player.Play("PlayerMove2");
            hitChar = girlfriendL;
        }
        // Boyfriendと衝突
        else if (boyfriendL.GetComponent<SpriteRenderer>().enabled)
        {
            // Boyfriendと衝突後、ゲームクリアアニメーション再生
            animator_enemy.SetBool("FallInLoveFlag", true);
            animator_player.Play("PlayerMove2");
            animator_player.SetBool("ClearFlag", true);
            hitChar = boyfriendL;
        }
        // Geekと衝突
        else if (geekL.GetComponent<SpriteRenderer>().enabled)
        {
            // Geekと衝突後、ゲームオーバーアニメーション再生
            animator_enemy.SetBool("FallInLoveFlag", true);
            animator_enemy.SetBool("SnatchFlag", true);
            animator_player.Play("PlayerMove2");
            hitChar = geekL;
        }
        // Enemyと衝突
        else
        {
            // Grandmaが出現していたら、テレポート
            if (grandmaL.GetComponent<SpriteRenderer>().enabled)
            {
                grandmaL.GetComponent<Animator>().Play("GrandmaTeleport");
            }
            // Enemyと衝突後、ゲームオーバーアニメーション再生
            animator_enemy.SetBool("FallInLoveFlag", true);
            animator_enemy.SetBool("SnatchFlag", true);
            animator_player.Play("PlayerMove1");
        }

        // Enemy移動アニメーション開始
        animator_enemy.GetComponent<Animator>().Play("EnemyMove");
    }
    // ------------------------------

    


}
