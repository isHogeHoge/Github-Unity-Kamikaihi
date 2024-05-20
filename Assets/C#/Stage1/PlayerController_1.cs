using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public class PlayerController_1 : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr_player;
    [SerializeField] BoxCollider2D boxCol_player;
    [SerializeField] GameObject itemManager;   
    [SerializeField] GameObject stageManager;
    [SerializeField] Sprite umbrellaSpr;
    [SerializeField] Sprite openAnUmbrellaSpr;  // Playerが傘を刺している画像 
    [SerializeField] Sprite playerOverSpr;

    private ItemManager im;          
    private StageManager sm;    
    private bool isOpeningAUmbrella = false;  // 傘使用中フラグ

    private void Start()
    {
        im = itemManager.GetComponent<ItemManager>();
        sm = stageManager.GetComponent<StageManager>();        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (isOpeningAUmbrella)
        {
            return;
        }

        // 雨粒と接触したらゲームオーバー
        if(col.gameObject.tag == "Dead")
        {
            sr_player.sprite = playerOverSpr;
            sm.GameOver(this.GetCancellationTokenOnDestroy()).Forget();
            boxCol_player.enabled = false;
        }

    }
    // 接触判定(Item)
    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        Image img_item = col.GetComponent<Image>();
        // 傘アイテム使用
        if (img_item.sprite == umbrellaSpr)
        {
            // アイテム使用処理
            img_item.sprite = null;
            im.UsedItem();

            // 傘をさしている状態に変更
            isOpeningAUmbrella = true;
            sr_player.sprite = openAnUmbrellaSpr;

            // PlayerのタグをGroundにし、接触時Rainオブジェクトが消えるようにする
            this.tag = "Ground";   

        }
    }
}
