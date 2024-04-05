using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class ThrowedStoneCnt : MonoBehaviour
{
    [SerializeField] GameObject beehiveOnTheTree;

    // ------- Animation -------
    // beehiveに当たった後
    private void PlayBeehiveFallAnima()
    {
        // beehiveが落下するアニメーション再生
        beehiveOnTheTree.GetComponent<Animator>().Play("BeehiveFall");
    }
    // --------------------------

}
