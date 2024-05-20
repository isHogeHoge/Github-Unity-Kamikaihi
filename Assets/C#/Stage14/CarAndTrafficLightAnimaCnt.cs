using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class CarAndTrafficLightAnimaCnt : MonoBehaviour
{
    [SerializeField] Animator animator_car;
    [SerializeField] Animator animator_player;

    private static bool isRedLight = false; // 赤信号

    // ゲーム終了時、赤信号フラグをOFFに
    private void OnDestroy()
    {
        isRedLight = false;
    }

    // ------ TrafficLight ------
    // 赤信号に切り替わった後、赤信号フラグをONに
    private void TurnRed()
    {
        isRedLight = true;
    }
    // -------------------------


    // --------- Car -----------
    // "CarMove2"アニメーション再生中
    private void isPlayCarStopAnima()
    {
        // 赤信号なら、車が止まるアニメーション再生
        if (isRedLight)
        {
            // 現在のアニメーションを一度終了
            animator_car.enabled = false;
            animator_car.enabled = true;
            animator_car.Play("CarStop");

            animator_player.Play("PlayerStop");

        }
    }
    // "CarMove2"アニメーション終了後(Playerと衝突時)
    // ゲームオーバーアニメーション再生
    private void PlayPlayerOverAnima()
    {
        animator_player.Play("PlayerOver");
    }

    // 車が止まった後、ゲームクリアアニメーション再生
    private void PlayPlayerClearAnima()
    {
        animator_player.Play("PlayerClear");
    }
    // ---------------------------
    
}
