using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.EventSystems;

public class StageManager_8 : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject friend1;
    [SerializeField] GameObject friend2;
    [SerializeField] GameObject creamPuffWithLoupeImg; // 吹き出し内のシュークリーム&ルーペ画像
    [SerializeField] GameObject collider_LoupeItem; 
    [SerializeField] Sprite spicyCreamPuffSpr;     // からし入りのシュークリーム画像
    [SerializeField] Sprite sweetCreamPuffSpr;    // からし無しのシュークリーム画像

    private Collider_LoupeItemCnt clc;
    private RotateFoodsCnt rfc;
    private void Start()
    {
        clc = collider_LoupeItem.GetComponent<Collider_LoupeItemCnt>();
        rfc = this.GetComponent<RotateFoodsCnt>();
    }

    // 食べ物回転ボタンを離した時、吹き出しに表示する画像を設定
    public void ClickRotateBtn_UP()
    {
        // ルーペを使用していなかったら、メソッドを抜ける
        if(!clc.usedLoupe)
        {
            return;
        }

        // Playerの手前にあるシュークリームの画像を吹き出しに設定
        // からし入り
        if (this.GetComponent<RotateFoodsCnt>().indexOfFoods[0] == clc.indexOfSpicyCf)
        {
            creamPuffWithLoupeImg.GetComponent<Image>().sprite = spicyCreamPuffSpr;
        }
        // からし無し
        else
        {
            creamPuffWithLoupeImg.GetComponent<Image>().sprite = sweetCreamPuffSpr;
        }
    }

    // 「食べる」ボタンをクリックした時
    public void ClickEatBtn()
    {
        // ルーペを使用している & Playerの手前にからし入りのシュークリームがないなら、ゲームクリア
        if (clc.usedLoupe && rfc.indexOfFoods[0] != clc.indexOfSpicyCf)
        {
            player.GetComponent<Animator>().SetBool("ClearFlag", true);

            // からし入りのシュークリームがfriend1の手前にある時、Friend1ゲームオーバー
            if (rfc.indexOfFoods[1] == clc.indexOfSpicyCf)
            {
                friend1.GetComponent<Animator>().SetBool("OverFlag", true);
                friend2.GetComponent<Animator>().SetBool("ClearFlag", true);
            }
            // からし入りのシュークリームがfriend2の手前にある時、Fried2ゲームオーバー
            else if (rfc.indexOfFoods[4] == clc.indexOfSpicyCf)
            {
                friend1.GetComponent<Animator>().SetBool("ClearFlag", true);
                friend2.GetComponent<Animator>().SetBool("OverFlag", true);
            }

        }
        // ルーペを使用していない or 手前にからし入りシュークリームがあるなら、ゲームオーバー
        else
        {
            player.GetComponent<Animator>().SetBool("OverFlag", true);
            // Friend1&2はゲームクリア
            friend1.GetComponent<Animator>().SetBool("ClearFlag", true);
            friend2.GetComponent<Animator>().SetBool("ClearFlag", true);

        }
    }

    

}
