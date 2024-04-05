using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
public class Friend1Controller_17And26 : MonoBehaviour
{
    [SerializeField] GameObject helmet;
    [SerializeField] GameObject player;
    [SerializeField] GameObject watermelonOnTheGround; // WatermelonOnTheGround
    [SerializeField] GameObject hitEffect1;
    [SerializeField] GameObject hitEffect2;
    [SerializeField] GameObject fadePanel;
    [SerializeField] GameObject stageManager;
    [SerializeField] GameObject itemManager;
    // アイテム画像
    [SerializeField] Sprite clabSpr;

    // 接触判定(Item)
    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        // カニアイテム使用
        if (col.GetComponent<Image>().sprite == clabSpr)
        {
            // アイテム消費処理
            col.GetComponent<Image>().sprite = null;
            itemManager.GetComponent<ItemManager>().UsedItem();

            // ゲーム操作を禁止に
            stageManager.GetComponent<StageManager>().CantGameControl();

            // Friend1に左隣にスイカがあったら
            if (watermelonOnTheGround.GetComponent<Image>().enabled)
            {
                // カニに挟まれる→左に倒れるアニメーション再生
                this.GetComponent<Animator>().Play("Friend1IsNippedL");
            }
            // Friend1に左隣にスイカがなければ
            else
            {
                // カニに挟まれる→右に倒れるアニメーション再生
                this.GetComponent<Animator>().Play("Friend1IsNippedR");
            }
        }
    }

    // ----------- Animation -------------
    /// <summary>
    /// 場面切り替え & ゲームオーバーアニメーション再生
    /// </summary>
    /// <param name="num">1or3.再生するゲームオーバーアニメーションを選択</param>
    /// <returns></returns>
    private async UniTask PlayGameOverAnima(int num, CancellationToken ct)
    {
        // フェードイン
        await fadePanel.GetComponent<FadeInAndOut>().FadeIn(ct);

        // ゲームオーバーアニメーション再生
        this.GetComponent<Animator>().Play("Friend1Apologize");
        player.GetComponent<Animator>().Play($"PlayerOver{num}");

        // フェードアウト
        await fadePanel.GetComponent<FadeInAndOut>().FadeOut(ct);
    }

    // "Friend1Swing3"アニメーション開始時
    private void ActiveHitEffect2()
    {
        // Playerの頭上にHitEffectを表示
        hitEffect2.GetComponent<Animator>().enabled = true;
    }
    // 木刀を振った後
    private void GameEnd()
    {
        // Playerの頭の上にスイカオブジェクトが表示されていたら
        if (player.transform.GetChild(0).GetComponent<Image>().enabled)
        {
            player.transform.GetChild(0).GetComponent<Image>().enabled = false;
            // ゲームオーバーアニメーションを再生("PlayerOver1")
            PlayGameOverAnima(1, this.GetCancellationTokenOnDestroy()).Forget();
        }
        // Playerがヘルメットをかぶっていたら、そのままステージクリア
        else if (helmet != null && helmet.GetComponent<Image>().enabled)
        {
            stageManager.GetComponent<StageManager>().GameClear(26, this.GetCancellationTokenOnDestroy()).Forget();
        }
        // どちらも表示されていなかったら
        else
        {
            // ゲームオーバーアニメーションを再生("PlayerOver3")
            PlayGameOverAnima(3, this.GetCancellationTokenOnDestroy()).Forget();
        }


    }

    // 右に倒れるアニメーション終了時
    private void PlayPlayerOver3Anima()
    {
        // ゲームオーバーアニメーションを再生("PlayerOver3")
        PlayGameOverAnima(3, this.GetCancellationTokenOnDestroy()).Forget();
    }

    // 左に倒れるアニメーション再生中
    private void ActiveHitEffect1()
    {
        // スイカと木刀がぶつかった位置にHitEffectを表示
        hitEffect1.GetComponent<Image>().enabled = true;
    }
    // 左に倒れた後、ぶつかったスイカが割れる
    private void CutWatermelon()
    {
        hitEffect1.GetComponent<Image>().enabled = false;
        // ぶつかったスイカが揺れるアニメーション再生
        watermelonOnTheGround.GetComponent<Animator>().Play("WatermelonShake");

    }
    // -----------------------------------
}
