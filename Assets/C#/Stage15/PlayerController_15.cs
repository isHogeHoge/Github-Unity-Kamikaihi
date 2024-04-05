using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController_15 : MonoBehaviour
{
    [SerializeField] GameObject grandma;
    [SerializeField] GameObject beesInTheBeehive;
    [SerializeField] GameObject stone;
    [SerializeField] GameObject itemManager;
    [SerializeField] GameObject stageManager;
    [SerializeField] Sprite stoneSpr;

    internal bool throwedAStone = false; // 石アイテム使用でtrue
    internal bool wearHazmatSuits = false; // 防護服着用フラグ

    // 接触判定(Item)
    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        // 石アイテム使用
        if (col.GetComponent<Image>().sprite == stoneSpr)
        {
            // アイテム使用処理
            col.GetComponent<Image>().sprite = null;
            itemManager.GetComponent<ItemManager>().UsedItem();

            // ゲーム操作をできないようにする
            stageManager.GetComponent<StageManager_15>().CantGameControl();

            // 石を投げるアニメーション再生
            this.gameObject.GetComponent<Animator>().SetBool("ThrowFlag", true);
            throwedAStone = true;
        }
    }

    // ------------- Animation -------------
    // 防護服着用アニメーション再生時、フラグON
    private void WearHazmatSuits()
    {
        wearHazmatSuits = true;
    }

    // 石を投げるモーション後
    private void PlayStoneAnima()
    {
        // 石のアニメーション再生
        stone.SetActive(true);
    }

    // 移動アニメーション再生中
    // beeshiveからPlayerに向けてハチを移動させる
    private async void ActiveBeesInTheBeehive()
    {
        for (var i = 0; i < beesInTheBeehive.transform.childCount; i++)
        {
            GameObject obj = beesInTheBeehive.transform.GetChild(i).gameObject;
            // ハチ移動スクリプトをアクティブに
            obj.GetComponent<BeeUpAndDown>().enabled = false;
            obj.GetComponent<BeeMovement>().enabled = true;

            // 0.1秒ずつハチを表示
            obj.GetComponent<Image>().enabled = true;
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f),cancellationToken: this.GetCancellationTokenOnDestroy());
        }
    }

    // (クリア時の)移動アニメーション再生中
    private void ScrollStagePnl_Left()
    {
        // 左側のページにスクロールする
        stageManager.GetComponent<StageScrollCnt>().ScrollStagePnl("LEFT");
    }
    // (クリア時の)移動アニメーション終了時
    private void GameClear()
    {
        // ドアを開ける
        stageManager.GetComponent<StageManager_15>().OpenTheDoor();
        // Grandmaのアニメーション切り替え
        grandma.GetComponent<Animator>().Play("GrandmaMove");

        // ゲームクリア処理
        stageManager.GetComponent<StageManager>().GameClear(15, this.GetCancellationTokenOnDestroy()).Forget();
    }
    // -------------------------------------
}
