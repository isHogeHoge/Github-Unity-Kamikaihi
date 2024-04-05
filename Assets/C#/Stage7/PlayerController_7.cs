using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController_7 : MonoBehaviour
{
    [SerializeField] GameObject rUmbrellaOnTheGround;
    [SerializeField] GameObject yUmbrellaOnTheGround;
    [SerializeField] GameObject itemManager;   
    [SerializeField] GameObject stageManager;
    [SerializeField] Sprite gUmbrellaSpr;
    [SerializeField] Sprite rUmbrellaSpr;
    [SerializeField] Sprite yUmbrellaSpr;
    [SerializeField] Sprite openGUmbrellaSpr; // Playerが傘をさしている画像(緑)
    [SerializeField] Sprite openRUmbrellaSpr; // Playerが傘をさしている画像(赤)    
    [SerializeField] Sprite openYUmbrellaSpr; // Playerが傘をさしている画像(黄色)   
    [SerializeField] Sprite playerOverSpr;      

    private ItemManager im;          
    private StageManager sm;
    private bool openGUmbrella = false;

    private void Start()
    {
        im = itemManager.GetComponent<ItemManager>();        
        sm = stageManager.GetComponent<StageManager>();    

    }

    // 雨粒と接触時(ゲームオーバー)
    private void OnTriggerEnter2D(Collider2D col)
    {
        // 緑色の傘をさしていたら、メソッドを抜ける
        if (openGUmbrella)
        {
            return;
        }

        // さしていた傘を地面に表示
        if (col.tag == "Dead")
        {
            if (this.GetComponent<SpriteRenderer>().sprite == openRUmbrellaSpr)
            {
                rUmbrellaOnTheGround.GetComponent<Image>().enabled = true;
            }
            else if (this.GetComponent<SpriteRenderer>().sprite == openYUmbrellaSpr)
            {
                yUmbrellaOnTheGround.GetComponent<Image>().enabled = true;
            }

            // ゲームオーバー処理
            this.GetComponent<SpriteRenderer>().sprite = playerOverSpr;
            sm.GameOver(this.GetCancellationTokenOnDestroy()).Forget();
            this.GetComponent<BoxCollider2D>().enabled = false;
        }

    }

    // 接触判定(Item)
    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        // アイテム使用処理
        Sprite itemSpr = col.GetComponent<Image>().sprite; 
        col.GetComponent<Image>().sprite = null;
        im.UsedItem();

        // 傘アイテム使用
        // 緑
        if (itemSpr == gUmbrellaSpr)
        {
            // 緑色の傘を刺している状態に変更
            openGUmbrella = true;
            this.GetComponent<SpriteRenderer>().sprite = openGUmbrellaSpr;

            // タグをGroundにし、接触時Rainオブジェクトが消えるようにする
            this.tag = "Ground";

        }
        // 赤
        else if (itemSpr == rUmbrellaSpr)
        {
            // 赤色の傘を刺している状態に変更
            this.GetComponent<SpriteRenderer>().sprite = openRUmbrellaSpr;
        }
        // 黄色
        else if (itemSpr == yUmbrellaSpr)
        {
            // 緑色の傘を刺している状態に変更
            this.GetComponent<SpriteRenderer>().sprite = openYUmbrellaSpr;
        } 
    }

}
