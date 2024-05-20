using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using Random = UnityEngine.Random;

public class BeeUpAndDown : MonoBehaviour
{
    [SerializeField] string dir; // Beeの向いている方向

    private BeeUpAndDown thisScript;
    private RectTransform rect_bees;      
    private Vector3 startPos;        // 初期位置
    private Vector3 goalPos;         // ゴール座標
    private Vector3 targetPos_current;   // 現在の移動先ポジション
    private float moveSpeed;       // 移動スピード
    private float passedTime = 0f; // 経過時間

    private void Start()
    {
        thisScript = this.GetComponent<BeeUpAndDown>();
        rect_bees = this.GetComponent<RectTransform>();
        // 初期位置の代入
        startPos = rect_bees.anchoredPosition;
        // 移動スピードを200f~300fの間でランダムに設定
        moveSpeed = Random.Range(200f, 300f);

        // Beeの向いている方向に応じて、ゴール座標を設定
        float deltaPosY = 0f; 
        // 左
        if (dir == "LEFT")
        {
            // 移動先座標を現在のY座標 - 25fに設定
            deltaPosY = -25f;
        }
        // 右
        else if (dir == "RIGHT")
        {
            // 移動先座標を現在のY座標 + 25fに設定
            deltaPosY = 25f;
        }
        else
        {
            Debug.Log("無効な文字列です");
        }
        // 移動先座標の設定
        goalPos = new Vector3(rect_bees.anchoredPosition.x, rect_bees.anchoredPosition.y + deltaPosY, 0);
        targetPos_current = goalPos;
    }

    private void Update()
    {
        // ゲーム停止中orこのスクリプトが非アクティブならメソッドを抜ける
        if (Mathf.Approximately(Time.timeScale, 0f) || !thisScript.enabled)
        {
            return;
        }

        // Bee移動
        rect_bees.anchoredPosition = Vector3.MoveTowards(rect_bees.anchoredPosition, targetPos_current, moveSpeed * Time.deltaTime);

        // 経過時間に応じて、移動先座標を設定
        passedTime += Time.deltaTime;
        // 経過時間が0~1秒の間
        if (passedTime >= 0f && passedTime <= 1f)
        {
            // ゴール座標へ移動
            targetPos_current = goalPos;
            
        }
        // 経過時間が1秒~2秒の間
        else if(passedTime > 1f && passedTime <= 2f)
        {
            // 初期位置へ移動
            targetPos_current = startPos;
            
        }
        // 経過時間が2秒を超えたら
        else if(passedTime > 2f)
        {
            // 経過時間を0に
            passedTime = 0;
        }


    }
}
