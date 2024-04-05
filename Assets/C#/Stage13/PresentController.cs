using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PresentController : MonoBehaviour
{
    [SerializeField] GameObject playersHand;
    [SerializeField] GameObject player;

    // アニメーション終了後
    // ゲームオーバー(クリア)アニメーション再生
    private void PlayGameEndAnima(string gameState) // Over,Clear
    {
        player.GetComponent<Animator>().Play($"Player{gameState}");
    }
    // Player'sHandを非表示に
    private void InActivePlayersHand()
    {
        playersHand.GetComponent<Image>().enabled = false;
    }

}
