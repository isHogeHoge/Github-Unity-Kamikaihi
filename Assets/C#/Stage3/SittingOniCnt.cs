using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SittingOniCnt : MonoBehaviour
{
    [SerializeField] GameObject peachBesideOni1; // 回収された桃(1個目)
    [SerializeField] GameObject peachBesideOni2; // 回収された桃(2個目)
    [SerializeField] GameObject standingOni;   
    
    private Animator animator;   
    private int peachCount = 0;      // 接触した桃の数

    private void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // 接触判定
    private void OnTriggerEnter2D(Collider2D col)
    {
        // 桃と接触した時
        if(col.tag == "Item")
        {
            peachCount++;
            Debug.Log(peachCount);
            // 流れてきた桃を回収する(非アクティブ)
            col.gameObject.SetActive(false);
            animator.Play("OniPickup",0,0);     

        }
        // Playerと接触した時
        else if(col.tag == "Player")
        {
            // 流れてきたPlayerを回収する(非アクティブ)
            col.gameObject.SetActive(false);

            // Oniの切り替え & ゲームオーバーアニメーションの再生
            this.gameObject.SetActive(false);
            standingOni.SetActive(true); 

        }

    }

    // ------------ Animation ------------
    // 流れてきた桃を回収後
    // 1or2個目なら回収した分だけ桃を側に表示
    private void ActivePeachBesideOni()
    {
        switch (peachCount)
        {
            case 1:
                peachBesideOni1.GetComponent<SpriteRenderer>().enabled = true;
                break;
            case 2:
                peachBesideOni1.GetComponent<SpriteRenderer>().enabled = true;
                peachBesideOni2.GetComponent<SpriteRenderer>().enabled = true;
                break;
            
        }
    }
    // 3個目ならゲームクリア
    private void isPickUpAllPeach()
    {
        if (peachCount == 3)
        {
            // 回収した桃を非表示
            peachBesideOni1.GetComponent<SpriteRenderer>().enabled = false;
            peachBesideOni2.GetComponent<SpriteRenderer>().enabled = false;

            // Oniの切り替え & ゲームクリアアニメーションの再生
            this.gameObject.SetActive(false);
            standingOni.SetActive(true);
            standingOni.GetComponent<Animator>().SetBool("isClear", true);

        }

    }
    // --------------------------------------
}
