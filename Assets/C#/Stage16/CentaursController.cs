using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class CentaursController : MonoBehaviour
{
    [SerializeField] GameObject brother1;
    [SerializeField] GameObject brother1Btn;
    [SerializeField] GameObject centaurs;
    [SerializeField] GameObject centaur2;
    [SerializeField] GameObject centaur2Shadow;
    [SerializeField] GameObject btn_OpenTheWindow;
    [SerializeField] GameObject btn_OpenTheCurtain;
    [SerializeField] GameObject garland;
    [SerializeField] GameObject strawberry;
    [SerializeField] GameObject centaur1Btn;
    [SerializeField] GameObject rButton;
    [SerializeField] GameObject fadePanel;
    [SerializeField] GameObject itemManager;
    [SerializeField] GameObject stageManager;
    [SerializeField] Sprite garlandSpr;    
    [SerializeField] Sprite strawberrySpr; 

    internal bool canGetOut = false;    // Centaurs退出可能フラグ

    // Centaur1接触判定(Item)
    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        // 花輪アイテム使用
        if (col.GetComponent<Image>().sprite == garlandSpr)
        {
            // 飾りアイテム使用処理
            ActiveAccessory(col, garland);

            // 事前にイチゴアイテムが使用されていたら
            if (strawberry.GetComponent<Image>().enabled)
            {
                // Centaur2出現処理
                AppearCentaur2();
            }
        }
        // イチゴアイテム使用
        else if (col.GetComponent<Image>().sprite == strawberrySpr)
        {
            // 飾りアイテム使用処理
            ActiveAccessory(col, strawberry);

            // 事前に花輪アイテムが使用されていたら
            if (garland.GetComponent<Image>().enabled)
            {
                // Centaur2出現処理
                AppearCentaur2();
            }
        }
    }
    // 飾り(花輪・イチゴ)アイテム使用処理
    private void ActiveAccessory(Collider2D col, GameObject obj) // 接触したアイテム,表示する飾り
    {
        // アイテム使用処理
        col.GetComponent<Image>().sprite = null;
        itemManager.GetComponent<ItemManager>().UsedItem();

        // 自身に飾りアイテムを被せる
        obj.GetComponent<Image>().enabled = true;
    }

    // Centaur2出現処理
    private void AppearCentaur2()
    {
        // 画面をスクロールできないようにする
        rButton.SetActive(false);

        // Centaur2とその影を表示
        centaur2Shadow.SetActive(true);
        centaur2.GetComponent<Image>().enabled = true;

        // Centaurs退出可能に
        canGetOut = true;

        // カーテンと窓が両方空いていたら、そのままCentaursを退出させる
        if (!btn_OpenTheCurtain.GetComponent<Image>().enabled && !btn_OpenTheWindow.GetComponent<Image>().enabled)
        {
            CentaursGetOut(this.GetCancellationTokenOnDestroy()).Forget();
        }
    }

    // Centaurs退出処理
    internal async UniTask CentaursGetOut(CancellationToken ct)
    {
        centaur1Btn.SetActive(false);

        // Centaur1がCentaur2の方を向くアニメーション再生
        this.GetComponent<Animator>().Play("Centaur1Turn");

        // フェードイン&アウト
        await fadePanel.GetComponent<FadeInAndOut>().FadeInOut(ct);

        // 画面をスクロールできるようにする
        rButton.SetActive(true);

        // Centaur1を非アクティブに
        this.gameObject.SetActive(false);
        // Centaur2関連のオブジェクトを非アクティブに
        centaur2Shadow.SetActive(false);
        centaur2.GetComponent<Image>().enabled = false;

        // Centaursが退出するアニメーション再生
        centaurs.SetActive(true);

        // brotherをクリック & 出現可能に
        brother1.GetComponent<Animator>().Play("BrotherCanAppear");
        brother1Btn.GetComponent<Image>().enabled = true;

        canGetOut = false;
    }

}
