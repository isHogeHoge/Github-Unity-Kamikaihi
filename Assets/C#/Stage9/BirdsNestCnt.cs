using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BridsNestCnt : MonoBehaviour
{
    [SerializeField] Image img_bananaInBirdsNest;
    [SerializeField] GameObject monkey;
    [SerializeField] GameObject itemManager;
    [SerializeField] Sprite bananaSpr;

    private ItemManager im;

    private void Start()
    {
        im = itemManager.GetComponent<ItemManager>();
    }

    // 接触判定(Item)
    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        Image img_item = col.GetComponent<Image>();
        // バナナアイテム使用
        if (img_item.sprite == bananaSpr)
        {
            // アイテム使用処理

            img_item.sprite = null;
            im.UsedItem();                           

            // 鳥の巣にバナナを表示
            this.GetComponent<Image>().enabled = false;
            img_bananaInBirdsNest.enabled = true;

            // 猿出現
            monkey.SetActive(true);
        }

    }
}
