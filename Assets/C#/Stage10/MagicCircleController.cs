using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircleController : MonoBehaviour
{
    [SerializeField] Animator ainmator_player;
    [SerializeField] GameObject friend;

    // 魔法陣出現後、Playerのアニメーション切り替え
    private void PlayPlayerHopeAnima()
    {
        ainmator_player.Play("PlayerHope");
    }

    // 魔法陣が光った後、Friend出現
    private void FriendAppear()
    {
        if (!friend.activeSelf)
        {
            friend.SetActive(true);
        }
    }

}
