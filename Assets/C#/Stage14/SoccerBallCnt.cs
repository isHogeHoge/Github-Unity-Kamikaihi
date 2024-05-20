using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public class SoccerBallCnt : MonoBehaviour
{
    [SerializeField] GameObject stageManager;
    [SerializeField] Vector3 endPos;

    private RectTransform rect;        
    private bool isMoving = true;

    private async void Start()
    {
        rect = this.GetComponent<RectTransform>();

        // オープニング動画再生中は移動させない
        await stageManager.GetComponent<StageManager>().WaitForOpeningVideo(this.GetCancellationTokenOnDestroy());
    }

    private void Update()
    {
        // ポーズ中ならUpdateを抜ける
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        if (isMoving)
        {
            // endPosまで(一定のスピードで)移動
            const float speed = 500f;   
            rect.anchoredPosition = Vector3.MoveTowards(rect.anchoredPosition, endPos, speed * Time.deltaTime);
            if(rect.anchoredPosition.x == endPos.x)
            {
                isMoving = false;
            }
        }
    }
}
