using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class EraserCnt : MonoBehaviour
{
    [SerializeField] Image img_eraserBtn;         
    [SerializeField] SpriteRenderer sr_eraserOnGround;
    [SerializeField] SpriteRenderer sr_smoke1;           
    [SerializeField] SpriteRenderer sr_smoke2;           

    // ------ Animation ------
    // 黒板消し落下後
    private void ActiveSmoke()
    {
        // 煙を表示
        sr_smoke1.enabled = true;
        sr_smoke2.enabled = true;

    }
    // 黒板消しが転がった後
    private void ActiveEraserOnTheGround()
    {
        // 黒板消しを裏返った状態に変更
        img_eraserBtn.enabled = false;
        sr_eraserOnGround.enabled = true;

    }
    // -----------------------
}
