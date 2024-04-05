using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Collider_CharactersCnt : MonoBehaviour
{
    [SerializeField] GameObject grandma;
    [SerializeField] GameObject geek;
    [SerializeField] GameObject girlfriend;
    [SerializeField] GameObject boyfriend;
    [SerializeField] GameObject cake;
    [SerializeField] GameObject itemManager;
    [SerializeField] Sprite grandmaItemSpr;
    [SerializeField] Sprite geekItemSpr;
    [SerializeField] Sprite girlfriendItemSpr1; // Girlfriendがプレーンケーキを持っているアイテム画像
    [SerializeField] Sprite girlfriendItemSpr2; // Girlfriendがチョコケーキを持っているアイテム画像
    [SerializeField] Sprite chocolateCakeSpr;  // チョコレートケーキの画像
    [SerializeField] Sprite boyfriendItemSpr;

    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        // 使用したキャラクターを表示
        // Grandma
        if(col.GetComponent<Image>().sprite == grandmaItemSpr)
        {
            CharacterAppear(col, grandma);
        }
        // Geek
        else if(col.GetComponent<Image>().sprite == geekItemSpr)
        {
            CharacterAppear(col, geek);
        }
        // Girlfriend
        else if(col.GetComponent<Image>().sprite == girlfriendItemSpr1 || col.GetComponent<Image>().sprite == girlfriendItemSpr2)
        {
            // Girlfriendがチョコレートケーキを持っている状態に変更
            if (col.GetComponent<Image>().sprite == girlfriendItemSpr2)
            {
                cake.GetComponent<SpriteRenderer>().sprite = chocolateCakeSpr;
                girlfriend.GetComponent<Animator>().Play("GirlfriendHaveACake2");
            }
            CharacterAppear(col, girlfriend);
        }
        // Boyfriend
        else
        {
            CharacterAppear(col, boyfriend);
        }

        this.GetComponent<BoxCollider2D>().enabled = false;
    }
    /// <summary>
    /// アイテム消費 & キャラクター出現処理
    /// </summary>
    /// <param name="col">使用したキャラクターアイテム</param>
    /// <param name="someone">表示するキャラクター</param>
    private void CharacterAppear(Collider2D col,GameObject someone)
    {
        // アイテム使用処理
        col.GetComponent<Image>().sprite = null;
        itemManager.GetComponent<ItemManager>().UsedItem();

        someone.GetComponent<SpriteRenderer>().enabled = true;
    }
}
