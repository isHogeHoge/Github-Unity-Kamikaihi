using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class EnemiesAnimaCnt : MonoBehaviour
{
    [SerializeField] GameObject player;

    // 引き返すアニメーション開始時
    private void InActivePlayer()
    {
        // Playerを非表示に
        player.GetComponent<SpriteRenderer>().enabled = false;
    }
}
