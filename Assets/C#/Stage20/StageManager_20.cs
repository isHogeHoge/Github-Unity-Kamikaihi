using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageManager_20 : MonoBehaviour
{
    [SerializeField] GameObject stagePanel_UI;
    [SerializeField] GameObject stagePanel;
    [SerializeField] GameObject itemManger;

    void Start()
    {
        // ステージ初期位置から右に1ページ分だけ移動できるように設定
        stagePanel_UI.GetComponent<StageScrollCnt>().maxCountR = 1;
        stagePanel.GetComponent<StageScrollCnt>().maxCountR = 1;
    }

    // ---------- Button -----------
    // 弟
    public void ClickBrotherBtn(GameObject brother)
    {
        Animator animator = brother.GetComponent<Animator>();
        // クリックする度に"BrohterDrink"アニメーションが再生されるのを防ぐ
        if(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "BrotherDrink")
        {
            return;
        }
        animator.Play("BrotherDrink");
    }
    // クッキー
    public void ClickCookieBtn(GameObject cookieBtn)
    {
        // 自身をアイテムをして取得する
        Sprite cookieSpr = cookieBtn.GetComponent<Image>().sprite;
        itemManger.GetComponent<ItemManager>().ClickItemBtn(cookieSpr);
    }
    // --------------------------------
}
