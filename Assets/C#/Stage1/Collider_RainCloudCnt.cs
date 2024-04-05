using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;

public class Collider_RainCloudCnt : MonoBehaviour
{
    [SerializeField] GameObject stageManager;  
    [SerializeField] GameObject rainbow;       
    [SerializeField] GameObject player;        
    [SerializeField] Sprite gameClearSpr;      
    [SerializeField] int stageId;             

    private StageManager sm;
    private Color color;         // rainbowのカラー
    private float fadeTime = 0;  // rainbow画像の初期アルファ値
    private bool fadeInFlag = false;     // フェードインフラグ

    void Start()
    {
        sm = stageManager.GetComponent<StageManager>();
        color = rainbow.GetComponent<SpriteRenderer>().color;
    }

    void Update()
    {
        //rainbowの透明値を上げる(フェードイン)
        if (fadeInFlag)
        {
            fadeTime += Time.deltaTime; 
            color.a = fadeTime;
            // もし透明値が1以上になったら、フェードイン終了 → クリア処理
            if (fadeTime >= 1.0f)
            {
                StageClear(stageId, this.GetCancellationTokenOnDestroy()).Forget();
                fadeInFlag = false;
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        // RainCloudが通過したら、rainbowフェードイン
        if(col.tag == "RainCloud")
        {
            rainbow.SetActive(true);
            fadeInFlag = true;
            
        }
    }
    // クリア処理
    private async UniTask StageClear(int id,CancellationToken ct)
    {
        await UniTask.Delay(1000,cancellationToken: ct);
        player.GetComponent<SpriteRenderer>().sprite = gameClearSpr;
        await sm.GameClear(id,ct);
    }

}
