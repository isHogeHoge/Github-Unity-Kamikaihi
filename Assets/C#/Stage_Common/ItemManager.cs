using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemManager : MonoBehaviour
{
    [SerializeField] GameObject itemPanel;            
    [SerializeField] GameObject itemImage;            
    [SerializeField] GameObject label_fullItem;       // アイテム所持数Max時に表示されるラベル
    [SerializeField] GameObject itemInventory;        
    [SerializeField] GameObject audioPlayerSE;        
    [SerializeField] AudioClip itemSE;                // アイテム取得時SE

    private GameObject clickedItem = null;               // クリックされた所持アイテム
    private Ray ray;　　　　　　　　　　　                  // マウスポジションへのray(光線)
    private RaycastHit2D hit2d;                          // rayに当たったもの
    private Vector3 itemStartPos;                        // 所持アイテムの初期位置
    private bool isFollowing = false;                     // アイテムがマウスに追従するフラグ
    internal bool isFull = false;                         // アイテム所持数最大フラグ
    private int count_gottenItem = 0;                          // 現在のアイテム所持数

    private void Update()
    {
        // ポーズ中ならUpdateを抜ける
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        // アイテムをマウスに追従させる
        if (isFollowing)
        {
            //スクリーン座標をワールド座標に変換
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
            //アイテムの座標をマウスの座標(ワールド座標)に設定
            clickedItem.transform.position = worldPos;
        }
    }

    /// <summary>
    /// アイテムをクリックした時
    /// </summary>
    /// <param name="itemSpr">取得したアイテムの画像</param>
    public void ClickItemBtn(Sprite itemSpr)
    {
        // アイテムを５つ(Max)所持していたら、label_fullItemを表示&メソッドを抜ける
        if (isFull)
        {
            label_fullItem.GetComponent<Animator>().Play("FullItem_Active");
            return;
        }

        // アイテム取得時SEを再生
        audioPlayerSE.GetComponent<AudioSource>().PlayOneShot(itemSE);

        Time.timeScale = 0.0f;
        itemPanel.SetActive(true);

        // アイテム取得時パネルにアイテム画像を代入
        itemImage.GetComponent<Image>().sprite = itemSpr;

        // 再度アイテムを取得できないようにする
        GameObject clickedItemBtn = EventSystem.current.currentSelectedGameObject;
        if (clickedItemBtn)
        {
            clickedItemBtn.SetActive(false);
        }
    }

    // アイテム取得時パネルを閉じる時、アイテム欄にアイテムを追加
    public void ClickItemCloseBtn()
    {
        Time.timeScale = 1.0f;
        itemPanel.SetActive(false);
        GetItem();

    }

    // アイテム取得処理
    private void GetItem()
    {
        if(itemInventory.activeSelf == false)
        {
            itemInventory.SetActive(true);
        }

        // アイテム所持数を数え直す
        count_gottenItem = 0;
        
        // 取得したアイテム画像を空いているアイテム欄に代入
        for (var i = 0; i < itemInventory.transform.childCount; i++)
        {
            count_gottenItem++;

            GameObject item = itemInventory.transform.GetChild(i).gameObject;
            if (!item.GetComponent<Image>().sprite)   
            {
                item.SetActive(true);
                item.GetComponent<Image>().sprite = itemImage.GetComponent<Image>().sprite;

                // アイテムの透明度を0→1に
                Color color = item.GetComponent<Image>().color;
                color.a = 1.0f;                                  
                item.GetComponent<Image>().color = color;　　　　　

                break;

            }

        }

        // アイテム所持数がMax(5)なら、アイテム所持数最大フラグON
        if (count_gottenItem == 5)
        {
            isFull = true;
        }
    }

    // アイテム使用処理(アイテム欄の整理)
    public void UsedItem()
    {
        // 現在所持しているアイテムの画像代入用のリスト
        List<Sprite> items = new List<Sprite>();

        // 所持中のアイテム画像をリストに代入
        for(var i = 0; i < itemInventory.transform.childCount; i++)
        {
            GameObject item = itemInventory.transform.GetChild(i).gameObject;
            // アイテム画像を詰めてリストに代入(nullを弾く)
            if(item.GetComponent<Image>().sprite)
            {
                items.Add(item.GetComponent<Image>().sprite);
            }
        }

        // 現在のアイテム所持数を更新
        count_gottenItem = items.Count;
        isFull = false;

        // アイテム欄を整理する
        if (items.Count == 0)  
        {
            itemInventory.SetActive(false);
        }
        else
        {
            // +++ リスト出力 +++
            // 所持中のアイテムの画像をアイテム欄に再代入
            for (var i = 0; i < items.Count; i++)
            {
                GameObject item = itemInventory.transform.GetChild(i).gameObject;
                item.GetComponent<Image>().sprite = items[i];

            }
            // アイテム画像が代入されなかったアイテム欄は、非アクティブ&画像をnullにする
            for(var n = items.Count; n < itemInventory.transform.childCount; n++)
            {
                GameObject item = itemInventory.transform.GetChild(n).gameObject;
                
                item.SetActive(false);

                item.GetComponent<Image>().sprite = null;
                // アイテムの透明度を1→0に
                Color color = item.GetComponent<Image>().color;   
                color.a = 0.0f;                                  
                item.GetComponent<Image>().color = color;　　　　　

            }
            // +++++++++++++++++
        }        
    }

    // 所持中のアイテムをクリックした時、アイテムをマウスに追従させる
    public void PointerDownItem()
    {
        // クリックした場所から伸びるrayに当たったオブジェクトを取得
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);
        // rayに当たったのが所持中のアイテムなら
        if (hit2d && hit2d.transform.tag == "Item")
        {
            // クリックしたアイテムを取得
            clickedItem = hit2d.transform.gameObject;

            // 初期位置の設定 & アイテム欄のアイテムをマウスに追従させる
            itemStartPos = clickedItem.transform.position;
            isFollowing = true;
        }
        

        
    }

    // ホールド中のアイテムを離した時
    public void PointerUpItem()
    {
        // マウスに追従していたアイテムをアイテム欄に戻す
        isFollowing = false;
        clickedItem.transform.position = itemStartPos;
    }

    
}
