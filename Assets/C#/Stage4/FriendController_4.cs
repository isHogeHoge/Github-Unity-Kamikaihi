using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FriendController_4 : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject friend;
    [SerializeField] GameObject friendsHand;
    [SerializeField] GameObject candyOnTheGround;
    [SerializeField] GameObject door;

    // --------- Friend ---------
    // キャンディーを覗き見た後
    private void ActiveFriendsHand()
    {
        // キャンディーの方に手を伸ばす
        friend.GetComponent<SpriteRenderer>().enabled = false;
        friendsHand.GetComponent<SpriteRenderer>().enabled = true;
        friendsHand.GetComponent<Animator>().enabled = true;
    }
    // キャンディーを取得した後
    private void PlayGameClearAnima()
    {
        // ゲームクリアアニメーション再生
        player.GetComponent<Animator>().Play("PlayerClear");
    }
    // --------------------------

    // ------ Friend'sHand ------
    private void FriendGetACandy()
    {
        // キャンディーを取得する
        candyOnTheGround.GetComponent<SpriteRenderer>().enabled = false;
        friendsHand.GetComponent<SpriteRenderer>().enabled = false;
        friend.GetComponent<Animator>().Play("FriendGetACandy");
        friend.GetComponent<SpriteRenderer>().enabled = true;
    }
    // --------------------------
}
