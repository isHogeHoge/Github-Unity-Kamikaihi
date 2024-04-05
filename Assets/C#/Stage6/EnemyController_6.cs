using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class EnemyController_6 : MonoBehaviour
{
    [SerializeField] GameObject GreenGrassL;
    [SerializeField] GameObject GreenGrassR;
    [SerializeField] GameObject player;          
    [SerializeField] GameObject stageManager;    
    [SerializeField] GameObject itemManager;     
    [SerializeField] GameObject clickCancelPnl;  
    [SerializeField] GameObject FemaleCentaurBtn;   // 女ケンタウロス出現ボタン
    [SerializeField] Sprite garlandSpr;
    [SerializeField] Sprite strawberrySpr;

    private StageManager sm;                 
    private ItemManager im;                  
    private Animator animator;               // 自身のアニメーター
    private RectTransform rect;              // 自身のRectTransform
    private Vector2 startPos;                // スタート座標
    private const float moveSpeed = 500f;     // 自身の移動スピード
    private const float deltaPos = 40f;      // 自身の座標変化分
    private const float deltaSizeX = 4f;     // 自身のWidth変化分
    private const float deltaSizeY = 6f;     // 自身のHeight変化分
    private int moveCount = 0;                // 移動した回数
    private bool garlandExists = false;       // (Enemyの)アニメーションで花輪がのるフラグ
    private bool strawberryExists = false;    // (Enemyの)アニメーションでイチゴが乗るフラグ
    private bool snatched = false;          // Playerを連れ去るフラグ

    void Start()
    {
        sm = stageManager.GetComponent<StageManager>();
        im = itemManager.GetComponent<ItemManager>();
        animator = this.GetComponent<Animator>();
        rect = this.GetComponent<RectTransform>();

        startPos = rect.anchoredPosition;
    }

    private void FixedUpdate()
    {
        // ゲームオーバー時の移動処理
        if (snatched)
        {
            // スタート地点まで移動
            this.rect.anchoredPosition = Vector3.MoveTowards(this.rect.anchoredPosition, startPos, moveSpeed * Time.deltaTime);
            // サイズ(width,height)を徐々に小さくしていく
            this.rect.sizeDelta = new Vector2(this.rect.sizeDelta.x - deltaSizeX, this.rect.sizeDelta.y - deltaSizeY);
            // スタート地点を超えたら、移動を終える →　ゲームオーバー処理
            if(this.rect.anchoredPosition.y >= startPos.y || this.rect.anchoredPosition.x >= startPos.x)
            {
                sm.GameOver(this.GetCancellationTokenOnDestroy()).Forget();
                snatched = false;
            }
            
        }
    }

    // 接触判定
    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        // 花輪アイテム使用
        if (col.GetComponent<Image>().sprite == garlandSpr)
        {
            // アイテム消費処理
            col.GetComponent<Image>().sprite = null; 
            im.UsedItem();                           
            // 自身に花輪が乗るフラグをONにし、移動アニメーションの再生
            garlandExists = true;
            MoveForward();
        }
        // イチゴアイテム使用
        else if (col.GetComponent<Image>().sprite == strawberrySpr)
        {
            // アイテム消費処理
            col.GetComponent<Image>().sprite = null;
            im.UsedItem();                           
            // 自身にイチゴが乗るフラグをONにし、移動アニメーションの再生
            strawberryExists = true;
            MoveForward();
        }
    }

    // 前進移動処理
    public void MoveForward()
    {
        moveCount++;
        // 移動アニメーションが8回再生されていたら、ゲームオーバー
        if(moveCount == 8)
        {
            SnatchAPlayer();
            return;
        }

        // (使用されたアイテムに応じて)移動アニメーション再生
        if(!garlandExists && !strawberryExists) // 花輪×、イチゴ×
        {
            animator.Play("EnemyMove1");
        }
        else if(!garlandExists && strawberryExists)    // 花輪×、イチゴ⚪︎
        {
            animator.Play("EnemyMove2");
        }
        else if(garlandExists && !strawberryExists)    // 花輪⚪︎、イチゴ×
        {
            animator.Play("EnemyMove3");
        }
        // 花輪⚪︎,イチゴ⚪︎なら、ゲームクリア処理
        else
        {
            animator.Play("EnemyMove4");
            CanAppearFemaleCentaur();
        }

        // 自身のサイズと座標を変更する
        ChangePosition();
        ChangeSize();

        
    }
    // (前進時)自身の座標変更
    private void ChangePosition()
    {
        // 自身の座標変更
        float posX = this.GetComponent<RectTransform>().anchoredPosition.x;
        float posY = this.GetComponent<RectTransform>().anchoredPosition.y;
        rect.anchoredPosition = new Vector2(posX - deltaPos, posY - (deltaPos * 2));
    }
    // (前進時)自身の幅&高さ変更
    private void ChangeSize()
    {
        // 自身の幅と高さ変更
        float width = this.GetComponent<RectTransform>().sizeDelta.x;
        float height = this.GetComponent<RectTransform>().sizeDelta.y;
        rect.sizeDelta = new Vector2(width + (deltaSizeX * 10f), height + (deltaSizeY * 10f));
    }


    // Playerを連れ去る処理
    private void SnatchAPlayer()
    {
        player.GetComponent<Image>().enabled = false;

        // (使用されたアイテムに応じて)Playerを連れ去るアニメーション再生
        if (garlandExists == false && strawberryExists == false) // 花輪×、イチゴ×
        {
            animator.Play("EnemySnatch1");
        }
        else if (garlandExists == false && strawberryExists)  // 花輪×、イチゴ⚪︎
        {
            animator.Play("EnemySnatch2");
        }
        else if (garlandExists && strawberryExists == false)  // 花輪⚪︎、イチゴ×
        {
            animator.Play("EnemySnatch3");
        }
        else                                                // 花輪⚪︎、イチゴ⚪︎
        {
            animator.Play("EnemySnatch4");
        }
        snatched = true;

    }

    // 女ケンタウロス出現(ゲームクリア)可能に
    private void CanAppearFemaleCentaur()
    {
        clickCancelPnl.SetActive(true);
        // GreeenGrassが揺れ続けるアニメーション再生
        GreenGrassL.GetComponent<Animator>().Play("GreenGrassLKeepShaking");
        GreenGrassR.GetComponent<Animator>().Play("GreenGrassRKeepShaking");
        // 女ケンタウロス出現ボタンを押したら、ゲームクリア
        FemaleCentaurBtn.SetActive(true);

    }
}
