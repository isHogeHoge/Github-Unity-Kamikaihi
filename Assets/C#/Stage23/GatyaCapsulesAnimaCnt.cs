using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatyasCapsulesAnimaCnt : MonoBehaviour
{
    [SerializeField] GameObject clerk;

    // アニメーション終了後
    private void PlayClerkIsSurprisedAnima()
    {
        // Clerkが驚くアニメーション再生
        clerk.GetComponent<Animator>().Play("ClerkIsSurprised");
    }

}
