using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MonkeyController : MonoBehaviour
{
    [SerializeField] GameObject monkey;
    [SerializeField] GameObject birdsNest;
    [SerializeField] GameObject BananaInBirdsNest;
    [SerializeField] GameObject RVine;
    [SerializeField] GameObject MVine;
    [SerializeField] GameObject LVine;

    // ------------ Animation ------------
    // バナナ取得時
    private void InActiveBananaInBirdsNest()
    {
        // 鳥の巣からバナナを非表示
        BananaInBirdsNest.GetComponent<Image>().enabled = false;
        birdsNest.GetComponent<Image>().enabled = true;
    }
    // ツタを通り過ぎた後
    private void VineHangDown(string dir)
    {
        // 接触したツタを垂れ下げる
        switch (dir)
        {
            case "R":
                RVine.GetComponent<Animator>().Play($"RVineHangDown");
                break;
            case "M":
                MVine.GetComponent<Animator>().Play($"MVineHangDown");
                break;
            case "L":
                LVine.GetComponent<Animator>().Play($"LVineHangDown");
                break;
            default:
                Debug.Log("無効な文字列です");
                break;
        }
        
    }
    // -----------------------------------

}
