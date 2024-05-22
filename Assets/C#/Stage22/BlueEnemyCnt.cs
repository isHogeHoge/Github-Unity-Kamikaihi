using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Numerics;
using UnityEngine;

public class BlueEnemyCnt : MonoBehaviour
{
    [SerializeField] GameObject redEnemysGoal; // 敵(赤)のゴール
    [SerializeField] GameObject blueEnemy_Goal;
    [SerializeField] GameObject blueEnemy;

    // 地面の青いサークルに接触した時、移動ストップ
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.CompareTag("BlueGoal"))
        {
            // 敵(赤)もゴール可能に
            redEnemysGoal.SetActive(true);

            // ゴール時のオブジェクトに切り替え
            this.transform.gameObject.SetActive(false);
            blueEnemy_Goal.SetActive(true);

        }
    }

    // 出現アニメーション終了時、実際にPlayerを追随する敵(青)を出現
    private void AppearBlueEnemy_Playing()
    {
        blueEnemy.SetActive(true);
    }
}
