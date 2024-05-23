using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class CustomerAnimaCnt_28 : MonoBehaviour
{
    [SerializeField] Button seatLBtn;
    [SerializeField] Button seatMBtn;
    [SerializeField] SpriteRenderer sr_ramenL;
    [SerializeField] SpriteRenderer sr_ramenM;
    [SerializeField] Animator animator_clerk;
    [SerializeField] Sprite dishOfRamen;

    // +++++ CustomerL&M +++++
    // 退出アニメーション開始時
    private void EmptytheDish(string customer) // 退出したCustomer
    {
        // ラーメンの器を空にする
        switch (customer)
        {
            // 退出したのがCustomerL
            case "CustomerL":
                sr_ramenL.sprite = dishOfRamen;
                break;
            // 退出したのがCustomerM
            case "CustomerM":
                sr_ramenM.sprite = dishOfRamen;
                break;
            default:
                Debug.Log($"{customer}は無効な値です");
                break;
        }
        
    }
    // 退出アニメーション終了時
    private void PlayerCanSitDown(string customer) // 退出したCustomer
    {
        // ラーメンの器を非表示 & Player空いた座席に着席可能に
        switch (customer)
        {
            // 退出したのがCustomerL
            case "CustomerL":
                sr_ramenL.enabled = false;
                seatLBtn.enabled = true;
                break;
            // 退出したのがCustomerM
            case "CustomerM":
                sr_ramenM.enabled = false;
                seatMBtn.enabled = true;
                break;
            default:
                Debug.Log($"{customer}は無効な値です");
                break;
        }
        animator_clerk.Play("ClerkWalk");
    }
    // +++++++++++++++++++++++

}
