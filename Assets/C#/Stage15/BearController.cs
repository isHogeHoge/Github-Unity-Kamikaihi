using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearController : MonoBehaviour
{
    [SerializeField] Animator animator_player;

    // -------- Animation --------
    // 出現後、ゲームオーバーアニメーション再生
    private void PlayPlayerOverAnima()
    {
        animator_player.SetBool("OverFlag", true);
    }
    // ---------------------------
}
