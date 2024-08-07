using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayersLifeCnt : MonoBehaviour
{
    [SerializeField] RectTransform rect_canvas;
    [SerializeField] RectTransform rect_playersLife;
    [SerializeField] GameObject player;


    private void Update()
    {
        // --- Playerの座標をRectTransform座標に変換する ---
        Vector2 playerPos_rect = Vector2.zero;
        // ワールド座標 → スクリーン座標
        Vector2 screenPos = Camera.main.WorldToScreenPoint(player.transform.position);
        // スクリーン座標 → RectTransform座標
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect_canvas, screenPos, Camera.main, out playerPos_rect);
        // -----------------------------------------------

        // Playerを追跡するようにPlayer'sLifeを配置
        float revisedValueY = 200f;  // Y座標のズレ補正用
        rect_playersLife.localPosition = new Vector3(playerPos_rect.x, playerPos_rect.y + revisedValueY, 0f);
    }

}
