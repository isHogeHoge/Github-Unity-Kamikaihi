using UnityEngine;

// 障害物オブジェクト全てにアタッチされる
public class ObstacleCnt : MonoBehaviour
{
    [SerializeField] RectTransform rect_canvas;
    [SerializeField] RectTransform rect_stagePanel;
    [SerializeField] RectTransform rect_playersLife;
    [SerializeField] GameObject score_parent; // スコアPrefabの代入先(親オブジェクト)
    [SerializeField] GameObject effect_parent;// エフェクトPrefabの代入先(親オブジェクト)
    [SerializeField] RectTransform rect_LmoveBtn;
    [SerializeField] GameObject player;
    [SerializeField] GameObject stageManager;
    [SerializeField] GameObject prefab_1Points;
    [SerializeField] GameObject prefab_5Points;
    [SerializeField] GameObject prefab_10Points;
    [SerializeField] GameObject prefab_135Points;
    [SerializeField] GameObject prefab_effect;

    private StageManager_31 sm_31;
    private RectTransform rect_obstacle;
    private int score = 0;
    private bool wasAdded_lowScore = false;   // ロースコア加算済みフラグ
    private bool wasAdded_highScore = false; // ハイスコア加算済みフラグ

    private void Start()
    {
        sm_31 = stageManager.GetComponent<StageManager_31>();
        rect_obstacle = this.GetComponent<RectTransform>();
    }

    // 自身が移動ボタンより下に移動したら、(ロー)スコアを加算する
    private void Update()
    {
        // スコア加算済みorゲーム中でないならメソッドを抜ける
        if (wasAdded_lowScore || sm_31.gameState != GameState.playing)
        {
            return;
        }

        // 移動ボタン&自身の上辺Y座標を取得
        float moveBtn_Y = rect_LmoveBtn.anchoredPosition.y;
        float moveBtn_TopY = moveBtn_Y + (rect_LmoveBtn.sizeDelta.y / 2);
        float this_TopY = CalculateThisTopY();

        // 自身が移動ボタンより下に移動したら
        if (this_TopY < moveBtn_TopY)
        {
            // Playerとの距離に応じて、スコアを加算&表示する
            float playerPosX = rect_playersLife.anchoredPosition.x;
            float thisPosX = rect_obstacle.anchoredPosition.x;
            float distanceX_1points = 400f; // 1点となる距離の基準値
            float distanceX_5points = 150f; // 5点となる距離の基準値
            GameObject lowScore_Prefab = null; // 表示するスコアPrefab
            // 1点
            if (Mathf.Abs(playerPosX - thisPosX) >= distanceX_1points)
            {
                lowScore_Prefab = prefab_1Points;
                score += 1;
            }
            // 5点
            else if(Mathf.Abs(playerPosX - thisPosX) >= distanceX_5points)
            {
                lowScore_Prefab = prefab_5Points;
                score += 5;
            }
            // 10点
            else
            {
                lowScore_Prefab = prefab_10Points;
                score += 10;
                
            }

            // 自身の真上にスコアエフェクトを表示
            GenerateScoreObj(lowScore_Prefab);

            // スコアを加算
            sm_31.AddScore(score);

            wasAdded_lowScore = true;
        }
    }
    // 自身の上辺Y座標を算出するメソッド
    private float CalculateThisTopY()
    {
        float stagePanel_Y = rect_stagePanel.anchoredPosition.y;
        float this_Y = rect_obstacle.anchoredPosition.y;
        float this_TopY = stagePanel_Y + this_Y + (rect_obstacle.sizeDelta.y / 2); // StagePanel(親オブジェクト)の移動分も含める
        return this_TopY;
    }

    // スコアエフェクト生成処理
    private void GenerateScoreObj(GameObject ScorePrefab)
    {
        // 座標を設定
        Vector3 scorePos = new Vector3(rect_obstacle.anchoredPosition.x, CalculateThisTopY(), 0f);

        // score_parentに生成したスコアエフェクトを代入する
        GameObject scoreObj = Instantiate(ScorePrefab, scorePos, Quaternion.identity);
        scoreObj.transform.SetParent(score_parent.transform, false);
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        // Player'sLifeの内部コライダーと接触時、ゲームオーバー
        if (col.tag == "Dead")
        {
            sm_31.GameOver();
        }

    }
    private void OnTriggerExit2D(Collider2D col)
    {
        // ゲームオーバー時orスコア加算済みならメソッドを抜ける
        if (col.tag == "Dead" || wasAdded_highScore)
        {
            return;
        }

        // Player'sLifeと表面接触時、ハイスコア加点(+135)
        GenerateScoreObj(prefab_135Points);
        score += 135;
        wasAdded_highScore = true;

    }
    // Player'sLifeと接触した場所にエフェクトを生成する
    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.tag != "Player")
        {
            return;
        }
        // 衝突位置を取得(ワールド座標)
        Vector2 hitPos = col.ClosestPoint(this.transform.position);
        // --- 衝突位置(ワールド座標)をRectTransform座標に変換する ---
        Vector2 hitPos_rect = Vector2.zero;
        // ワールド座標 → スクリーン座標
        Vector2 screenPos = Camera.main.WorldToScreenPoint(hitPos);
        // スクリーン座標 → RectTransform座標
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect_canvas, screenPos, Camera.main, out hitPos_rect);
        // -----------------------------------------------

        // 衝突位置にエフェクトを生成
        Vector3 effectPos = new Vector3(hitPos_rect.x, hitPos_rect.y, 0f);
        GameObject effect_Obj = Instantiate(prefab_effect, effectPos, Quaternion.identity);
        effect_Obj.transform.SetParent(effect_parent.transform, false);

        
    }

    
}
