using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager_28 : MonoBehaviour
{
    [SerializeField] Animator animator_customerL;
    [SerializeField] Animator animator_customerM;
    void Start()
    {
        // 5秒後にCustomerL,10秒後にCustomerMが退出する
        Invoke(nameof(CustomerLGoOut), 5f);
        Invoke(nameof(CustomerMGoOut), 10f);
    }

    // CustomerR退出処理
    private void CustomerLGoOut()
    {
        animator_customerL.Play("CustomerLGoOut");
    }
    // CustomerM退出処理
    private void CustomerMGoOut()
    {
        animator_customerM.Play("CustomerMGoOut");
    }
}
