using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public class StageManager_2 : MonoBehaviour
{
    [SerializeField] Image img_paperHolder;
    [SerializeField] Animator animator_toiletLever;
    [SerializeField] Animator animator_player;
    [SerializeField] SpriteRenderer sr_speechBubble;
    [SerializeField] Sprite openSpr;   // トイレットペーパーホルダーが開いている画像
    [SerializeField] Sprite closeSpr;  // トイレットペーパーホルダーが閉まっている画像

    private bool isOpen = false;        // トイレットペーパーホルダー開閉フラグ
    private int clickCount = 0;        // トイレのレバークリック数

    // ---------- ボタン -----------
    // ドア
    public void ClickDoor()
    {
        // Playerがドアの方を見るアニメーションを再生
        animator_player.Play("PlayerTurn");
    }
    // トイレットペーパーホルダー
    public void ClickToiletPaperHodler()
    {
        // 開閉フラグを切り替える
        isOpen = !isOpen;
        if (isOpen) 
        {
            // トイレットペーパーホルダーが開いている画像を代入
            img_paperHolder.sprite = openSpr;
        }
        else
        {
            // トイレットペーパーホルダーが閉まっている画像を代入
            img_paperHolder.sprite = closeSpr;
        }
    }
    // トイレのレバー
    public void ClickToiletLever()
    {
        clickCount++;
        animator_toiletLever.Play("ToiletLeverIsPulled", 0, 0);
        // レバーを5回引いたら、ゲームオーバー
        if(clickCount == 5)
        {
            this.GetComponent<StageManager>().CantGameControl();
            animator_toiletLever.Play("ToiletLeverFall");
            animator_player.Play("PlayerOver2");
        }
    }
    // プレイヤー
    public void ClickPlayer()
    {
        // 吹き出しを表示・非表示
        sr_speechBubble.enabled = !sr_speechBubble.enabled;
    }
    // -----------------------------
}
