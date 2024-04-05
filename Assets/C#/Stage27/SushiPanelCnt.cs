using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// sushiPanelCntオブジェクトにアタッチ(常にアクティブにするため)
public class SushiPanelCnt : MonoBehaviour
{
    [SerializeField] GameObject sushiImg;
    [SerializeField] GameObject eatBtn;
    [SerializeField] GameObject triosSushi;
    [SerializeField] GameObject stageManager;
    [SerializeField] GameObject sushiInFrontOfPlayer; // Playerの手前にある寿司(最初はえび寿司)

    private RotateFoodsCnt rfc;
    private Image img_sushiImg;

    void Start()
    {
        rfc = stageManager.GetComponent<RotateFoodsCnt>();
        img_sushiImg = sushiImg.GetComponent<Image>();
    }

    void Update()
    {
        // Playerの手前にある寿司を取得
        sushiInFrontOfPlayer = triosSushi.transform.GetChild(rfc.indexOfFoods[0]).gameObject;

        // Playerの手前に寿司があれば、その寿司の画像を吹き出しに設定
        // 「食べる」ボタンをアクティブに
        if (sushiInFrontOfPlayer.GetComponent<SpriteRenderer>().enabled)
        {
            img_sushiImg.sprite = sushiInFrontOfPlayer.GetComponent<SpriteRenderer>().sprite;
            eatBtn.SetActive(true);
        }
        // Playerの手前に寿司がなければ、吹き出しを空に
        // 「食べる」ボタンを非アクティブに
        else
        {
            img_sushiImg.sprite = null;
            eatBtn.SetActive(false);
        }
        
        

    }
}
