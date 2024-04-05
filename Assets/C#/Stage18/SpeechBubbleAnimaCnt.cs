using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class SpeechBubbleAnimaCnt : MonoBehaviour
{
    [SerializeField] GameObject trio;  // player,friend1,friend2の親オブジェクト
    [SerializeField] GameObject player;
    [SerializeField] GameObject friend1;
    [SerializeField] GameObject friend2;
    [SerializeField] GameObject monk;
    [SerializeField] GameObject btn_TriosSB; // player,friend1,friend2の吹き出し消去ボタン
    [SerializeField] GameObject stageManager;


    // ---- player,friend1,friend2の吹き出し ----
    // 吹き出し(下)アニメーション開始時、
    // "isStart"フラグを初期状態にリセットし、タップされるまで吹き出しが表示されるようにする
    private void ResetIsStartFlg()
    {
        Debug.Log("ok");
        for(var i = 0; i < this.transform.parent.childCount; i++)
        {
            this.transform.parent.GetChild(i).GetComponent<Animator>().SetBool("isStart", false);
        }
    }
    // 吹き出し(下)出現後
    // 吹き出し上部分と吹き出し内のアニメーションを再生
    private void ActiveThisSB_UpAndImg()
    {
        
        for (var i = 1; i < 3; i++)
        {
            string animation = ""; // アニメーション名
            switch (i)
            {
                // iが1なら吹き出し(上)のアニメーション再生
                case 1:
                    animation = "Trio'sSB_Up";
                    break;
                // iが2なら吹き出し内のアニメーションを再生
                case 2:
                    animation = "Trio'sSB_Img";
                    break;

            }
            this.transform.parent.GetChild(i).GetComponent<Animator>().Play($"{animation}",0,0);
            this.transform.parent.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
        }

    }

    // 吹き出し(上)出現中
    // 出現した吹き出しをクリックで消去可能に
    private void ActiveThisSpeechBubbleBtn()
    {
        // 自身と同じタグの吹き出し消去ボタンをアクティブに
        for(var i = 0; i < btn_TriosSB.transform.childCount; i++)
        {
            GameObject sb1Btn = btn_TriosSB.transform.GetChild(i).gameObject;
            if (this.transform.parent.tag == sb1Btn.tag)
            {
                sb1Btn.GetComponent<Image>().enabled = true;
            }

        }
    }
    // 吹き出し(上)が一定時間表示されたら、Monkを出現元のX座標まで移動させる(ゲームオーバー)
    private void MonkMoveToThisParentPosX()
    {
        if (this.transform.GetComponent<SpriteRenderer>().enabled)
        {
            // ゲーム操作を禁止に
            stageManager.GetComponent<StageManager_18>().gameState = GameState.gameOver;
            stageManager.GetComponent<StageManager>().CantGameControl();
            // 吹き出しの出現を停止
            stageManager.GetComponent<StageManager_18>().InActiveSpeechBubble();

            // Monkを吹き出し出現元(Player,Friend1,Friend2)のX座標まで移動させる
            Transform parent = this.transform.parent;
            monk.GetComponent<MonkController>().targetPos = new Vector3(parent.position.x, monk.transform.position.y, 0);
            // Monkの移動速度の変更
            monk.GetComponent<MonkController>().moveSpeed = 5f;
            

        }
    }
    // ----------------------------------------

    // ------------ Monkの吹き出し ------------
    // 吹き出し(下)出現後
    // 吹き出し(上)のアニメーション再生
    private void ActiveMonksSB_Up()
    {
        monk.transform.GetChild(1).GetComponent<Animator>().Play("Monk'sSB_Up",0,0);
        monk.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
    }

    // 吹き出し(上)出現後
    // ゲームクリア時以外なら、吹き出しを初期状態に
    private void isInActiveMonksSB()
    {
        if (stageManager.GetComponent<StageManager_18>().gameState == GameState.gameClear)
        {
            return;
        }

        for (var i = 0; i < monk.transform.childCount; i++)
        {
            monk.transform.GetChild(i).GetComponent<Animator>().SetBool("isStart", true);
            monk.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = false;
        }
        
    }
    // ---------------------------------------
    
}
