using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageTransitionBtnCnt : MonoBehaviour
{
    [SerializeField] GameObject audioPlayerSE; 
    [SerializeField] AudioClip clickSound1; // ステージ移行ボタンを1回クリックした時のSE
    [SerializeField] AudioClip clickSound2; // ステージ移行ボタンを連続で2回クリックした時のSE

    private AudioSource se;
    private GameObject clickedObject1; // 1回目にクリックしたゲームオブジェクト
    private GameObject clickedObject2; // 2回目にクリックしたゲームオブジェクト
    private Ray ray;　　　　　　　　　　　// マウスポジションへのray(光線)
    private RaycastHit2D hit2d;        // rayに当たったもの

    private void Start()
    {
        se = audioPlayerSE.GetComponent<AudioSource>();
        clickedObject1 = null;
    }


    // ステージ移行ボタンをクリックした時
    public void ClickEvent()
    {

        // +++ クリックしたステージ移行ボタンを取得 +++
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

        if (hit2d)
        {
            clickedObject2 = hit2d.transform.gameObject;
        }
        // +++++++++++++++++++++++++++++++++++++++

        // 1回目と2回目でクリックしたボタンが同じならステージ移行
        if (clickedObject1 == clickedObject2)
        {

            se.PlayOneShot(clickSound2);

            ChangeScene changeScene = clickedObject2.GetComponent<ChangeScene>();
            changeScene.Load();
        }
        // そうでないなら(1回目と2回目でクリックしたボタンが違う)、1回目にクリックしたボタンとする
        else
        {
            se.PlayOneShot(clickSound1);
            clickedObject1 = clickedObject2;
        }

    }

}
