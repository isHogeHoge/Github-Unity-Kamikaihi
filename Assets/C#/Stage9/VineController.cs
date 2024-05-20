using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VineController : MonoBehaviour
{
    [SerializeField] Button RVineBtn;
    [SerializeField] Button MVineBtn;
    [SerializeField] Button LVineBtn;

    // ツタが垂れ下がった後
    private void ActiveVineItemBtn(string dir)
    {
        // 垂れ下がったツタをアイテムとして取得できるようにする
        switch (dir)
        {
            case "R":
                RVineBtn.enabled = true;
                break;
            case "M":
                MVineBtn.enabled = true;
                break;
            case "L":
                LVineBtn.enabled = true;
                break;
            default:
                Debug.Log("無効な文字列です");
                break;
        }
    }
}
