using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StageManager_13 : MonoBehaviour
{
    [SerializeField] Animator animator_present;
    [SerializeField] Animator animator_arrow;  // 選択カーソル
    [SerializeField] GameObject upBtn;

    private StageScrollCnt scrollCnt;
    private GameObject clickedBtn;  // クリックした十字キー(UpButton,DownButton)

    private void Start()
    {
        // クリックしている十字キーをUpButtonに設定
        clickedBtn = upBtn;

        // ステージ初期位置から右・左に1ページ分だけ移動できるように設定
        scrollCnt = this.GetComponent<StageScrollCnt>();
        scrollCnt.maxCountL = -1;
        scrollCnt.maxCountR = 1;
    }

    // 十字キー
    public void ClickArrowKeys(string dir)
    {
        clickedBtn = EventSystem.current.currentSelectedGameObject;
        // 選択カーソルをクリックした方向へ動かす
        animator_arrow.Play($"Arrow_{dir}");
    }
    // 決定ボタン
    public void ClickSelectBtn()
    {
        // 最後十字キー(上)を押していたら
        //「あける」を選択しているなら
        // ゲームオーバーアニメーション再生
        if(clickedBtn == upBtn)
        {
            animator_present.GetComponent<Animator>().Play("PresentMove_Over");
        }
        // 最後十字キー(下)を押していたら
        //「かいひ」を選択しているなら
        // ゲームクリアアニメーション再生
        else
        {
            animator_present.GetComponent<Animator>().Play("PresentMove_Clear");
        }
    }

    

}
