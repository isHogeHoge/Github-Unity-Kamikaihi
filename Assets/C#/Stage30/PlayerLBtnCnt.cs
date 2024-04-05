using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class PlayerLBtnCnt : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject greenPanel; // (暗視スコープ装着中)画面を緑色にするためのパネル
    [SerializeField] GameObject redLasers;
    [SerializeField] GameObject greenLaser;
    [SerializeField] GameObject itemManager;
    [SerializeField] GameObject stageManager;
    // アイテム画像
    [SerializeField] Sprite nightScopeSpr;

    internal bool canClickPlayerL = true;

    // (暗視スコープ装備中に)自身をクリックした時、暗視スコープアイテムを取得できるようにする
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!canClickPlayerL)
        {
            return;
        }

        if (this.GetComponent<Animator>().GetBool("isWearing"))
        {
            itemManager.GetComponent<ItemManager>().ClickItemBtn(nightScopeSpr);
            // 暗視スコープ無しのアニメーションにに変更
            this.GetComponent<Animator>().SetBool("isWearing", false);

            // ----- 画面を明転させる -----
            greenPanel.GetComponent<Image>().enabled = false;
            // レーザー(緑)はアクティブなら表示、レーザー(赤)は非表示に
            if(stageManager.GetComponent<StageManager_30>().currentLaser == ActiveLaser.green)
            {
                greenLaser.GetComponent<SpriteRenderer>().enabled = true;
            }
            for(var i = 0; i < redLasers.transform.childCount; i++)
            {
                redLasers.transform.GetChild(i).GetComponent<Image>().enabled = false;
            }
            // レーザー(緑)を視認できるので接触判定をOFFに
            greenLaser.GetComponent<BoxCollider2D>().enabled = false;  
            // ---------------------------
        }
        
    }
}
