using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapAnimaCnt : MonoBehaviour
{
    [SerializeField] Animator animator_hotBath;
    [SerializeField] SpriteRenderer sr_signBoard;

    // +++++ Needle'sTrap +++++
    // 針が引っ込むアニメーション終了時
    private void ActiveHotBathTrap()
    {
        // お湯風呂トラップに切り替わる
        animator_hotBath.Play("HotBathActive");
        sr_signBoard.enabled = true;
    }
    // ++++++++++++++++++++++++
}
