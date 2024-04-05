using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager_25 : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject player;
    [SerializeField] GameObject grandma;
    [SerializeField] GameObject geekL; // 左ページのGeek
    [SerializeField] GameObject geekR; // 右ページのGeek
    [SerializeField] GameObject girlfriendL; // 左ページのGirlfriend
    [SerializeField] GameObject girlfriendR; // 右ページのGirlfriend
    [SerializeField] GameObject boyfriend;
    [SerializeField] GameObject musicalNotes;
    [SerializeField] GameObject stagePanel_UI; // スクロールさせるUI
    [SerializeField] GameObject stagePanel;    // スクロールさせるゲームオブジェクト

    // Enemy&Playerと衝突するキャラクター
    // Enemyの衝突アニメーション開始時にhitCharの衝突アニメーションを再生する
    internal GameObject hitChar = null; 
    void Start()
    {
        // ステージ初期位置から右に1ページ分だけ移動できるように設定
        stagePanel_UI.GetComponent<StageScrollCnt>().maxCountR = 1;
        stagePanel.GetComponent<StageScrollCnt>().maxCountR = 1;
    }

    // --------- Button -----------
    // Girlfriend部屋の扉
    public void ClickGirlfriendsDoorBtn(string state) // Open,Close
    {
        // GirlfriendRへ使用できるアイテムがないなら、(扉の開閉に関わらず)コライダーを常にONにする
        if (girlfriendR.GetComponent<GirlfriendRCnt_25>().usedChocolateItem)
        {
            return;
        }

        // 扉の開閉時にアイテム使用可・不可を切り替える
        switch (state)
        {
            // 扉が開いているなら、GirlfriendRへアイテム使用可(コライダーON)
            case "Open":
                girlfriendR.GetComponent<Animator>().Play("GirlfriendStop_ColliderOn");
                break;
            // 扉が閉まっているなら、GirlfriendRへアイテム使用不可(コライダーOFF)
            case "Close":
                girlfriendR.GetComponent<Animator>().Play("GirlfriendStop_ColliderOff");
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
        if (musicalNotes.GetComponent<SpriteRenderer>().enabled)
        {
            musicalNotes.GetComponent<AnimaController_25>().StopTheMusic();
            // Geekをアイテムとして取得していないなら、部屋に引き返すアニメーション再生
            if (geekR.activeSelf)
            {
                geekR.GetComponent<GeeksMovementCnt>().GeekGoBack();
            }

        }
        // 音楽停止中なら、音楽を鳴らす
        else
        {
            musicalNotes.GetComponent<Animator>().enabled = true;
            musicalNotes.GetComponent<SpriteRenderer>().enabled = true;
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
            player.GetComponent<Animator>().Play("PlayerMove2");
            hitChar = girlfriendL;
        }
        // Boyfriendと衝突
        else if (boyfriend.GetComponent<SpriteRenderer>().enabled)
        {
            // Boyfriendと衝突後、ゲームクリアアニメーション再生
            enemy.GetComponent<Animator>().SetBool("FallInLoveFlag", true);
            player.GetComponent<Animator>().Play("PlayerMove2");
            player.GetComponent<Animator>().SetBool("ClearFlag", true);
            hitChar = boyfriend;
        }
        // Geekと衝突
        else if (geekL.GetComponent<SpriteRenderer>().enabled)
        {
            // Geekと衝突後、ゲームオーバーアニメーション再生
            enemy.GetComponent<Animator>().SetBool("FallInLoveFlag", true);
            enemy.GetComponent<Animator>().SetBool("SnatchFlag", true);
            player.GetComponent<Animator>().Play("PlayerMove2");
            hitChar = geekL;
        }
        // Enemyと衝突
        else
        {
            // Grandmaが出現していたら、テレポート
            if (grandma.GetComponent<SpriteRenderer>().enabled)
            {
                grandma.GetComponent<Animator>().Play("GrandmaTeleport");
            }
            // Enemyと衝突後、ゲームオーバーアニメーション再生
            enemy.GetComponent<Animator>().SetBool("FallInLoveFlag", true);
            enemy.GetComponent<Animator>().SetBool("SnatchFlag", true);
            player.GetComponent<Animator>().Play("PlayerMove1");
        }

        // Enemy移動アニメーション開始
        enemy.GetComponent<Animator>().Play("EnemyMove");
    }
    // ------------------------------

    


}
