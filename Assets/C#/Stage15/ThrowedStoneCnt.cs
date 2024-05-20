using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class ThrowedStoneCnt : MonoBehaviour
{
    [SerializeField] Animator animator_beehiveOnTheTree;

    // ------- Animation -------
    // beehiveに当たった後
    private void PlayBeehiveFallAnima()
    {
        // beehiveが落下するアニメーション再生
        animator_beehiveOnTheTree.Play("BeehiveFall");
    }
    // --------------------------

}
