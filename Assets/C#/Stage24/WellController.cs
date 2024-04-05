using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Cysharp.Threading.Tasks;
public class WellController : MonoBehaviour
{
    [SerializeField] GameObject maskBtn;  // カッパマスク取得ボタン
    [SerializeField] GameObject surprisedFriend2; // 蜘蛛に驚いているFriend2
    [SerializeField] GameObject faintingFriend2;  // 気絶しているFriend2
    [SerializeField] GameObject mask;             // カッパマスク
    [SerializeField] GameObject itemManager;
    [SerializeField] GameObject stageManager;
    // アイテム画像
    [SerializeField] Sprite spiderSpr;

    internal bool isScaredByFriend2 = true;    // Friend2に驚かされるフラグ

    // 接触判定(Item)
    private async void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        // 蜘蛛アイテム使用
        if (col.GetComponent<Image>().sprite == spiderSpr)
        {
            // アイテム使用処理
            col.GetComponent<Image>().sprite = null;
            itemManager.GetComponent<ItemManager>().UsedItem();

            //  --- Friend2が蜘蛛に驚く&気絶する → カッパマスクアイテム出現 ---
            surprisedFriend2.GetComponent<SpriteRenderer>().enabled = true;

            await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: this.GetCancellationTokenOnDestroy());

            surprisedFriend2.GetComponent<SpriteRenderer>().enabled = false;
            faintingFriend2.GetComponent<SpriteRenderer>().enabled = true;
            mask.GetComponent<SpriteRenderer>().enabled = true;
            maskBtn.GetComponent<Button>().enabled = true;
            // ----------------------------------------------------------

            // Friend2に驚かされるフラグOFF
            isScaredByFriend2 = false;


        }
    }
}
