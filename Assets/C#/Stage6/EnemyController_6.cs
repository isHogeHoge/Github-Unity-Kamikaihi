using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class EnemyController_6 : MonoBehaviour
{
    [SerializeField] Animator animator_GreenGrassL;
    [SerializeField] Animator animator_GreenGrassR;
    [SerializeField] Image img_player;          
    [SerializeField] GameObject stageManager;    
    [SerializeField] GameObject itemManager;     
    [SerializeField] GameObject clickCancelPnl;  
    [SerializeField] GameObject FemaleCentaurBtn;   // 女ケンタウロス出現ボタン
    [SerializeField] Sprite garlandSpr;
    [SerializeField] Sprite strawberrySpr;

    private StageManager sm;                 
    private ItemManager im;                  
    private Animator animator_enemy;               // 自身のアニメーター
    private RectTransform rect_enemy;              // 自身のRectTransform
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
        animator_enemy = this.GetComponent<Animator>();
        rect_enemy = this.GetComponent<RectTransform>();

        startPos = rect_enemy.anchoredPosition;
    }

    private void FixedUpdate()
    {
        // ゲームオーバー時の移動処理
        if (snatched)
        {
            // スタート地点まで移動
            rect_enemy.anchoredPosition = Vector3.MoveTowards(rect_enemy.anchoredPosition, startPos, moveSpeed * Time.deltaTime);
            // サイズ(width,height)を徐々に小さくしていく
            rect_enemy.sizeDelta = new Vector2(rect_enemy.sizeDelta.x - deltaSizeX, rect_enemy.sizeDelta.y - deltaSizeY);
            // スタート地点を超えたら、移動を終える →　ゲームオーバー処理
            if(rect_enemy.anchoredPosition.y >= startPos.y || rect_enemy.anchoredPosition.x >= startPos.x)
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

        Image img_item = col.GetComponent<Image>();
        // 花輪アイテム使用
        if (img_item.sprite == garlandSpr)
        {
            // アイテム消費処理
            img_item.sprite = null; 
            im.UsedItem();                           
            // 自身に花輪が乗るフラグをONにし、移動アニメーションの再生
            garlandExists = true;
            MoveForward();
        }
        // イチゴアイテム使用
        else if (img_item.sprite == strawberrySpr)
        {
            // アイテム消費処理
            img_item.sprite = null;
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
            animator_enemy.Play("EnemyMove1");
        }
        else if(!garlandExists && strawberryExists)    // 花輪×、イチゴ⚪︎
        {
            animator_enemy.Play("EnemyMove2");
        }
        else if(garlandExists && !strawberryExists)    // 花輪⚪︎、イチゴ×
        {
            animator_enemy.Play("EnemyMove3");
        }
        // 花輪⚪︎,イチゴ⚪︎なら、ゲームクリア処理
        else
        {
            animator_enemy.Play("EnemyMove4");
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
        float posX = rect_enemy.anchoredPosition.x;
        float posY = rect_enemy.anchoredPosition.y;
        rect_enemy.anchoredPosition = new Vector2(posX - deltaPos, posY - (deltaPos * 2));
    }
    // (前進時)自身の幅&高さ変更
    private void ChangeSize()
    {
        // 自身の幅と高さ変更
        float width = rect_enemy.sizeDelta.x;
        float height = rect_enemy.sizeDelta.y;
        rect_enemy.sizeDelta = new Vector2(width + (deltaSizeX * 10f), height + (deltaSizeY * 10f));
    }


    // Playerを連れ去る処理
    private void SnatchAPlayer()
    {
        img_player.enabled = false;

        // (使用されたアイテムに応じて)Playerを連れ去るアニメーション再生
        if (garlandExists == false && strawberryExists == false) // 花輪×、イチゴ×
        {
            animator_enemy.Play("EnemySnatch1");
        }
        else if (garlandExists == false && strawberryExists)  // 花輪×、イチゴ⚪︎
        {
            animator_enemy.Play("EnemySnatch2");
        }
        else if (garlandExists && strawberryExists == false)  // 花輪⚪︎、イチゴ×
        {
            animator_enemy.Play("EnemySnatch3");
        }
        else                                                // 花輪⚪︎、イチゴ⚪︎
        {
            animator_enemy.Play("EnemySnatch4");
        }
        snatched = true;

    }

    // 女ケンタウロス出現(ゲームクリア)可能に
    private void CanAppearFemaleCentaur()
    {
        clickCancelPnl.SetActive(true);
        // GreeenGrassが揺れ続けるアニメーション再生
        animator_GreenGrassL.Play("GreenGrassLKeepShaking");
        animator_GreenGrassR.Play("GreenGrassRKeepShaking");
        // 女ケンタウロス出現ボタンを押したら、ゲームクリア
        FemaleCentaurBtn.SetActive(true);

    }
}
