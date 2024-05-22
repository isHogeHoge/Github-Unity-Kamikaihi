using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatyasCapsulesAnimaCnt : MonoBehaviour
{
    [SerializeField] Animator animator_clerk;

    // アニメーション終了後
    private void PlayClerkIsSurprisedAnima()
    {
        // Clerkが驚くアニメーション再生
        animator_clerk.Play("ClerkIsSurprised");
    }

}
