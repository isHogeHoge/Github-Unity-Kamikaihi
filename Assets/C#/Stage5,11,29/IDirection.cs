using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IDirection
{
    // 向きに応じてPlayer停止アニメーション再生
    void PlayStopAnima(Animator animator);

    // 向きに応じてPlayer移動アニメーション再生
    void PlayMoveAnima(Animator animator);

    // 向きに応じてPlayerキックアニメーション再生
    void PlayKickAnima(Animator animator);

    /// <summary>
    /// player,slimeの移動先ポジション設定
    /// </summary>
    /// <param name="obj">移動させたいオブジェクト(player,slime)</param>
    /// <returns></returns>
    Vector3 SetTargetPos(GameObject obj);

    /// <summary>
    /// キックしたスライムの移動先に障害物(スライム,壁)がないか調べる
    /// </summary>
    /// <param name="col_slime">playerがキックしたスライム</param>
    /// <returns>移動先に障害物(スライム,壁)があればtrue,なければfalse</returns>
    bool ObstacleExistsToDistination(GameObject col_slime);

    /// <summary>
    /// Playerの進行方向1マス先にあるスライムがあるか調べる
    /// </summary>
    /// <returns>Playerの進行方向1マス先にスライムがあればそのオブジェクト、なければnull</returns>
    GameObject GetSlimeInFrontOfPlayer(GameObject player);
}
