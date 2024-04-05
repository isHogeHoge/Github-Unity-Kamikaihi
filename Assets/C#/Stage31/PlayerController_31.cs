using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerController_31 : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject stagePanel;
    [SerializeField] GameObject tutorialText_U;
    [SerializeField] GameObject tutorialText_D;
    [SerializeField] GameObject playersLife;
    [SerializeField] GameObject stageManager;

    private StageManager_31 sm_31;
    private float minX = -2.24f; // 移動範囲の最小値(X座標)
    private float maxX = 2.34f;  // 移動範囲の最大値(X座標)
    private Vector3 direction;   // Playerの移動方向
    private bool isMoving = false;

    private void Start()
    {
        sm_31 = stageManager.GetComponent<StageManager_31>();
    }

    private void Update()
    {
        // ゲーム中でないならUpdateを抜ける
        if (Mathf.Approximately(Time.timeScale, 0f) || sm_31.gameState != GameState.playing)
        {
            return;
        }

        if (isMoving)
        {
            float speed = 1f;
            this.transform.position += speed * direction * Time.deltaTime;

            // x軸方向の移動範囲制限
            Vector3 playerPos = transform.position;
            playerPos.x = Mathf.Clamp(playerPos.x, minX, maxX);
            this.transform.position = playerPos;
        }
    }

    // ----- 移動ボタン -----
    // クリックした時
    public void ClickMoveBtn_Down(string dir)
    {
        // チュートリアル終了後ならゲーム開始
        if (!stagePanel.activeSelf)
        {
            tutorialText_U.GetComponent<TextMeshProUGUI>().enabled = false;
            tutorialText_D.GetComponent<TextMeshProUGUI>().enabled = false;
            stagePanel.SetActive(true);
        }

        switch (dir)
        {
            // 右
            case "RIGHT":
                this.GetComponent<Animator>().Play("PlayerMove_TopRight");
                direction = new Vector3(1, 0, 0);
                isMoving = true;
                break;
            // 左
            case "LEFT":
                this.GetComponent<Animator>().Play("PlayerMove_TopLeft");
                direction = new Vector3(-1, 0, 0);
                isMoving = true;
                break;
            default:
                Debug.Log($"{dir}は無効な文字列です");
                break;
        }
        
    }
    // 離した時
    public void ClickMoveBtn_Up()
    {
        this.GetComponent<Animator>().Play("PlayerMove_Up");
        isMoving = false;
    }
    // -------------------
}
