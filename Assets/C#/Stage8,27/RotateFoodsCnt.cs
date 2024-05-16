using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;

public class RotateFoodsCnt : MonoBehaviour
{
    [SerializeField] GameObject player;       
    [SerializeField] GameObject friend1;      
    [SerializeField] GameObject friend2;      
    [SerializeField] GameObject foodsPnl;    // 食べ物の吹き出しパネル
    [SerializeField] GameObject foods;      // 食べ物の親オブジェクト
    [SerializeField] GameObject rotateBtnL; // 食べ物を時計回りに回転させるボタン 
    [SerializeField] GameObject rotateBtnR; // 食べ物を反時計回りに回転させるボタン
    [SerializeField] GameObject playersFood;
    [SerializeField] GameObject friend1sFood;
    [SerializeField] GameObject friend2sFood;

    private GameObject clickedRotateBtn;      
    private List<GameObject> trios;         // player,friend1,friend2のリスト
    private List<GameObject> triosFoods;    // player,friend1,friend2が手に取る食べ物のリスト
    private List<Vector3> endPos_foods;   // 食べ物の移動先座標
    internal List<int> indexOfFoods;       // 食べ物に番号を振りリストに代入する.Playerの手前の料理を0とし、時計回りに数えていく
    private const float moveSpeed = 2;　   // 移動(回転)スピード
    private bool isRotating;               // 回転中フラグ

    private void Start()
    {
        trios = new List<GameObject> { player, friend1, friend2 };
        triosFoods = new List<GameObject> { playersFood, friend1sFood, friend2sFood };

        // 食べ物の座標を代入
        endPos_foods = new List<Vector3>();
        for (var i = 0; i < foods.transform.childCount; i++)
        {
            GameObject food = foods.transform.GetChild(i).gameObject;
            endPos_foods.Add(food.transform.position);
        }

        // 食べ物の通し番号を時計回りに代入
        indexOfFoods = new List<int> { 0, 1, 2, 3, 4 };
    }

    private void Update()
    {
        // ポーズ中ならUpdateを抜ける
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        if (isRotating)
        {
            int count = 0;　// 目的地点に到着した食べ物の数
            for (var i = 0; i < foods.transform.childCount; i++)
            {
                GameObject food = foods.transform.GetChild(i).gameObject;
                // 食べ物を右(左)隣の食べ物のポジションまで移動させる
                food.transform.position = Vector3.MoveTowards(food.transform.position, endPos_foods[i], moveSpeed * Time.deltaTime);
                if (Vector3.Distance(food.transform.position, endPos_foods[i]) == 0)
                {
                    count++;
                }

                // すべての食べ物が目的地に着いたら、移動を終える
                if (count == endPos_foods.Count)
                {
                    ActiveFoodsPnl();
                    isRotating = false;
                }
            }

        }
    }
    // 食べ物回転ボタンを押した時
    public void ClickRotateBtn_DOWN(GameObject rotateBtn)
    {
        // 食べ物回転中ならメソッドを抜ける
        if (isRotating)
        {
            return;
        }

        // 左
        if (rotateBtn == rotateBtnL)
        {
            // 右隣の食べ物の座標を移動先に
            // ex)リストを{0,1,2,3,4} → {1,2,3,4,0}にする
            Vector3 temp1 = endPos_foods[0];
            endPos_foods.RemoveAt(0);
            endPos_foods.Add(temp1);
            // 食べ物が時計回りに1つずれた状態でインデックスを更新
            // ex)リストを{0,1,2,3,4} → {4,0,1,2,3}にする
            int temp2 = indexOfFoods[indexOfFoods.Count - 1];
            indexOfFoods.RemoveAt(indexOfFoods.Count - 1);
            indexOfFoods.Insert(0, temp2);

        }
        // 右
        else
        {
            // 左隣の食べ物の座標を移動先に
            // ex)リストを{0,1,2,3,4} → {4,0,1,2,3}にする
            Vector3 temp1 = endPos_foods[endPos_foods.Count - 1];
            endPos_foods.RemoveAt(endPos_foods.Count - 1);
            endPos_foods.Insert(0, temp1);
            // 食べ物が反時計回りに1つずれた状態でインデックスを更新
            // ex)リストを{0,1,2,3,4} → {1,2,3,4,0}にする
            int temp2 = indexOfFoods[0];
            indexOfFoods.RemoveAt(0);
            indexOfFoods.Add(temp2);

        }

        // 移動開始
        isRotating = true;
        InActiveFoodsPnl();
    }

    // 「食べる」ボタンをクリックした時
    public void ClickEatBtn()
    {
        // プレイヤーの操作を受け付けないようにする
        this.gameObject.GetComponent<StageManager>().CantGameControl();
        foodsPnl.SetActive(false);

        PickupFoods(this.GetCancellationTokenOnDestroy()).Forget();

    }
    // player,friend1(2)が順番に食べ物を手に取る処理
    private async UniTask PickupFoods(CancellationToken ct)
    {
        // -------- friend1(2)が手に取る食べ物を調べリストに代入 -----------
        // friend1,friend2の手前の食べ物のインデックス
        int indexOfFriend1sFood = 1;
        int indexOfFriend2sFood = 4;

        // friend1の手前から、反時計回りに手に取る(表示されている)食べ物のインデックスを調べる
        for (indexOfFriend1sFood = 1; indexOfFriend1sFood < 4; indexOfFriend1sFood++)
        {
            GameObject food = foods.transform.GetChild(indexOfFoods[indexOfFriend1sFood]).gameObject;
            if (food.GetComponent<SpriteRenderer>().enabled)
            {
                break;
            }
        }
        // friend2の手前から、時計回りに手に取る(表示されている)食べ物のインデックスを調べる
        for (indexOfFriend2sFood = 4; indexOfFriend2sFood > 1; indexOfFriend2sFood--)
        {
            GameObject food = foods.transform.GetChild(indexOfFoods[indexOfFriend2sFood]).gameObject;
            if (food.GetComponent<SpriteRenderer>().enabled)
            {
                break;
            }
        }
        // player,friend1,friend2が取る食べ物のインデックスを代入
        List<int> indexes = new List<int> { 0, indexOfFriend1sFood, indexOfFriend2sFood };
        // -----------------------------------------------------------------
        
        for (var i = 0; i < trios.Count; i++)
        {
            // player,friend1,friend2が順番に食べ物を取るアニメーション再生
            trios[i].GetComponent<Animator>().Play($"{trios[i].name}Pickup");
            // 順番に皿の上にある食べ物を非表示にする
            GameObject food = foods.transform.GetChild(indexOfFoods[indexes[i]]).gameObject;
            food.GetComponent<SpriteRenderer>().enabled = false;
            // 非表示にした食べ物を手の中に表示
            if (triosFoods[i])
            {
                triosFoods[i].GetComponent<SpriteRenderer>().sprite = food.GetComponent<SpriteRenderer>().sprite;
                triosFoods[i].GetComponent<SpriteRenderer>().enabled = true;
            }
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: ct);
        }
    }

    private void ActiveFoodsPnl()
    {
        for (var i = 0; i < foodsPnl.transform.childCount; i++)
        {
            foodsPnl.transform.GetChild(i).GetComponent<Image>().enabled = true;
        }
    }
    private void InActiveFoodsPnl()
    {
        for (var i = 0; i < foodsPnl.transform.childCount; i++)
        {
            foodsPnl.transform.GetChild(i).GetComponent<Image>().enabled = false;
        }
    }

}
