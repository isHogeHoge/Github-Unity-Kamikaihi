using UnityEngine;
using UnityEngine.UI;

public class PresentController : MonoBehaviour
{
    [SerializeField] Animator animator_player;
    [SerializeField] Image img_playersHand;

    // アニメーション終了後
    // ゲームオーバー(クリア)アニメーション再生
    private void PlayGameEndAnima(string gameState) // Over,Clear
    {
        animator_player.Play($"Player{gameState}");
    }
    // Player'sHandを非表示に
    private void InActivePlayersHand()
    {
        img_playersHand.enabled = false;
    }

}
