using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VineController : MonoBehaviour
{
    [SerializeField] GameObject RVineBtn;
    [SerializeField] GameObject MVineBtn;
    [SerializeField] GameObject LVineBtn;

    // ツタが垂れ下がった後
    private void ActiveVineItemBtn(string dir)
    {
        // 垂れ下がったツタをアイテムとして取得できるようにする
        switch (dir)
        {
            case "R":
                RVineBtn.GetComponent<Button>().enabled = true;
                break;
            case "M":
                MVineBtn.GetComponent<Button>().enabled = true;
                break;
            case "L":
                LVineBtn.GetComponent<Button>().enabled = true;
                break;
            default:
                Debug.Log("無効な文字列です");
                break;
        }
    }
}
