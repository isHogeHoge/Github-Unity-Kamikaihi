using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
// アイテム合成クラス
// 該当のアイテムを3個入手したら別のアイテムと交換
public class CraftItemManager : MonoBehaviour
{
    [SerializeField] GameObject itemPanel;          
    [SerializeField] GameObject itemImage;          
    [SerializeField] GameObject itemInventory;      
    [SerializeField] GameObject itemManager;        
    [SerializeField] GameObject audioPlayerSE;
    [SerializeField] Sprite composableItemSpr;    // 合成前のアイテム
    [SerializeField] Sprite composedItemSpr;     // 合成後のアイテム
    [SerializeField] AudioClip se;             // アイテム取得時SE

    private int clickCount;  // 合成前のアイテムボタンをクリックした数

    // 合成前のアイテムボタンをクリックした時
    public void ClickComposableItem()
    {
        clickCount++;

        // 合成前のアイテムボタンを全て(3個)クリックしたら、合成後アイテムと交換する
        if (clickCount == 3)
        {
            // SEを鳴らす
            audioPlayerSE.GetComponent<AudioSource>().PlayOneShot(se);

            // クリックしたボタンを使用不可に
            GameObject clickedBtn = EventSystem.current.currentSelectedGameObject;
            clickedBtn.SetActive(false);

            // アイテム欄から合成前のアイテム画像を全て消去
            for (var i = 0; i < itemInventory.transform.childCount; i++)
            {
                GameObject obj = itemInventory.transform.GetChild(i).gameObject;
                if (obj.GetComponent<Image>().sprite == composableItemSpr)
                {
                    obj.GetComponent<Image>().sprite = null;
                }
            }
            // アイテム欄を整理する
            itemManager.GetComponent<ItemManager>().UsedItem();

            // 合成後のアイテム取得
            Time.timeScale = 0.0f;
            itemPanel.SetActive(true);
            // アイテム取得パネルに合成後のアイテム画像を代入
            itemImage.GetComponent<Image>().sprite = composedItemSpr;
        }
        // それ以外なら、普通のアイテム取得処理
        else
        {
            itemManager.GetComponent<ItemManager>().ClickItemBtn(composableItemSpr);
        }
    }
}
