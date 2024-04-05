using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager_26 : MonoBehaviour
{
    [SerializeField] GameObject itemManager;
    // アイテム画像
    [SerializeField] Sprite helmetSpr;    
    [SerializeField] Sprite watermelonSpr;

    private int count_gottenWatermelonR = 0; // アイテムとして取得したスイカの数
    private int count_totalWatermelonR = 6;   // 屋台に並んでいるスイカの数

    void Start()
    {
        // ステージ初期位置から右に1ページ分だけ移動できるように設定
        this.GetComponent<StageScrollCnt>().maxCountR = 1;
    }

    // --------- Button ---------
    // 屋台に並んでいるスイカ
    public void ClickWatermelonBtnR()
    {
        // アイテム所持数がMax(5)なら、取得したスイカの数をカウントアップしない
        if (!itemManager.GetComponent<ItemManager>().isFull)
        {
            count_gottenWatermelonR++;
        }

        // 最後にクリックしたスイカはヘルメットアイテムに変更
        if(count_gottenWatermelonR == count_totalWatermelonR)
        {
            itemManager.GetComponent<ItemManager>().ClickItemBtn(helmetSpr); // アイテム取得処理
            return;
        }
        // それ以外はスイカアイテムに
        itemManager.GetComponent<ItemManager>().ClickItemBtn(watermelonSpr);　// アイテム取得処理
    }
    // ----------------------------
}
