using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FriendController_4 : MonoBehaviour
{
    [SerializeField] Animator animator_player;
    [SerializeField] SpriteRenderer sr_friend;
    [SerializeField] Animator animator_friend;
    [SerializeField] SpriteRenderer sr_friendsHand;
    [SerializeField] Animator animator_friendsHand;
    [SerializeField] SpriteRenderer sr_candyOnTheGround;

    // --------- Friend ---------
    // キャンディーを覗き見た後
    private void ActiveFriendsHand()
    {
        // キャンディーの方に手を伸ばす
        sr_friend.enabled = false;
        sr_friendsHand.enabled = true;
        animator_friendsHand.enabled = true;
    }
    // キャンディーを取得した後
    private void PlayGameClearAnima()
    {
        // ゲームクリアアニメーション再生
        animator_player.Play("PlayerClear");
    }
    // --------------------------

    // ------ Friend'sHand ------
    private void FriendGetACandy()
    {
        // キャンディーを取得する
        sr_candyOnTheGround.enabled = false;
        sr_friendsHand.enabled = false;
        animator_friend.Play("FriendGetACandy");
        sr_friend.enabled = true;
    }
    // --------------------------
}
