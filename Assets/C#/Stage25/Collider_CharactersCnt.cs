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
    [SerializeField] SpriteRenderer sr_cake;
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

        Image img_character = col.GetComponent<Image>();
        // Grandma
        if(img_character.sprite == grandmaItemSpr)
        {
            CharacterAppear(img_character, grandma);
        }
        // Geek
        else if(img_character.sprite == geekItemSpr)
        {
            CharacterAppear(img_character, geek);
        }
        // Girlfriend
        else if(img_character.sprite == girlfriendItemSpr1 || img_character.sprite == girlfriendItemSpr2)
        {
            // Girlfriendがチョコレートケーキを持っている状態に変更
            if (img_character.sprite == girlfriendItemSpr2)
            {
                sr_cake.sprite = chocolateCakeSpr;
                girlfriend.GetComponent<Animator>().Play("GirlfriendHaveACake2");
            }
            CharacterAppear(img_character, girlfriend);
        }
        // Boyfriend
        else
        {
            CharacterAppear(img_character, boyfriend);
        }

        this.GetComponent<BoxCollider2D>().enabled = false;
    }
    /// <summary>
    /// アイテム消費 & キャラクター出現処理
    /// </summary>
    /// <param name="img_character">使用したキャラクターアイテム</param>
    /// <param name="someone">表示するキャラクター</param>
    private void CharacterAppear(Image img_character,GameObject someone)
    {
        // アイテム使用処理
        img_character.sprite = null;
        itemManager.GetComponent<ItemManager>().UsedItem();

        someone.GetComponent<SpriteRenderer>().enabled = true;
    }
}
