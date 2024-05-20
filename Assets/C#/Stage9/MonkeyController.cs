using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MonkeyController : MonoBehaviour
{
    [SerializeField] Image img_birdsNest;
    [SerializeField] Image img_bananaInBirdsNest;
    [SerializeField] Animator animator_RVine;
    [SerializeField] Animator animator_MVine;
    [SerializeField] Animator animator_LVine;

    // ------------ Animation ------------
    // バナナ取得時
    private void InActiveBananaInBirdsNest()
    {
        // 鳥の巣からバナナを非表示
        img_bananaInBirdsNest.enabled = false;
        img_birdsNest.enabled = true;
    }
    // ツタを通り過ぎた後
    private void VineHangDown(string dir)
    {
        // 接触したツタを垂れ下げる
        switch (dir)
        {
            case "R":
                animator_RVine.Play($"RVineHangDown");
                break;
            case "M":
                animator_MVine.Play($"MVineHangDown");
                break;
            case "L":
                animator_LVine.Play($"LVineHangDown");
                break;
            default:
                Debug.Log("無効な文字列です");
                break;
        }
        
    }
    // -----------------------------------

}
