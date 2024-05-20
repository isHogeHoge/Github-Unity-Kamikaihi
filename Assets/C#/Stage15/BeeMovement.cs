using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class BeeMovement : MonoBehaviour
{
    [SerializeField] Animator animator_bear;
    [SerializeField] GameObject player;
    [SerializeField] GameObject stageManager;
    [SerializeField] Vector2 targetPos; // 移動先座標

    private PlayerController_15 playerCnt_15;
    private RectTransform rect_bees;
    private bool isMoving = true;   

    void Start()
    {
        rect_bees = this.GetComponent<RectTransform>();
        playerCnt_15 = player.GetComponent<PlayerController_15>();
    }

    void Update()
    {
        // 移動中でないならメソッドを抜ける
        if (!isMoving)
        {
            return;
        }

        // 目的地点まで移動
        const float speed = 250f;
        rect_bees.anchoredPosition = Vector3.MoveTowards(rect_bees.anchoredPosition, targetPos, speed * Time.deltaTime);

        // 目的地点に到着した時
        if(rect_bees.anchoredPosition == targetPos)
        {
            // playerが蜂の巣に石を投げていたら
            if (playerCnt_15.throwedAStone)
            {
                // クマ出現アニメーションを再生
                animator_bear.enabled = true;
            }
            // playerが防護服を着ていたら
            else if (playerCnt_15.wearHazmatSuits)
            {
                // 自身(蜂)を非表示に
                this.GetComponent<Image>().enabled = false;
            }
            // playerが防具服を着ていなかったら
            else
            {
                // ゲームオーバ処理
                stageManager.GetComponent<StageManager>().GameOver(this.GetCancellationTokenOnDestroy()).Forget();
            }

            isMoving = false;
            
        }
    }
}
