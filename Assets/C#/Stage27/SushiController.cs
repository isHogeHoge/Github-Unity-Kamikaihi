using UnityEngine;
using UnityEngine.UI;

// Trioのテーブル上にある寿司 & Brother'sSushiにアタッチされる
public class SushiController : MonoBehaviour
{
    [SerializeField] GameObject sushiPnl;
    [SerializeField] GameObject eatBtn;
    [SerializeField] GameObject clickCancelPnl;
    [SerializeField] GameObject triosSushi;
    [SerializeField] GameObject itemManager;
    public Sprite sushiSpr; // 自身の画像(アイテムパネルに表示される)

    private ItemManager im;
    private Button btn_eatBtn;
    private GameObject clickedSushi; // クリックした寿司
    private static int sushiCount = 5; // trioのテーブル上にある寿司の数
    private int layerMask = 1 << 10; // "Sushi"レイヤー
    

    private void OnDestroy()
    {
        sushiCount = 5;
    }

    private void Start()
    {
        im = itemManager.GetComponent<ItemManager>();
        btn_eatBtn = eatBtn.GetComponent<Button>();
    }

    private void Update()
    {
        // ポーズ中orタップ禁止中ならUpdateを抜ける
        if (Mathf.Approximately(Time.timeScale, 0f) || clickCancelPnl.activeSelf)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            clickedSushi = null;
            // クリックした場所から伸びるrayに当たったオブジェクト(layerが"Sushi")を取得
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, Mathf.Infinity,layerMask);
            if (!hit2d) return;
            clickedSushi = hit2d.transform.gameObject;
            SpriteRenderer sr_clickedSushi = clickedSushi.GetComponent<SpriteRenderer>();

            // クリックした寿司(取得可能)だけアイテム取得処理を行う
            if (clickedSushi == this.gameObject && sr_clickedSushi.enabled)
            {
                // 皿の上にある寿司を、アイテムとして取得
                im.ClickItemBtn(sushiSpr);
                if (im.isFull) return;
                sr_clickedSushi.enabled = false;

                // trioのテーブルにある寿司が0個なら、寿司パネルを非アクティブに
                // trioのテーブルにある寿司が2個以下なら、「食べる」ボタンを非アクティブに
                if (this.transform.parent.gameObject == triosSushi)
                {
                    MinusSushiCount();
                    isInActiveSushiPnlAndEatBtn();
                }
                clickedSushi = null;
            }
        }
    }

    // (trioの)テーブルの寿司の数を+1する
    internal void PlusSushiCount()
    {
        sushiCount++;
        
    }
    // (trioの)テーブルの寿司の数を-1する
    private void MinusSushiCount()
    {
        sushiCount--;
    }

    // 寿司をアイテム取得した時に実行
    // trioのテーブルにある寿司が0個なら、寿司パネルを非アクティブに
    // trioのテーブルにある寿司が2個以下なら、「食べる」ボタンを非アクティブに
    private void isInActiveSushiPnlAndEatBtn()
    {
        if(sushiCount == 0)
        {
            sushiPnl.SetActive(false);
        }
        if(sushiCount <= 2)
        {
            btn_eatBtn.interactable = false;
        }
    }

    // 寿司アイテムを使用した時に実行
    // trioのテーブルにある寿司が1個以上なら、寿司パネルをアクティブに
    // trioのテーブルにある寿司が3個以上なら、「食べる」ボタンをアクティブに
    internal void isActiveSushiPnlAndEatBtn()
    {
        if (sushiCount >= 1)
        {
            sushiPnl.SetActive(true);
        }
        if (sushiCount >= 3)
        {
            btn_eatBtn.interactable = true;
        }
        
    }
}
