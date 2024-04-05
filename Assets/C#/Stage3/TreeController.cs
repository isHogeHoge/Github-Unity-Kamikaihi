using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TreeController : MonoBehaviour, IPointerClickHandler
{
    private Animator animator_Tree;     
    private int clickCount = 0;            // 木をクリックした回数

    void Start()
    {
        animator_Tree = this.GetComponent<Animator>();
    }

    // 自身がクリックされたときに呼び出されるメソッド
    public void OnPointerClick(PointerEventData eventData)
    {
        clickCount++;
        // 木が揺れるアニメーション再生
        animator_Tree.Play("TreeSway");

        // クリックした回数が3以下(木に桃がついている)なら、桃が落ちるアニメーション再生
        if (clickCount <= 3)
        {
            GameObject obj = GameObject.Find($"PeachOnTheTree{clickCount}");   
            Animator animator_Peach = obj.GetComponent<Animator>();
            animator_Peach.SetBool("isFall", true);
        }

    }
}
