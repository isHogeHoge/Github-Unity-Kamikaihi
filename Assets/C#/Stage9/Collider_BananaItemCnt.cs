using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collider_BananaItemCnt : MonoBehaviour
{
    [SerializeField] GameObject BananaInBirdsNest;
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

        // バナナアイテム使用
        if (col.GetComponent<Image>().sprite == bananaSpr)
        {
            // アイテム使用処理
            col.GetComponent<Image>().sprite = null;
            im.UsedItem();                           

            // 鳥の巣にバナナを表示
            this.GetComponent<Image>().enabled = false;
            BananaInBirdsNest.GetComponent<Image>().enabled = true;

            // 猿出現
            monkey.SetActive(true);
        }

    }
}
