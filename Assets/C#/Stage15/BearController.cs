using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearController : MonoBehaviour
{
    [SerializeField] GameObject player;

    // -------- Animation --------
    // 出現後、ゲームオーバーアニメーション再生
    private void PlayPlayerOverAnima()
    {
        player.GetComponent<Animator>().SetBool("OverFlag", true);
    }
    // ---------------------------
}
