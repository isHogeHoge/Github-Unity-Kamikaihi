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

    private SushiController sushiCnt;
    private SpriteRenderer sr_shrimp;
    private void Start()
    {
        sushiCnt = triosSushi.transform.GetChild(0).GetComponent<SushiController>();
        sr_shrimp = triosSushi.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        Image img_sushi = col.GetComponent<Image>();
        // 皿の上に寿司を表示
        // えび寿司(わさびあり)
        if(img_sushi.sprite == shrimpSpr1)
        {
            // すでにえび寿司(わさび抜き)が皿にあるならメソッドを抜ける
            if (sr_shrimp.enabled)
            {
                return;
            }
            ActiveShrimp(shrimpSpr1);
        }
        // えび寿司(わさび抜き)
        else if(img_sushi.sprite == shrimpSpr2)
        {
            // すでにえび寿司(わさびあり)が皿にあるならメソッドを抜ける
            if (sr_shrimp.enabled)
            {
                return;
            }
            ActiveShrimp(shrimpSpr2);
        }
        // えび寿司以外は、そのまま表示
        else
        {
            GameObject sushi = triosSushi.transform.Find($"{img_sushi.sprite.name}").gameObject;
            sushi.GetComponent<SpriteRenderer>().enabled = true;
        }

        // trioのテーブルにある寿司が1個以上なら、寿司パネルをアクティブに
        // Trioのテーブル上にある寿司が3個以上なら、「食べる」ボタンを表示
        sushiCnt.PlusSushiCount();
        sushiCnt.isActiveSushiPnlAndEatBtn();

        // アイテム使用処理
        img_sushi.sprite = null;
        itemManager.GetComponent<ItemManager>().UsedItem();
    }

    // えび寿司(わさびあり・なし)を皿に表示する
    private void ActiveShrimp(Sprite shrimpSpr)
    {
        // アイテムパネルに表示する画像を、えび寿司(わさび抜き)に
        sushiCnt.sushiSpr = shrimpSpr;
        sr_shrimp.enabled = true;
        triosSushi.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;

    }
}
