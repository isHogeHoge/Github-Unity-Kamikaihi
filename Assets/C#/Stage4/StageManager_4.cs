using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class StageManager_4 : MonoBehaviour
{
    [SerializeField] GameObject player;            
    [SerializeField] GameObject friend;           
    [SerializeField] GameObject teacher;          

    private StageManager sm;

    private void Start()
    {
        sm = this.GetComponent<StageManager>();
    }

    // ------------ ボタン ------------
    // 黒板消し
    public void ClickEraserBtn()
    {
        GameOver("PlayerJump");
    }
    // 教室のドア
    public void ClickDoorBtn()
    {
        GameOver("PlayerOpenTheDoor");
        friend.GetComponent<Animator>().Play("FriendTeasePlayer");
    }
    // 消火栓の警報器
    public void ClickAlarmBtn()
    {
        GameOver("PlayerIsSurprised");
        teacher.SetActive(true);
    }
    // --------------------------------

    /// <summary>
    /// ゲームオーバー処理
    /// </summary>
    /// <param name="animation">Playerのアニメーション名</param>
    private void GameOver(string animation)
    {
        // ゲーム操作を禁止に
        sm.CantGameControl();

        // Playerのアニメーション切り替え(ゲームオーバー)
        Animator animator = player.GetComponent<Animator>();
        animator.Play($"{animation}");
    }

}
