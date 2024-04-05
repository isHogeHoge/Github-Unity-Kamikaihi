using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player,Red(Blue)Enemyにアタッチ
public class ChangeLayerCnt : MonoBehaviour
{
    [SerializeField] GameObject blueEnemy_Goal; // 敵(青)ゴール後
    [SerializeField] GameObject player;

    private SpriteRenderer sr; // 自身のSpriteRenderer
    private SpriteRenderer sr_player; // PlayerのSpriteRenderer
    private ChangeLayerCnt clc_player; // PlayerのChangeLayerCnt
    internal bool isEntering = false; // 障害物と接触中はtrue

    private void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
        sr_player = player.GetComponent<SpriteRenderer>();
        clc_player = player.GetComponent<ChangeLayerCnt>();
    }

    private void Update()
    {
        if (this.gameObject == player)
        {
            return;
        }
        // Red(Blue)EnemyをPlayerの手前or奥に表示させる
        // (Player&Enemyが)両者とも障害物と接触しているorいないなら、レイヤーを変更する
        if (!clc_player.isEntering && !isEntering || clc_player.isEntering && isEntering)
        {
            // Playerより下側にいるなら、手前に表示
            if (this.transform.position.y < player.transform.position.y)
            {
                sr.sortingOrder = sr_player.sortingOrder + 1;
            }
            // 上側にいるなら、奥に表示
            else
            {
                sr.sortingOrder = sr_player.sortingOrder - 1;
            }
        }
    }
    // 障害物orゴール時の敵(青)のコライダーに入った時
    private void OnTriggerEnter2D(Collider2D col)
    {
        // 障害物orゴール時の敵(青)が手前に表示されるようにレイヤーを変更する
        // 障害物
        if (col.gameObject.CompareTag("Obstacle"))
        {
            ChangeLayer(col.gameObject, 6);
        }
        // ゴール時の敵(青)
        else if (col.gameObject.CompareTag("Goal"))
        {
            ChangeLayer(col.gameObject, 3);
        }
        else
        {
            return;
        }
        isEntering = true;

    }
    /// <summary>
    /// 自身のレイヤー変更
    /// </summary>
    /// <param name="col">障害物orゴール時の敵(青)</param>
    /// <param name="deltaSortingOrder">レイヤー変化分</param>
    private void ChangeLayer(GameObject col,int deltaSortingOrder)
    {
        // 自身が接触したオブジェクトより手前に表示されているなら
        if (sr.sortingOrder >= col.GetComponent<SpriteRenderer>().sortingOrder)
        {
            // 自身のレイヤーを下げ、奥に表示されるようにする
            sr.sortingOrder -= deltaSortingOrder;
        }
        
    }

    // 障害物コライダーから出た時
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Obstacle") || col.gameObject.CompareTag("Goal"))
        {
            isEntering = false;
            // Playerのみレイヤーをデフォルト(6)に戻す
            // isObstacleがfalseなら、Red(Blue)EnemyのレイヤーはPlayerのレイヤー±1される(Updateメソッド内処理)
            if (this.gameObject == player)
            {
                sr.sortingOrder = 6;
            }
            
        }

    }
}
