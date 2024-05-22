using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class FriendsAnimaCnt_24 : MonoBehaviour
{
    [SerializeField] BoxCollider2D boxCol_well;
    [SerializeField] GameObject player;
    [SerializeField] GameObject stageManager;

    // ++++ Friend1&2 ++++
    // 出現アニメーション終了時
    private void isPlayPlayerOverAnima()
    {
        // (カッパのマスクを着けていないなら)Playerゲームオーバーアニメーション再生
        if (!player.GetComponent<PlayerController_24>().wearingMask)
        {
            player.GetComponent<Animator>().SetBool("Over1Flag", true);
        }
    }
    // +++++++++++++++++++

    // ++++ Friend1 ++++
    // 出現アニメーション再生中
    private void InActivePlayersBoxCollider()
    {
        // Playerへのアイテム使用を禁止に
        player.GetComponent<BoxCollider2D>().enabled = false;

    }
    // 気絶するアニメーション終了時
    private void RestartPlayerMoving()
    {
        // Playerの移動再開
        stageManager.GetComponent<PlayersMovementCnt_24>().PlayerMove();
    }
    // +++++++++++++++++

    // ++++ Friend2 ++++
    // 出現後、Friend2へのアイテム使用禁止に
    private void InActiveBoxCol_Well()
    {
        boxCol_well.enabled = false;
    }
    // +++++++++++++++++
}
