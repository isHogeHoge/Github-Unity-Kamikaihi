using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class BeehiveController : MonoBehaviour
{
    [SerializeField] GameObject smokeUnderTheBeehive;
    [SerializeField] GameObject beehiveOnTheGround;
    [SerializeField] GameObject beesByTheBeehive;
    [SerializeField] GameObject beesInTheBeehive;
    [SerializeField] GameObject beesNearTheGround;
    [SerializeField] GameObject fairy;
    [SerializeField] GameObject fairyBtn;
    [SerializeField] GameObject itemManager;
    [SerializeField] Sprite smokeSpr;

    // 接触判定(Item)
    private async void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        // 煙アイテム使用
        if (col.GetComponent<Image>().sprite == smokeSpr)
        {
            // アイテム使用処理
            col.GetComponent<Image>().sprite = null;
            itemManager.GetComponent<ItemManager>().UsedItem();

            // (beehive下に)煙アニメーション再生
            smokeUnderTheBeehive.SetActive(true);
            // beehiveからハチ出現
            for(var i = 0; i < beesInTheBeehive.transform.childCount; i++)
            {
                GameObject obj = beesInTheBeehive.transform.GetChild(i).gameObject;
                // 0.3秒ずつハチを表示
                obj.GetComponent<Image>().enabled = true;
                await UniTask.Delay(TimeSpan.FromSeconds(0.3f));
            }

            // 妖精出現
            fairy.GetComponent<Image>().enabled = true;
            fairy.GetComponent<Animator>().enabled = true;
        }
    }

    // ----------- Animation -----------
    // 落下アニメーション開始時
    private void InActiveBeesInTheBeehive()
    {
        // 煙アイテム使用で出現したハチを非アクティブに
        if (beesInTheBeehive.activeSelf)
        {
            beesInTheBeehive.SetActive(false);
        }
    }
    // 落下アニメーション終了時
    private void BeesMoveAndAppear()
    {
        // 落下後のbeehiveを表示
        beehiveOnTheGround.GetComponent<Image>().enabled = true;

        // 横にいたハチ2匹を落下した位置まで移動させる
        for (var i = 0; i < beesByTheBeehive.transform.childCount; i++)
        {
            beesByTheBeehive.transform.GetChild(i).GetComponent<BeeUpAndDown>().enabled = false;
            beesByTheBeehive.transform.GetChild(i).GetComponent<BeeMovement>().enabled = true;

        }
        // 地面付近にハチを表示
        beesNearTheGround.SetActive(true);
    }
    // ---------------------------------
}
