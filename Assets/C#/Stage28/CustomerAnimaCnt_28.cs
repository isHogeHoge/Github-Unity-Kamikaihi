using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class CustomerAnimaCnt_28 : MonoBehaviour
{
    [SerializeField] GameObject seatLBtn;
    [SerializeField] GameObject seatMBtn;
    [SerializeField] GameObject ramenL;
    [SerializeField] GameObject ramenM;
    [SerializeField] GameObject clerk;
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
                ramenL.GetComponent<SpriteRenderer>().sprite = dishOfRamen;
                break;
            // 退出したのがCustomerM
            case "CustomerM":
                ramenM.GetComponent<SpriteRenderer>().sprite = dishOfRamen;
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
                ramenL.GetComponent<SpriteRenderer>().enabled = false;
                seatLBtn.GetComponent<Button>().enabled = true;
                break;
            // 退出したのがCustomerM
            case "CustomerM":
                ramenM.GetComponent<SpriteRenderer>().enabled = false;
                seatMBtn.GetComponent<Button>().enabled = true;
                break;
            default:
                Debug.Log($"{customer}は無効な値です");
                break;
        }
        clerk.GetComponent<Animator>().Play("ClerkWalk");
    }
    // +++++++++++++++++++++++

}
