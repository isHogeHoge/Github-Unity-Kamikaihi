using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class PlayerLBtnCnt : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image img_greenPanel; // 暗視スコープ装着中パネル
    [SerializeField] GameObject redLasers;
    [SerializeField] SpriteRenderer sr_greenLaser;
    [SerializeField] BoxCollider2D boxCol_greenLaser;
    [SerializeField] GameObject itemManager;
    [SerializeField] GameObject stageManager;
    // アイテム画像
    [SerializeField] Sprite nightScopeSpr;

    private ItemManager im;
    private StageManager_30 sm_30;
    private Animator animator_playerL;
    internal bool canClickPlayerL = true;
    private void Start()
    {
        im = itemManager.GetComponent<ItemManager>();
        sm_30 = stageManager.GetComponent<StageManager_30>();
        animator_playerL = this.GetComponent<Animator>();
    }

    // (暗視スコープ装備中に)自身をクリックした時、暗視スコープアイテムを取得できるようにする
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!canClickPlayerL) return;

        if (animator_playerL.GetBool("isWearing"))
        {
            im.ClickItemBtn(nightScopeSpr);
            // 暗視スコープ無しのアニメーションにに変更
            animator_playerL.SetBool("isWearing", false);

            // ----- 画面を明転させる -----
            img_greenPanel.enabled = false;
            // レーザー(緑)はアクティブなら表示、レーザー(赤)は非表示に
            if(sm_30.currentLaser == ActiveLaser.green)
            {
                sr_greenLaser.enabled = true;
            }
            for(var i = 0; i < redLasers.transform.childCount; i++)
            {
                redLasers.transform.GetChild(i).GetComponent<Image>().enabled = false;
            }
            // レーザー(緑)を視認できるので接触判定をOFFに
            boxCol_greenLaser.enabled = false;  
            // ---------------------------
        }
        
    }
}
