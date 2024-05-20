using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class PlayerAndSlimeCnt : MonoBehaviour
{
    [SerializeField] GameObject stageManager; 
    [SerializeField] GameObject cancelPnl;    // クリック操作を禁止するパネル
    [SerializeField] GameObject upButton;    
    [SerializeField] GameObject downButton;  
    [SerializeField] GameObject leftButton;  
    [SerializeField] GameObject rightButton; 
    [SerializeField] GameObject kickButton;
    [SerializeField] GameObject itemButton; // きな粉アイテム使用ボタン
    [SerializeField] Image img_hpBar;       
    [SerializeField] Image img_itemBar;    
    [SerializeField] GameObject slimes;      
    [SerializeField] Sprite hpBar_OverSpr;  // ゲームオーバー時のHPBar画像
    [SerializeField] int stageNum;

    // playerが現在向いている方向(列挙型)
    private enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    // 現在の向きに応じて、LEFT,RIGHT,UP,DOWNクラスを切り替えるためのディクショナリ
    Dictionary<Direction, IDirection> direction_dic;

    private StageManager sm;        　　      
    private List<GameObject> buttons;        // 操作ボタンのリスト
    private GameObject col_slime;            // キック時に接触したスライム
    private Animator animator_player;         // Playerのアニメーター
    private Direction nowDir_enum;　　　　　　　// Playerが向いている方向(列挙型)
    private string nowDir_str;                // Playerが向いてる方向(文字列)
    private Vector3 endPos_player;
    private Vector3 endPos_slime;
    private bool moving_player = false;     
    private bool moving_slime = false;      
    private bool isKicking = false;            　// Playerキック中フラグ
    private bool isClicked_WASDBtn = false;   // 移動ボタンがクリックされているか
    private bool gotAKinako = false;       // きな粉アイテムを取得済みかフラグ
    /// <summary>
    /// キックボタンを離した時、他のボタンを使用可能にするかのフラグ
    /// スライムと接触していない、またはスライム移動後ならtrue
    /// </summary>
    private bool isActiveBtns = true;
    
    void Start()
    {
        // 初期の向きを左に設定
        nowDir_str = "LEFT";
        nowDir_enum = Direction.Left;

        // ディクショナリの初期化
        // Playerが向いている方向(列挙型)をキーに、LEFT,RIGHT,UP,DOWNクラスのインスタンスを取得できるようにする
        direction_dic = new Dictionary<Direction, IDirection>();
        direction_dic[Direction.Up] = new UP();
        direction_dic[Direction.Down] = new DOWN();
        direction_dic[Direction.Left] = new LEFT();
        direction_dic[Direction.Right] = new RIGHT();

        // 移動ボタンとキックボタンをリストに代入
        // きな粉アイテムが使用できるステージなら、アイテム使用ボタン(×ボタン)もリストに加える
        if (itemButton)
        {
            buttons = new List<GameObject> { upButton, downButton, leftButton, rightButton, kickButton, itemButton };
        }
        else
        {
            buttons = new List<GameObject> { upButton, downButton, leftButton, rightButton, kickButton};
        }

        animator_player = this.GetComponent<Animator>();
        sm = stageManager.GetComponent<StageManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        // +++++++ player移動 +++++++
        if (moving_player)
        {
            const float speed = 1.5f;   
            // endPosまで(一定のスピードで)移動
            this.transform.position = Vector3.MoveTowards(this.transform.position, endPos_player, speed * Time.deltaTime);

            // endPosに到着した時
            if (this.transform.position == endPos_player)
            {                
                // 移動ボタンを押していたら
                if (isClicked_WASDBtn)
                {
                    // 向きに応じて、playerの移動先の位置を更新
                    StartMovement_Player();
                }
                // 移動ボタンを押していなかったら、すべてのボタンを使用可にして移動終了
                else
                {
                    ActiveBtns();
                    moving_player = false;
                }
                
            }
        }
        
        // +++++++++++++++++++++++++++++++

        // ++++++ slime移動 +++++++
        if (moving_slime)
        {
            const float speed_slime = 1.0f;
            // endPosまで(一定のスピードで)移動
            col_slime.transform.position = Vector3.MoveTowards(col_slime.transform.position, endPos_slime, speed_slime * Time.deltaTime);
            // endPosに到着した時、すべてのボタンを使用可能にして移動終了
            if (col_slime.transform.position == endPos_slime)
            {
                ActiveBtns();
                isActiveBtns = true;
                moving_slime = false;
            }
        }
        //++++++++++++++++++++++++
    }

    // Playerの移動開始処理
    private void StartMovement_Player()
    {
        // 向きに応じて、移動先&移動アニメーションの設定
        IDirection direction = direction_dic[nowDir_enum];
        endPos_player = direction.SetTargetPos(this.gameObject);
        direction.PlayMoveAnima(animator_player);
        moving_player = true;
    }
    // ++++++++ Button全般 ++++++++
    // 現在クリックしているボタン以外、使用不可に
    public void InActiveBtns()
    {
        //押されたボタンのオブジェクトを取得　
        GameObject clickedBtn = EventSystem.current.currentSelectedGameObject;
        foreach (var i in buttons)
        {
            if (i != clickedBtn)
            {
                i.SetActive(false);
            }
        }
    }
    // すべてのボタンを使用可能に
    public void ActiveBtns()
    {
        foreach (var i in buttons)
        {
            i.SetActive(true);
        }
    }
    // +++++++++++++++++++++++++++++

    // +++++++++ 移動ボタン +++++++++
    // クリック時
    public void ClickWASDBtn_Down(string dir)
    {
        if (moving_player)
        {
            return;
        }

        // Direction型(列挙型)の変数を設定、Leftを初期値として代入
        Direction dirType = Direction.Left;
        switch (dir)
        {
            case "UP":
                dirType = Direction.Up;
                break;
            case "DOWN":
                dirType = Direction.Down;
                break;
            case "RIGHT":
                dirType = Direction.Right;
                break;
        }

        isClicked_WASDBtn = true;
        // クリックしている移動ボタン以外を使用不可に
        InActiveBtns();

        // もし現在の向きとクリックしたボタンの方向が一緒なら、プレイヤー移動
        if (dir == nowDir_str)
        {
            // Playrerを取得した向きへ移動させる
            StartMovement_Player();
        }
        // そうでないなら、クリックしたボタンの方向にプレイヤーの向きを変更
        else
        {
            // 現在の向きをクリックした方向に設定する
            nowDir_str = dir;
            nowDir_enum = dirType;

            // Playerの向きに応じて、停止アニメーション再生
            IDirection direction = direction_dic[nowDir_enum];            
            direction.PlayStopAnima(animator_player);
        }
    }
    // 離した時
    public void ClickWASDBtn_Up()
    {
        isClicked_WASDBtn = false;
        if (!moving_player)
        {
            ActiveBtns();
        }
    }
    // +++++++++++++++++++++++++++++++


    // +++++++++ キックボタン +++++++++
    // クリック時
    public void ClickKickBtn_Down()
    {
        // スライム移動中ならキック無効
        if (moving_slime)
        {
            return;
        }

        isKicking = true;
        // キックボタン以外を使用不可に
        InActiveBtns();

        // Playerの向きに応じてキックアニメーションを再生
        IDirection direction = direction_dic[nowDir_enum];
        direction.PlayKickAnima(animator_player);
    }
    // キックボタンを離した時
    public void ClickKickBtn_Up()
    {
        // スライム移動中ならキック無効
        if (moving_slime)
        {
            return;
        }

        // すべてのボタンを使用可能に
        if (isActiveBtns)
        {
            ActiveBtns();
        }
    }
    // キックアニメーション再生後処理
    private void EndKickAnime()
    {
        isKicking = false;        
    }
    // +++++++++++++++++++++++++++++++

    // ++++++++ アイテムボタン +++++++++
    public void ClickItemBtn()
    {
        // きな粉アイテムを取得していないならメソッドを抜ける
        if (!gotAKinako)
        {
            return;
        }

        // Playerの進行方向1マス先にスライムがあれば、きな粉アイテムを使用
        IDirection direction = direction_dic[nowDir_enum];
        GameObject slime = direction.GetSlimeInFrontOfPlayer(this.transform.gameObject);
        if (slime)
        {
            SpriteRenderer sr_kinakoOnSlime = slime.transform.GetChild(1).GetComponent<SpriteRenderer>();
            SpriteRenderer sr_sprinkledKinako = slime.transform.GetChild(2).GetComponent<SpriteRenderer>();

            float playerHeight = this.GetComponent<SpriteRenderer>().bounds.size.y;
            // SlimeがPlayer(足元座標)より上にあるなら、きな粉が奥に表示されるようにレイヤーを変更する
            if (slime.transform.position.y > this.transform.position.y - playerHeight / 2)
            {
                sr_kinakoOnSlime.sortingOrder = 3;
                sr_sprinkledKinako.sortingOrder = 3;
            }

            // スライムの上にきな粉を表示
            sr_kinakoOnSlime.enabled = true;
            // スライムにきな粉を振りかけるアニメーション再生
            sr_sprinkledKinako.enabled = true;
            slime.transform.GetChild(2).GetComponent<Animator>().enabled = true;
        }
    }
    // +++++++++++++++++++++++++++++++


    // ++++++++ 接触判定 +++++++++
    // コライダーEnter
    private void OnTriggerEnter2D(Collider2D col)
    {
        // スライムと接触
        if (col.tag == "Enemy")
        {
            // キックボタンを離しても、他のボタンを使用不可能のままに
            isActiveBtns = false;
        }

        // キック時以外
        if (!isKicking)
        {
            // きな粉アイテムと接触
            if (col.tag == "Item")
            {
                // きな粉アイテムを使用可能に
                gotAKinako = true;
                col.transform.gameObject.SetActive(false);
                img_itemBar.enabled = true;
                itemButton.GetComponent<Button>().enabled = true;
            }
            // スライム内部と接触(ゲームオーバー)
            else if (col.tag == "Dead")
            {
                // プレイヤーの操作を受け付けないようにする
                cancelPnl.SetActive(true);
                isClicked_WASDBtn = false;
                // ゲームオーバー処理
                img_hpBar.sprite = hpBar_OverSpr;
                animator_player.Play("PlayerDead");
                sm.GameOver(this.GetCancellationTokenOnDestroy()).Forget();
            }
            // ゴール地点のコライダーと接触(ゲームクリア)
            else if (col.tag == "Clear")
            {
                // プレイヤーの操作を受け付けないようにする
                cancelPnl.SetActive(true);
                isClicked_WASDBtn = false;
                // ゲームクリア処理
                sm.GameClear(stageNum, this.GetCancellationTokenOnDestroy()).Forget();
            }

        }        
    }
    // コライダーExit
    private void OnTriggerExit2D(Collider2D col)
    {
        // スライムを蹴った後
        if (col.tag == "Enemy" && isKicking) 
        {
            IDirection direction = direction_dic[nowDir_enum];
            // 移動先に別のスライムがあるなら、キックしたスライムを移動させずにメソッドを抜ける
            if (direction.ObstacleExistsToDistination(col.gameObject))
            {
                // すべてのボタンを使用可能に
                ActiveBtns();
                isActiveBtns = true;
                return;
            }
            // 移動先に別のスライムがないなら、キックしたスライムをPlayerが向いている方向へ移動させる
            else
            {
                // スライム移動アニメーション再生
                col.GetComponent<Animator>().Play("SlimeMove");
                // キックしたスライムをendPosまで移動させる
                col_slime = col.gameObject;
                endPos_slime = direction.SetTargetPos(col_slime);
                moving_slime = true;
            }
        }
    }
    // ++++++++++++++++++++++++++++++++
    

}

