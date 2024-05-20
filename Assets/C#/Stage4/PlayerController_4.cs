using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_4 : MonoBehaviour
{
    [SerializeField] Animator animator_friend;

    // ---------- Animation -----------
    // ジャンプアニメーション後、Friendをドアガラスの中に表示
    private void PlayFriendLookAnima()
    {
        animator_friend.Play("FriendLookThroughTheGlass");
    }
    // -----------------------------------

}
