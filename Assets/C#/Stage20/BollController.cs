using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BollController : MonoBehaviour
{
    [SerializeField] GameObject usingFlour;   
    [SerializeField] GameObject usingMilkCarton; 
    [SerializeField] GameObject usingSpoon;       
    [SerializeField] GameObject usingChiliSauce_Boll; 
    [SerializeField] GameObject usingCutButter;      
    [SerializeField] GameObject usingEgg;  
    [SerializeField] GameObject yellowDough_InBoll;
    [SerializeField] GameObject redDough_InBoll;
    [SerializeField] GameObject itemManager;
    // アイテム画像
    [SerializeField] Sprite flourSpr;    
    [SerializeField] Sprite milkSpr;      
    [SerializeField] Sprite sugerSpr;     
    [SerializeField] Sprite chiliSauceSpr;
    [SerializeField] Sprite butterSpr;    
    [SerializeField] Sprite eggSpr;      

    private bool usedChiliSauce = false;  // チリソースアイテム使用フラグ

    // 接触判定(Item)
    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        // 小麦粉(flour)アイテム
        if (col.GetComponent<Image>().sprite == flourSpr)
        {
            // 小麦粉をボウルに入れる
            IngredientsIN(col, usingFlour);

            // 事前にチリソースアイテムを使用していたら
            if (usedChiliSauce)
            {
                // ボウルの中に赤い生地を表示
                redDough_InBoll.GetComponent<SpriteRenderer>().enabled = true;
            }
            // チリソースアイテムを使用していなかったら
            else
            {
                // ボウルの中に黄色い生地を表示
                yellowDough_InBoll.GetComponent<SpriteRenderer>().enabled = true;
            }

        }
        // 牛乳(milk)アイテム
        else if(col.GetComponent<Image>().sprite == milkSpr)
        {
            // 牛乳をボウルに入れる
            IngredientsIN(col, usingMilkCarton);
        }
        // 砂糖(suger)アイテム
        else if (col.GetComponent<Image>().sprite == sugerSpr)
        {
            // 砂糖をボウルに入れる
            IngredientsIN(col, usingSpoon);
        }
        // チリソース(chiliSauce)アイテム
        else if(col.GetComponent<Image>().sprite == chiliSauceSpr)
        {
            usedChiliSauce = true;
            // チリソースをボウルに入れる
            IngredientsIN(col, usingChiliSauce_Boll);

            // ボウルの中にすでに黄色い生地が表示されていたら
            if (yellowDough_InBoll.GetComponent<SpriteRenderer>().enabled)
            {
                // 生地を赤にする
                yellowDough_InBoll.GetComponent<SpriteRenderer>().enabled = false;
                redDough_InBoll.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        // バター(butter)アイテム
        else if(col.GetComponent<Image>().sprite == butterSpr)
        {
            // バターをボウルに入れる
            IngredientsIN(col, usingCutButter);
        }
        // 卵(egg)アイテム
        else if(col.GetComponent<Image>().sprite == eggSpr)
        {
            // 卵をボウルに入れる
            IngredientsIN(col, usingEgg);
        }
    }

    // 材料(flour,milk,butter,suger,egg,chiliSauce)をボウルに入れる処理
    private void IngredientsIN(Collider2D col,GameObject ingredient)
    {
        // アイテム使用処理
        col.GetComponent<Image>().sprite = null;
        itemManager.GetComponent<ItemManager>().UsedItem();

        // 材料をボールに入れるアニメーションを再生
        ingredient.GetComponent<Animator>().enabled = true;
                
    }
}
