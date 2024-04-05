using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class CookieDoughAnimaCnt : MonoBehaviour
{
    [SerializeField] GameObject usedFlour;      // 小麦粉(使用後)
    [SerializeField] GameObject usingMilkCarton; // 牛乳パックアニメーション
    [SerializeField] GameObject usingMilk;       // 牛乳(液体)アニメーション
    [SerializeField] GameObject usedMilk;        // 牛乳パック(使用後)
    [SerializeField] GameObject usingSuger;      // 砂糖(アニメーション)
    [SerializeField] GameObject usedSuger;       // 砂糖(使用後)
    [SerializeField] GameObject usedChiliSauce;  // チリソース(使用後)
    [SerializeField] GameObject usingButter;      // バター(アニメーション)
    [SerializeField] GameObject usedButter;      // バター(使用後)
    [SerializeField] GameObject usedEgg;         // 卵(使用後)
    [SerializeField] GameObject finishedDough;
    [SerializeField] GameObject cookieCutter;
    [SerializeField] GameObject boll;               // Boll_Back
    [SerializeField] GameObject yellowDough_InBoll;
    [SerializeField] GameObject redDough_InBoll;
    [SerializeField] GameObject whisk;              // 泡立て器
    [SerializeField] GameObject dough_OnBoard;
    [SerializeField] GameObject cookieDoughBtn;
    [SerializeField] GameObject bakedCookieBtn;
    [SerializeField] GameObject plateOnTable;
    [SerializeField] GameObject stageManager;

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
            DoughIsFinished();
        }
    }
    // 生地完成処理
    private void DoughIsFinished()
    {
        // ボウルの中の生地が赤なら
        if (redDough_InBoll.GetComponent<SpriteRenderer>().enabled)
        {
            // 完成した生地の色を赤に
            finishedDough.GetComponent<SpriteRenderer>().sprite = redDough_InBollSpr;
            dough_OnBoard.GetComponent<SpriteRenderer>().sprite = redDough_OnBoradSpr;
            cookieDoughBtn.GetComponent<Image>().sprite = rCookieDoughSpr;
        }
        // ボウルの中の生地が黄色なら変更なし(黄色のまま)
        // 生地完成アニメーション再生
        finishedDough.GetComponent<Animator>().enabled = true;

        // ボウルの中の生地を非表示に
        yellowDough_InBoll.GetComponent<SpriteRenderer>().enabled = false;
        redDough_InBoll.GetComponent<SpriteRenderer>().enabled = false;
        // 泡立て器(whisk)のアニメーションを停止
        whisk.GetComponent<Animator>().Play("WhiskStop");
        
        // bollにアイテムを使用できないようにする
        boll.GetComponent<BoxCollider2D>().enabled = false;
    }

    // +++++++ Flour +++++++
    // アニメーション終了時
    private void UsedFlour()
    {
        // 小麦粉(使用後)を表示
        usedFlour.GetComponent<Image>().enabled = true;
        // 材料(flour, milk, butter, suger, egg)を全て使用していたら、生地完成処理
        CountUsedIngredient();
    }
    // ++++++++++++++++++++++

    // ++++++ MilkCarton ++++++
    // 牛乳パックを傾けるアニメーション終了時("MilkCartonAnima1")
    private void PlayMilkAnimation()
    {
        // 牛乳(液体)アニメーションを再生
        usingMilk.GetComponent<Animator>().enabled = true;
    }
    // 牛乳パックの傾きを戻すアニメーション終了時("MilkCartonAnima2")
    private void UsedMilk()
    {
        // 牛乳(使用後)を表示
        usedMilk.GetComponent<Image>().enabled = true;
        // 材料(flour, milk, butter, suger, egg)を全て使用していたら、生地完成処理
        CountUsedIngredient();
    }
    // +++++++++++++++++++++++++

    // ++++++++ Milk ++++++++
    // 牛乳(液体)をボウルに注ぐアニメーション終了時
    private void PlayMilkCartonAnima2()
    {
        // 牛乳パックの傾きを戻すアニメーション再生
        usingMilkCarton.GetComponent<Animator>().Play("MilkCartonAnima2");
    }
    // +++++++++++++++++++++++

    // +++++++ Spoon ++++++++
    // アニメーション開始時
    private void ActiveSuger()
    {
        // 砂糖を表示
        usingSuger.GetComponent<SpriteRenderer>().enabled = true;
    }
    // アニメーション終了後
    private void UsedSuger()
    {
        // 砂糖(使用後)を表示
        usingSuger.GetComponent<SpriteRenderer>().enabled = false;
        usedSuger.GetComponent<Image>().enabled = true;
        // 材料(flour, milk, butter, suger, egg)を全て使用していたら、生地完成処理
        CountUsedIngredient();
    }
    // +++++++++++++++++++++++++

    // ++++++ ChiliSauce +++++++
    // アニメーション終了時
    private void UsedChiliSauce()
    {
        // チリソース(使用後)を表示
        usedChiliSauce.GetComponent<Image>().enabled = true;
    }
    // +++++++++++++++++++++++++

    // ++++++++ Butter +++++++++
    // アニメーション開始時
    private void ActiveButter()
    {
        // バターを表示
        usingButter.GetComponent<SpriteRenderer>().enabled = true;
    }
    // アニメーション終了時
    private void UsedButter()
    {
        // バター(使用後)を表示
        usingButter.GetComponent<SpriteRenderer>().enabled = false;
        usedButter.GetComponent<Image>().enabled = true;

        // 材料(flour, milk, butter, suger, egg)を全て使用していたら、生地完成処理
        CountUsedIngredient();
    }
    // ++++++++++++++++++++++++++

    // ++++++++ UsingEgg ++++++++
    // アニメーション終了時
    private void UsedEgg()
    {
        // 卵(使用後)を表示
        usedEgg.GetComponent<Image>().enabled = true;
        // 材料(flour, milk, butter, suger, egg)を全て使用していたら、生地完成処理
        CountUsedIngredient();
    }
    // +++++++++++++++++++++++++++

    // ++++++ FinishedDough ++++++
    // Boardの上まで移動後
    private void StartCuttingOut()
    {
        // 生地の型抜き開始
        // boardの上にある生地を表示
        dough_OnBoard.GetComponent<SpriteRenderer>().enabled = true;
        // cookieCutterのアニメーション再生
        cookieCutter.GetComponent<Animator>().enabled = true;
    }
    // +++++++++++++++++++++++++++

    // ++++++ CookieCutter ++++++
    // "CookieCutter_Up"アニメーション再生中
    private void CookieDoughIsFinished()
    {
        // 型抜きクッキー(生地)完成
        // boardの上にある生地を非表示
        dough_OnBoard.GetComponent<SpriteRenderer>().enabled = false;
        // アイテム取得 & チリソースアイテム使用可能に
        cookieDoughBtn.GetComponent<Image>().enabled = true;
        cookieDoughBtn.GetComponent<BoxCollider2D>().enabled = true;

    }
    // ++++++++++++++++++++++++++

    // +++ PlateInTheFirePlace +++
    // クッキーを焼くアニメーション終了時
    private void BakedCookieIsFinished()
    {
        // テーブルの上にプレートを表示
        plateOnTable.GetComponent<SpriteRenderer>().enabled = true;
        // 焼き上がったクッキー(チョコペン使用×)アイテムを使用可能に
        plateOnTable.GetComponent<BoxCollider2D>().enabled = true;

        // 焼き上がったクッキーアイテムを取得可能に
        bakedCookieBtn.GetComponent<Image>().enabled = true;
        // チリソースアイテム使用可能に
        bakedCookieBtn.GetComponent<BoxCollider2D>().enabled = true;
    }
    // +++++++++++++++++++++++++++
}
