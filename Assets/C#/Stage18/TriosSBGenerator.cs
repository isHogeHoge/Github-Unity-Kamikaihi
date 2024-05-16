using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// player,friend1,friend2の吹き出し管理クラス
public class TriosSBGenerator : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject friend1;
    [SerializeField] GameObject friend2;
    [SerializeField] GameObject trio;   // player,friend1,friend2の親オブジェクト
    [SerializeField] GameObject monk;  
    // 吹き出し内の画像
    [SerializeField] Sprite book;
    [SerializeField] Sprite controller;
    [SerializeField] Sprite girlFriend;
    [SerializeField] Sprite omeletRice;
    [SerializeField] Sprite sodaPop;

    private MonkController mc;
    private List<Sprite> sprites;        // 吹き出し内画像のリスト
    private float deltaTime = 2.5f;  　　// 吹き出しの出現間隔(次の吹き出し出現までに要する時間)
    private float passedTimes = 0f;      // 経過時間(吹き出し出現で0にリセット)
    private float gameTimes = 0f;        // ゲーム経過時間

    void Start()
    {
        mc = monk.GetComponent<MonkController>();
        sprites = new List<Sprite> { book, controller, girlFriend, omeletRice, sodaPop };
    }

    void Update()
    {
        // ポーズならUpdateを抜ける
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        passedTimes += Time.deltaTime;
        gameTimes += Time.deltaTime;

        // ゲーム時間が15秒を経過したら、吹き出しの出現間隔を0.5秒に
        if(gameTimes >= 15f)
        {
            deltaTime = 0.5f;
        }

        // 吹き出し出現処理
        if(passedTimes >= deltaTime)
        {
            // trio(player,Friend1,Friend2)の中から、1人をランダムに抽出
            GameObject someone = trio.transform.GetChild(Random.Range(0,trio.transform.childCount)).gameObject;
            // 吹き出し内の画像をランダムに抽出
            Sprite sprite = sprites[Random.Range(0, sprites.Count)];

            // プレイヤーが吹き出しを消す前に、新たな吹き出しが出現しないようにする
            if (someone.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled)
            {
                return;
            }

            // タップされるまで吹き出しが表示されるようにする
            for (var i = 0; i < someone.transform.childCount; i++)
            {
                someone.transform.GetChild(i).GetComponent<Animator>().SetBool("isStart", false);
            }

            // ------- 吹き出し出現 -------
            // 吹き出し内の画像を設定
            someone.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = sprite;
            // ランダムに選ばれた人物の吹き出し(下)アニメーション再生
            someone.transform.GetChild(0).GetComponent<Animator>().Play("Trio'sSB_Down",0,0);
            someone.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            // --------------------------

            // 僧侶をランダムに選ばれた人物のX座標まで移動
            mc.targetPos = new Vector3(someone.transform.position.x, monk.transform.position.y, 0);

            passedTimes = 0f;
        }
    }

}
