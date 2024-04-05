using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collider_TriosSushiCnt : MonoBehaviour
{
    [SerializeField] GameObject eatBtn;
    [SerializeField] GameObject triosSushi;
    [SerializeField] GameObject itemManager;
    // アイテム画像
    [SerializeField] Sprite shrimpSpr1; // えび寿司(わさびあり)画像
    [SerializeField] Sprite shrimpSpr2; // えび寿司(わさび抜き)画像

    private SushiController sc;
    private void Start()
    {
        sc = triosSushi.transform.GetChild(0).GetComponent<SushiController>();
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        Sprite itemSpr = col.GetComponent<Image>().sprite;
        // 皿の上に寿司を表示
        // えび寿司(わさびあり)
        if(itemSpr == shrimpSpr1)
        {
            // すでにえび寿司(わさび抜き)が皿にあるならメソッドを抜ける
            if (triosSushi.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled)
            {
                return;
            }
            ActiveShrimp(shrimpSpr1);
        }
        // えび寿司(わさび抜き)
        else if(itemSpr == shrimpSpr2)
        {
            // すでにえび寿司(わさびあり)が皿にあるならメソッドを抜ける
            if (triosSushi.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled)
            {
                return;
            }
            ActiveShrimp(shrimpSpr2);
        }
        // えび寿司以外は、そのまま表示
        else
        {
            GameObject obj = triosSushi.transform.Find($"{itemSpr.name}").gameObject;
            obj.GetComponent<SpriteRenderer>().enabled = true;
        }

        // trioのテーブルにある寿司が1個以上なら、寿司パネルをアクティブに
        // Trioのテーブル上にある寿司が3個以上なら、「食べる」ボタンを表示
        sc.PlusSushiCount();
        sc.isActiveSushiPnlAndEatBtn();

        // アイテム使用処理
        col.GetComponent<Image>().sprite = null;
        itemManager.GetComponent<ItemManager>().UsedItem();
    }

    // えび寿司(わさびあり・なし)を皿に表示する
    private void ActiveShrimp(Sprite shrimpSpr)
    {
        // アイテムパネルに表示する画像を、えび寿司(わさび抜き)に
        triosSushi.transform.GetChild(0).GetComponent<SushiController>().sushiSpr = shrimpSpr;
        triosSushi.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        triosSushi.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;

    }
}
