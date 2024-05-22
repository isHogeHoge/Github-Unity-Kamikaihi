using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class StageManager_21 : MonoBehaviour
{
    [SerializeField] GameObject smallEnemyBtn; // SmallEnemyを風呂に落とすボタン
    [SerializeField] Button safetyZoneBtn; 
    [SerializeField] Button treasureBtn;
    [SerializeField] Image img_goBtn;
    [SerializeField] GameObject player;
    [SerializeField] Animator animator_smallEnemy;
    [SerializeField] SpriteRenderer sr_safetyZone;
    [SerializeField] SpriteRenderer sr_fallingTreasure;
    [SerializeField] GameObject rock;
    [SerializeField] Sprite safetyZoneSpr; // 安全地帯(空)の画像
    [SerializeField] Sprite playerInTheSafetyZone1; // Playerが安全地帯に入っている画像(宝所持×)
    [SerializeField] Sprite playerInTheSafetyZone2; // Playerが安全地帯に入っている画像(宝所持⚪︎)

    private StageManager sm;
    private SpriteRenderer sr_player;
    private Animator animator_player;
    private void Start()
    {
        sm = this.GetComponent<StageManager>();
        sr_player = player.GetComponent<SpriteRenderer>();
        animator_player = player.GetComponent<Animator>();
    }

    // ---------- Button -----------
    // 「すすむ」ボタン
    public void ClickGoBtn()
    {
        // 小さいアヌビスが橋になっているアニメーションが再生中なら
        if(animator_smallEnemy.GetCurrentAnimatorClipInfo(0)[0].clip.name == "SmallEnemyFellDown")
        {
            // Playerが橋を渡るアニメーション再生
            animator_player.Play("PlayerCross");

            // 進むボタンをクリック不可能に
            img_goBtn.enabled = false;
        }
        else
        {
            // 橋がない時、首を振るアニメーションを再生
            animator_player.Play("ShakePlayer'sHead");
        }
    }

    // SmallEnemyを風呂に落とすボタン
    public void ClickSmallEnemyBtn()
    {
        // Playerが橋を渡っている最中なら、PlayerとSmallEnemy両方ともお湯風呂に落ちる(ゲームオーバー)
        if(animator_player.GetCurrentAnimatorClipInfo(0)[0].clip.name == "PlayerCross")
        {
            animator_player.Play("PlayerPanic");
            animator_smallEnemy.Play("SmallEnemyPanic");
        }
        // Playerが橋を渡っていなかったら、SmallEnemyだけお湯風呂に落ちる
        else
        {
            // 最後SmallEnemyに攻撃されてゲームオーバー
            animator_player.Play("PlayerIsSurprised1");
            animator_smallEnemy.Play("SmallEnemyPanic");
        }
    }

    // 安全地帯(SafetyZone)
    public void ClickSafetyZoneBtn()
    {
        // 宝をゲットしていたら
        if (!treasureBtn.enabled)
        {
            // ゲーム操作をできないようにする
            CantGameControl();
            safetyZoneBtn.enabled = false;

            // 安全地帯に入る(宝所持⚪︎)
            player.SetActive(false);
            sr_fallingTreasure.enabled = false;
            sr_safetyZone.sprite = playerInTheSafetyZone2;            

            // ゲームクリア処理
            sm.GameClear(21, this.GetCancellationTokenOnDestroy()).Forget();

        }
        // 宝をゲットしていなかったら
        else
        {
            // 安全地帯に入る(宝所持×)
            if (sr_player.enabled)
            {
                sr_player.enabled = false;
                sr_safetyZone.sprite = playerInTheSafetyZone1;
            }
            // 安全地帯からでる
            else
            {
                sr_player.enabled = true;
                sr_safetyZone.sprite = safetyZoneSpr;
            }
        }
    }

    // アヌビス壁画のひび割れ
    public void ClickCrackBtn3()
    {
        // ゲーム操作をできないようにする
        CantGameControl();
        // PlayerがSaftyZoneに入っていたら出す
        sr_player.enabled = true;
        sr_safetyZone.sprite = safetyZoneSpr;

        // ゲームオーバーアニメーション再生
        // Playerが宝を取得していない
        if (treasureBtn.enabled)
        {
            animator_player.Play("PlayerIsSurprised2");
        }
        // Playerが宝を取得している
        else
        {
            animator_player.Play("PlayerIsSurprised3");
        }

        // 岩を一時停止
        rock.GetComponent<RockController>().isMoving = false;
    }
// -----------------------------------

    // ゲーム操作をできないようにする
    internal void CantGameControl()
    {
        img_goBtn.enabled = false;
        sm.CantGameControl();
    }

}
