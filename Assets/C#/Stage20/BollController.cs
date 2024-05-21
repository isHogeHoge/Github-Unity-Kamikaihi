using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BollController : MonoBehaviour
{
    [SerializeField] Animator animator_usingFlour;   
    [SerializeField] Animator animator_usingMilkCarton; 
    [SerializeField] Animator animator_usingSpoon;       
    [SerializeField] Animator animator_usingChiliSauce_Boll; 
    [SerializeField] Animator animator_usingCutButter;      
    [SerializeField] Animator animator_usingEgg;  
    [SerializeField] SpriteRenderer sr_yellowDough_InBoll;
    [SerializeField] SpriteRenderer sr_redDough_InBoll;
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

        Image img_item = col.GetComponent<Image>();
        // 小麦粉(flour)アイテム
        if (img_item.sprite == flourSpr)
        {
            // 小麦粉をボウルに入れる
            IngredientsIN(img_item, animator_usingFlour);

            // 事前にチリソースアイテムを使用していたら
            if (usedChiliSauce)
            {
                // ボウルの中に赤い生地を表示
                sr_redDough_InBoll.enabled = true;
            }
            // チリソースアイテムを使用していなかったら
            else
            {
                // ボウルの中に黄色い生地を表示
                sr_yellowDough_InBoll.enabled = true;
            }

        }
        // 牛乳(milk)アイテム
        else if(img_item.sprite == milkSpr)
        {
            // 牛乳をボウルに入れる
            IngredientsIN(img_item, animator_usingMilkCarton);
        }
        // 砂糖(suger)アイテム
        else if (img_item.sprite == sugerSpr)
        {
            // 砂糖をボウルに入れる
            IngredientsIN(img_item, animator_usingSpoon);
        }
        // チリソース(chiliSauce)アイテム
        else if(img_item.sprite == chiliSauceSpr)
        {
            usedChiliSauce = true;
            // チリソースをボウルに入れる
            IngredientsIN(img_item, animator_usingChiliSauce_Boll);

            // ボウルの中にすでに黄色い生地が表示されていたら
            if (sr_yellowDough_InBoll.enabled)
            {
                // 生地を赤にする
                sr_yellowDough_InBoll.enabled = false;
                sr_redDough_InBoll.enabled = true;
            }
        }
        // バター(butter)アイテム
        else if(img_item.sprite == butterSpr)
        {
            // バターをボウルに入れる
            IngredientsIN(img_item, animator_usingCutButter);
        }
        // 卵(egg)アイテム
        else if(img_item.sprite == eggSpr)
        {
            // 卵をボウルに入れる
            IngredientsIN(img_item, animator_usingEgg);
        }
    }

    // 材料(flour,milk,butter,suger,egg,chiliSauce)をボウルに入れる処理
    private void IngredientsIN(Image img_item,Animator animator_ingredient)
    {
        // アイテム使用処理
        img_item.sprite = null;
        itemManager.GetComponent<ItemManager>().UsedItem();

        // 材料をボールに入れるアニメーションを再生
        animator_ingredient.enabled = true;
                
    }
}
