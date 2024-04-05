using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayerController_2 : MonoBehaviour
{
    [SerializeField] GameObject hanako;
    [SerializeField] GameObject toiletLid; // トイレの蓋
    [SerializeField] GameObject playerOnTheFountain;
    [SerializeField] GameObject toiletSeat; // トイレの便座
    [SerializeField] GameObject clearImg; // フェードアウト後の背景画像
    [SerializeField] GameObject player_clear;
    [SerializeField] GameObject fadePanel;
    [SerializeField] GameObject itemInventory;
    [SerializeField] GameObject stageManager;    
    [SerializeField] GameObject itemManager;
    [SerializeField] Sprite recoderSpr;
    [SerializeField] Sprite testpaperSpr;    

    private StageManager sm;   
    private ItemManager im;

    private void Start()
    {
        sm = stageManager.GetComponent<StageManager>();
        im = itemManager.GetComponent<ItemManager>();

    }

    // 接触判定(Item)
    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }
        // ゲーム操作できないようにする
        sm.CantGameControl();
        
        // アイテム使用処理
        Sprite itemSpr = col.GetComponent<Image>().sprite;
        col.GetComponent<Image>().sprite = null; 
        im.UsedItem();                           
        

        // リコーダーアイテム使用
        if (itemSpr == recoderSpr)
        {
            this.GetComponent<Animator>().Play("PlayerPlayARecoder");

        }
        // テスト用紙アイテム使用
        else if (itemSpr == testpaperSpr)
        {
            this.GetComponent<Animator>().Play("PlayerUseTestpaper");
        }
        
    }

    // ------------ animation -------------
    // リコーダーを吹いた後、Hanako出現(ゲームオーバー)
    private void HanakoAppear()
    {
        hanako.GetComponent<Image>().enabled = true;
        hanako.GetComponent<Animator>().enabled = true;
    }
    // テスト用紙を使用後、トイレから出る(ゲームクリア)
    private async void GoOutOfTheBathroom()
    {
        // フェードイン
        await fadePanel.GetComponent<FadeInAndOut>().FadeIn(this.GetCancellationTokenOnDestroy());

        // クリア時の背景を表示
        clearImg.GetComponent<Image>().enabled = true;
        // Playerの切り替え
        this.GetComponent<SpriteRenderer>().enabled = false;
        player_clear.GetComponent<Image>().enabled = true;
        
        // フェードアウト
        await fadePanel.GetComponent<FadeInAndOut>().FadeOut(this.GetCancellationTokenOnDestroy());

        // Playerがトイレから出るアニメーション再生
        player_clear.GetComponent<Animator>().enabled = true;
    }
    // Over2アニメーション終了後、トイレの水が溢れる(ゲームオーバー)
    private void WatarOverflow()
    {
        toiletLid.GetComponent<Image>().enabled = true;
        toiletSeat.GetComponent<Image>().enabled = true;
        // Playerの切り替え
        this.GetComponent<SpriteRenderer>().enabled = false;
        playerOnTheFountain.GetComponent<Image>().enabled = true;
        // Playが噴水に押し上げられるアニメーション再生
        playerOnTheFountain.GetComponent<Animator>().enabled = true;
    }
    // --------------------------------------


}
