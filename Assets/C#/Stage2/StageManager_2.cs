using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public class StageManager_2 : MonoBehaviour
{
    [SerializeField] GameObject toiletLever;
    [SerializeField] GameObject player;        
    [SerializeField] GameObject speechBubble;  
    [SerializeField] GameObject paperHolder;   
    [SerializeField] Sprite openSpr;           // トイレットペーパーホルダーが開いている画像
    [SerializeField] Sprite closeSpr;          // トイレットペーパーホルダーが閉まっている画像

    private bool isOpen = false;                    // トイレットペーパーホルダー開閉フラグ
    private int clickCount = 0;                 // トイレのレバークリック数

    // ---------- ボタン -----------
    // ドア
    public void ClickDoor()
    {
        // Playerがドアの方を見るアニメーションを再生
        player.GetComponent<Animator>().Play("PlayerTurn");
    }
    // トイレットペーパーホルダー
    public void ClickToiletPaperHodler()
    {
        // 開閉フラグを切り替える
        isOpen = !isOpen;
        if (isOpen) 
        {
            // トイレットペーパーホルダーが開いている画像を代入
            paperHolder.GetComponent<Image>().sprite = openSpr;
        }
        else
        {
            // トイレットペーパーホルダーが閉まっている画像を代入
            paperHolder.GetComponent<Image>().sprite = closeSpr;
        }
    }
    // トイレのレバー
    public void ClickToiletLever()
    {
        clickCount++;
        toiletLever.GetComponent<Animator>().Play("ToiletLeverIsPulled", 0, 0);
        // レバーを5回引いたら、ゲームオーバー
        if(clickCount == 5)
        {
            toiletLever.GetComponent<Animator>().Play("ToiletLeverFall");
            player.GetComponent<Animator>().Play("PlayerOver2");
        }
    }
    // プレイヤー
    public void ClickPlayer()
    {
        // 吹き出しを表示・非表示
        speechBubble.GetComponent<SpriteRenderer>().enabled = !speechBubble.GetComponent<SpriteRenderer>().enabled;
    }
    // -----------------------------
}
