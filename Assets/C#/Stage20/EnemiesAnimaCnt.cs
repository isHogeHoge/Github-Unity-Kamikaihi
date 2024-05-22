using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class EnemiesAnimaCnt : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr_player;

    // 引き返すアニメーション開始時
    private void InActivePlayer()
    {
        sr_player.enabled = false;
    }
}
