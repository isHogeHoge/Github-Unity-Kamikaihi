using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class StageManager_21 : MonoBehaviour
{
    [SerializeField] GameObject smallEnemyBtn; // SmallEnemyを風呂に落とすボタン
    [SerializeField] GameObject safetyZoneBtn; 
    [SerializeField] GameObject treasureBtn;
    [SerializeField] GameObject goBtn;
    [SerializeField] GameObject player;
    [SerializeField] GameObject smallEnemy;
    [SerializeField] GameObject safetyZone;
    [SerializeField] GameObject fallingTreasure;
    [SerializeField] GameObject rock;
    [SerializeField] Sprite safetyZoneSpr; // 安全地帯(空)の画像
    [SerializeField] Sprite playerInTheSafetyZone1; // Playerが安全地帯に入っている画像(宝所持×)
    [SerializeField] Sprite playerInTheSafetyZone2; // Playerが安全地帯に入っている画像(宝所持⚪︎)

    // ---------- Button -----------
    // 「すすむ」ボタン
    public void ClickGoBtn()
    {
        // 小さいアヌビスが橋になっているアニメーションが再生中なら
        Animator animator = smallEnemy.GetComponent<Animator>();
        if(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "SmallEnemyFellDown")
        {
            // Playerが橋を渡るアニメーション再生
            player.GetComponent<Animator>().Play("PlayerCross");

            // 進むボタンをクリック不可能に
            goBtn.GetComponent<Image>().enabled = false;
        }
        else
        {
            // 橋がない時、首を振るアニメーションを再生
            player.GetComponent<Animator>().Play("ShakePlayer'sHead");
        }
    }

    // SmallEnemyを風呂に落とすボタン
    public void ClickSmallEnemyBtn()
    {
        Animator animator = player.GetComponent<Animator>();
        // Playerが橋を渡っている最中なら、PlayerとSmallEnemy両方ともお湯風呂に落ちる(ゲームオーバー)
        if(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "PlayerCross")
        {
            player.GetComponent<Animator>().Play("PlayerPanic");
            smallEnemy.GetComponent<Animator>().Play("SmallEnemyPanic");
        }
        // Playerが橋を渡っていなかったら、SmallEnemyだけお湯風呂に落ちる
        else
        {
            // 最後SmallEnemyに攻撃されてゲームオーバー
            player.GetComponent<Animator>().Play("PlayerIsSurprised1");
            smallEnemy.GetComponent<Animator>().Play("SmallEnemyPanic");
        }
    }

    // 安全地帯(SafetyZone)
    public void ClickSafetyZoneBtn()
    {
        // 宝をゲットしていたら
        if (!treasureBtn.GetComponent<Button>().enabled)
        {
            // ゲーム操作をできないようにする
            CantGameControl();
            safetyZoneBtn.GetComponent<Button>().enabled = false;

            // 安全地帯に入る(宝所持⚪︎)
            player.SetActive(false);
            fallingTreasure.GetComponent<SpriteRenderer>().enabled = false;
            safetyZone.GetComponent<SpriteRenderer>().sprite = playerInTheSafetyZone2;            

            // ゲームクリア処理
            this.GetComponent<StageManager>().GameClear(21, this.GetCancellationTokenOnDestroy()).Forget();

        }
        // 宝をゲットしていなかったら
        else
        {
            // 安全地帯に入る(宝所持×)
            if (player.GetComponent<SpriteRenderer>().enabled)
            {
                player.GetComponent<SpriteRenderer>().enabled = false;
                safetyZone.GetComponent<SpriteRenderer>().sprite = playerInTheSafetyZone1;
            }
            // 安全地帯からでる
            else
            {
                player.GetComponent<SpriteRenderer>().enabled = true;
                safetyZone.GetComponent<SpriteRenderer>().sprite = safetyZoneSpr;
            }
        }
    }

    // アヌビス壁画のひび割れ
    public void ClickCrackBtn3()
    {
        // ゲーム操作をできないようにする
        CantGameControl();
        // PlayerがSaftyZoneに入っていたら出す
        player.GetComponent<SpriteRenderer>().enabled = true;
        safetyZone.GetComponent<SpriteRenderer>().sprite = safetyZoneSpr;

        // ゲームオーバーアニメーション再生
        // Playerが宝を取得していない
        if (treasureBtn.GetComponent<Button>().enabled)
        {
            player.GetComponent<Animator>().Play("PlayerIsSurprised2");
        }
        // Playerが宝を取得している
        else
        {
            player.GetComponent<Animator>().Play("PlayerIsSurprised3");
        }

        // 岩を一時停止
        rock.GetComponent<RockController>().isMoving = false;
    }
// -----------------------------------

    // ゲーム操作をできないようにする
    internal void CantGameControl()
    {
        goBtn.GetComponent<Image>().enabled = false;
        this.GetComponent<StageManager>().CantGameControl();
    }

}
