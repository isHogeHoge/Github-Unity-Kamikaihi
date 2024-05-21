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
    [SerializeField] GameObject watermelonOnTheGround;
    [SerializeField] Image img_hitEffect1;
    [SerializeField] Animator animator_hitEffect2;
    [SerializeField] GameObject fadePanel;
    [SerializeField] GameObject stageManager;
    [SerializeField] GameObject itemManager;
    // アイテム画像
    [SerializeField] Sprite clabSpr;

    private StageManager sm;
    private FadeInAndOut fadeCnt;
    private Animator animator_friend1;
    private Image watermelonOnThePlayer;
    private void Start()
    {
        sm = stageManager.GetComponent<StageManager>();
        fadeCnt = fadePanel.GetComponent<FadeInAndOut>();
        animator_friend1 = this.GetComponent<Animator>();
        watermelonOnThePlayer = player.transform.GetChild(0).GetComponent<Image>();
    }

    // 接触判定(Item)
    private void OnTriggerExit2D(Collider2D col)
    {
        // (アイテム以外と接触)または(アイテムホールド中)なら、メソッドを抜ける
        if (col.gameObject.tag != "Item" || Input.GetMouseButton(0))
        {
            return;
        }

        Image img_item = col.GetComponent<Image>();
        // カニアイテム使用
        if (img_item.sprite == clabSpr)
        {
            // アイテム消費処理
            img_item.sprite = null;
            itemManager.GetComponent<ItemManager>().UsedItem();

            // ゲーム操作を禁止に
            sm.CantGameControl();

            // Friend1に左隣にスイカがあったら
            if (watermelonOnTheGround.GetComponent<Image>().enabled)
            {
                // カニに挟まれる→左に倒れるアニメーション再生
                animator_friend1.Play("Friend1IsNippedL");
            }
            // Friend1に左隣にスイカがなければ
            else
            {
                // カニに挟まれる→右に倒れるアニメーション再生
                animator_friend1.Play("Friend1IsNippedR");
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
        await fadeCnt.FadeIn(ct);

        // ゲームオーバーアニメーション再生
        animator_friend1.Play("Friend1Apologize");
        player.GetComponent<Animator>().Play($"PlayerOver{num}");

        // フェードアウト
        await fadeCnt.FadeOut(ct);
    }

    // "Friend1Swing3"アニメーション開始時
    private void ActiveHitEffect2()
    {
        // Playerの頭上にHitEffectを表示
        animator_hitEffect2.enabled = true;
    }
    // 木刀を振った後
    private void GameEnd()
    {
        // Playerの頭の上にスイカオブジェクトが表示されていたら
        if (watermelonOnThePlayer.enabled)
        {
            watermelonOnThePlayer.enabled = false;
            // ゲームオーバーアニメーションを再生("PlayerOver1")
            PlayGameOverAnima(1, this.GetCancellationTokenOnDestroy()).Forget();
        }
        // Playerがヘルメットをかぶっていたら、そのままステージクリア
        else if (helmet && helmet.GetComponent<Image>().enabled)
        {
            sm.GameClear(26, this.GetCancellationTokenOnDestroy()).Forget();
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
        img_hitEffect1.enabled = true;
    }
    // 左に倒れた後、ぶつかったスイカが割れる
    private void CutWatermelon()
    {
        img_hitEffect1.enabled = false;
        // ぶつかったスイカが揺れるアニメーション再生
        watermelonOnTheGround.GetComponent<Animator>().Play("WatermelonShake");

    }
    // -----------------------------------
}
