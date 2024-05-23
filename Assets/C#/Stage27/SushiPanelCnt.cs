using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// sushiPanelCntオブジェクトにアタッチ(常にアクティブにするため)
public class SushiPanelCnt : MonoBehaviour
{
    [SerializeField] Image sushiImg;
    [SerializeField] GameObject eatBtn;
    [SerializeField] GameObject triosSushi;
    [SerializeField] SpriteRenderer sr_sushiInFrontOfPlayer; // Playerの手前にある寿司(Startはえび寿司)
    [SerializeField] GameObject stageManager;

    private RotateFoodsCnt rfc;

    void Start()
    {
        rfc = stageManager.GetComponent<RotateFoodsCnt>();
    }

    void Update()
    {
        // Playerの手前にある寿司を取得
        sr_sushiInFrontOfPlayer = triosSushi.transform.GetChild(rfc.indexOfFoods[0]).GetComponent<SpriteRenderer>();

        // Playerの手前に寿司があれば、その寿司の画像を吹き出しに設定
        // 「食べる」ボタンをアクティブに
        if (sr_sushiInFrontOfPlayer.enabled)
        {
            sushiImg.sprite = sr_sushiInFrontOfPlayer.sprite;
            eatBtn.SetActive(true);
        }
        // Playerの手前に寿司がなければ、吹き出しを空に
        // 「食べる」ボタンを非アクティブに
        else
        {
            sushiImg.sprite = null;
            eatBtn.SetActive(false);
        }
        
        

    }
}
