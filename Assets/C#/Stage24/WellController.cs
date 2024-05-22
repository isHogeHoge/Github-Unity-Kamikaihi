using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Cysharp.Threading.Tasks;
public class WellController : MonoBehaviour
{
    [SerializeField] Button maskBtn;  // カッパマスク取得ボタン
    [SerializeField] GameObject friend2;
    [SerializeField] SpriteRenderer sr_surprisedFriend2;
    [SerializeField] SpriteRenderer sr_faintingFriend2;
    [SerializeField] SpriteRenderer sr_mask;        // カッパマスク
    [SerializeField] GameObject itemManager;
    [SerializeField] GameObject stageManager;
    // アイテム画像
    [SerializeField] Sprite spiderSpr;

    // 接触判定(Item)
    private async void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        Image img_item = col.GetComponent<Image>();
        // 蜘蛛アイテム使用
        if (img_item.sprite == spiderSpr)
        {
            // アイテム使用処理
            img_item.sprite = null;
            itemManager.GetComponent<ItemManager>().UsedItem();

            //  --- Friend2が蜘蛛に驚く&気絶する → カッパマスクアイテム出現 ---
            friend2.SetActive(false);
            sr_surprisedFriend2.enabled = true;

            await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: this.GetCancellationTokenOnDestroy());

            sr_surprisedFriend2.enabled = false;
            sr_faintingFriend2.enabled = true;
            sr_mask.enabled = true;
            maskBtn.enabled = true;
            // ----------------------------------------------------------

        }
    }
}
