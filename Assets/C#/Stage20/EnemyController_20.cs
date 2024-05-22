using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
public class EnemyController_20 : MonoBehaviour
{
    [SerializeField] Animator animator_player;
    [SerializeField] SpriteRenderer sr_speechBubble;
    [SerializeField] Animator animator_enemies;
    [SerializeField] Animator animator_throwedEggs;
    [SerializeField] GameObject itemManager;
    [SerializeField] GameObject stageManager;
    [SerializeField] Sprite sb_Over1Spr;       // ×マークの吹き出し画像
    [SerializeField] Sprite sb_Over2Spr;       // ⚪︎×マークの吹き出し画像
    // 焼き上がったクッキーのアイテム画像
    [SerializeField] Sprite yellowCookie1Spr;  // 黄色クッキー(チョコペン使用×)画像
    [SerializeField] Sprite yellowCookie2Spr;  // 黄色クッキー(チョコペン使用⚪︎)画像
    [SerializeField] Sprite redCookie1Spr;     // 赤色クッキー(チョコペン使用×)画像
    [SerializeField] Sprite redCookie2Spr;     // 赤色クッキー(チョコペン使用⚪︎)画像

    private StageManager sm;
    private Animator animator_enemy;
    private void Start()
    {
        sm = stageManager.GetComponent<StageManager>();
        animator_enemy = this.GetComponent<Animator>();
    }
    // 接触判定(Item)
    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        Sprite itemSpr = col.GetComponent<Image>().sprite;
        // アイテム使用処理
        col.GetComponent<Image>().sprite = null;
        itemManager.GetComponent<ItemManager>().UsedItem();

        // ゲーム操作をできないようにする
        sm.CantGameControl();

        //「焼き上がったクッキー(黄色)&チョコペン使用⚪︎」アイテム使用
        if (itemSpr == yellowCookie2Spr)
        {
            // クッキーを食べる → ゲームクリアアニメーション再生
            animator_enemy.Play("EnemyEat");
            animator_enemy.SetBool("ClearFlag", true);
        }
        //「焼き上がったクッキー(赤色)&チョコペン使用⚪︎」アイテム使用
        else if (itemSpr == redCookie2Spr)
        {
            // クッキーを食べる → ゲームオーバーアニメーション再生 
            animator_enemy.Play("EnemyEat");
            animator_enemy.SetBool("OverFlag", true);
        }
        // 「焼き上がったクッキー(黄色or赤色)&チョコペン使用×」アイテム使用
        else if (itemSpr == yellowCookie1Spr || itemSpr == redCookie1Spr)
        {
            // ⚪︎×マークの吹き出しに変更
            sr_speechBubble.sprite = sb_Over2Spr;

            // ゲームオーバーアニメーションを再生
            animator_enemy.Play("EnemyGetAngry2");
        }
        // それ以外のアイテム画像
        else
        {
            // ×マークの吹き出しに変更
            sr_speechBubble.sprite = sb_Over1Spr;

            // ゲームオーバーアニメーションを再生
            animator_enemy.Play("EnemyGetAngry2");
        }
    }

    // -------- Animation ---------
    // 卵を投げるアニメーション開始時
    private void isActiveThrowedEggs()
    {
        // 初回のみ卵アニメーション再生
        if (sr_speechBubble.enabled)
        {
            animator_throwedEggs.Play("ThrowedEgg1");
        }
    }
    // クッキーを食べる & 卵を投げるアニメーション開始時
    private void InActiveSpeechBubble()
    {
        // 吹き出しを非表示
        sr_speechBubble.enabled = false;
    }

    // 帰るアニメーション終了時
    private void GameClear()
    {
        // ゲームクリア処理
        animator_player.Play("PlayerClear_20");
        sm.GameClear(20, this.GetCancellationTokenOnDestroy()).Forget();
    }

    // "EnemyGetAngry2"アニメーション開始時
    // "EnemyOver2"アニメーション再生中
    private void PlayPlayerOverAnima()
    {
        // Playerゲームオーバーアニメーション再生
        animator_player.Play("PlayerOver_20");
    }
    // "EnemyOver2"アニメーション終了時
    private void EnemiesAppear()
    {
        // Enemies出現
        animator_enemies.Play("EnemiesCome");

    }
    // ----------------------------

}
