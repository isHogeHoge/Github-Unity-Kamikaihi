using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class CookingDoughtAnimaCnt : MonoBehaviour
{
    [SerializeField] Image img_usedFlour;      // 小麦粉(使用後)
    [SerializeField] Animator animator_usingMilkCarton; // 牛乳パックアニメーション
    [SerializeField] Animator animator_usingMilk;       // 牛乳(液体)アニメーション
    [SerializeField] Image img_usedMilk;        // 牛乳パック(使用後)
    [SerializeField] SpriteRenderer sr_usingSuger;      // 砂糖
    [SerializeField] Image img_usedSuger;       // 砂糖(使用後)
    [SerializeField] Image img_usedChiliSauce;  // チリソース(使用後)
    [SerializeField] SpriteRenderer sr_usingButter;      // バター
    [SerializeField] Image img_usedButter;      // バター(使用後)
    [SerializeField] Image img_usedEgg;         // 卵(使用後)
    [SerializeField] GameObject finishedDough;
    [SerializeField] BoxCollider2D boxCol_boll;           // Boll_Back
    [SerializeField] SpriteRenderer sr_yellowDough_InBoll;
    [SerializeField] SpriteRenderer sr_redDough_InBoll;
    [SerializeField] Animator animator_whisk;              // 泡立て器
    [SerializeField] SpriteRenderer sr_dough_OnBoard;
    [SerializeField] Image img_cookieDoughBtn;

    [SerializeField] Sprite redDough_InBollSpr;
    [SerializeField] Sprite redDough_OnBoradSpr;
    [SerializeField] Sprite rCookieDoughSpr; // クッキー生地(赤)の画像

    private static int count_ingredient = 0;   // 使用した材料アイテムの数(チリソースアイテム以外)

    // オブジェクトが破棄された時
    private void OnDestroy()
    {
        // 使用した材料アイテムの数を0にリセット
        count_ingredient = 0;
    }

    // 使用した素材アイテムを数えるメソッド
    private void CountUsedIngredient()
    {
        count_ingredient++;
        // 材料(flour,milk,butter,suger,egg)を全て使用していたら、生地完成
        if (count_ingredient == 5)
        {
            Finished_Dough();
        }
    }
    // 生地完成処理
    private void Finished_Dough()
    {
        // ボウルの中の生地が赤なら
        if (sr_redDough_InBoll.enabled)
        {
            // 完成した生地の色を赤に
            finishedDough.GetComponent<SpriteRenderer>().sprite = redDough_InBollSpr;
            sr_dough_OnBoard.sprite = redDough_OnBoradSpr;
            img_cookieDoughBtn.sprite = rCookieDoughSpr;
        }
        // ボウルの中の生地が黄色なら変更なし(黄色のまま)
        // 生地完成アニメーション再生
        finishedDough.GetComponent<Animator>().enabled = true;

        // ボウルの中の生地を非表示に
        sr_yellowDough_InBoll.enabled = false;
        sr_redDough_InBoll.enabled = false;
        // 泡立て器(whisk)のアニメーションを停止
        animator_whisk.Play("WhiskStop");
        
        // bollにアイテムを使用できないようにする
        boxCol_boll.enabled = false;
    }

    // +++++++ Flour +++++++
    // アニメーション終了時
    private void UsedFlour()
    {
        // 小麦粉(使用後)を表示
        img_usedFlour.enabled = true;
        // 材料(flour, milk, butter, suger, egg)を全て使用していたら、生地完成処理
        CountUsedIngredient();
    }
    // ++++++++++++++++++++++

    // ++++++ MilkCarton ++++++
    // 牛乳パックを傾けるアニメーション終了時("MilkCartonAnima1")
    private void PlayMilkAnimation()
    {
        // 牛乳(液体)アニメーションを再生
        animator_usingMilk.enabled = true;
    }
    // 牛乳パックの傾きを戻すアニメーション終了時("MilkCartonAnima2")
    private void UsedMilk()
    {
        // 牛乳(使用後)を表示
        img_usedMilk.enabled = true;
        // 材料(flour, milk, butter, suger, egg)を全て使用していたら、生地完成処理
        CountUsedIngredient();
    }
    // +++++++++++++++++++++++++

    // ++++++++ Milk ++++++++
    // 牛乳(液体)をボウルに注ぐアニメーション終了時
    private void PlayMilkCartonAnima2()
    {
        // 牛乳パックの傾きを戻すアニメーション再生
        animator_usingMilkCarton.Play("MilkCartonAnima2");
    }
    // +++++++++++++++++++++++

    // +++++++ Spoon ++++++++
    // アニメーション開始時
    private void ActiveSuger()
    {
        // 砂糖を表示
        sr_usingSuger.enabled = true;
    }
    // アニメーション終了後
    private void UsedSuger()
    {
        // 砂糖(使用後)を表示
        sr_usingSuger.enabled = false;
        img_usedSuger.enabled = true;
        // 材料(flour, milk, butter, suger, egg)を全て使用していたら、生地完成処理
        CountUsedIngredient();
    }
    // +++++++++++++++++++++++++

    // ++++++ ChiliSauce +++++++
    // アニメーション終了時
    private void UsedChiliSauce()
    {
        // チリソース(使用後)を表示
        img_usedChiliSauce.enabled = true;
    }
    // +++++++++++++++++++++++++

    // ++++++++ Butter +++++++++
    // アニメーション開始時
    private void ActiveButter()
    {
        // バターを表示
        sr_usingButter.enabled = true;
    }
    // アニメーション終了時
    private void UsedButter()
    {
        // バター(使用後)を表示
        sr_usingButter.enabled = false;
        img_usedButter.enabled = true;

        // 材料(flour, milk, butter, suger, egg)を全て使用していたら、生地完成処理
        CountUsedIngredient();
    }
    // ++++++++++++++++++++++++++

    // ++++++++ UsingEgg ++++++++
    // アニメーション終了時
    private void UsedEgg()
    {
        // 卵(使用後)を表示
        img_usedEgg.enabled = true;
        // 材料(flour, milk, butter, suger, egg)を全て使用していたら、生地完成処理
        CountUsedIngredient();
    }
    // +++++++++++++++++++++++++++

   
}
