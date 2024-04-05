using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_4 : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] GameObject eraser;
    [SerializeField] GameObject friend;

    // ---------- Animation -----------
    // ジャンプアニメーション後、Friendをドアガラスの中に表示
    private void PlayFriendLookAnima()
    {
        friend.GetComponent<Animator>().Play("FriendLookThroughTheGlass");
    }
    // -----------------------------------

}
