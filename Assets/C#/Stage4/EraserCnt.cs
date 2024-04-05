using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class EraserCnt : MonoBehaviour
{
    [SerializeField] GameObject eraserBtn;         
    [SerializeField] GameObject eraserOnGround;
    [SerializeField] GameObject smoke1;           
    [SerializeField] GameObject smoke2;           

    // ------ Animation ------
    // 黒板消し落下後
    private void ActiveSmoke()
    {
        // 煙を表示
        smoke1.GetComponent<SpriteRenderer>().enabled = true;
        smoke2.GetComponent<SpriteRenderer>().enabled = true;

    }
    // 黒板消しが転がった後
    private void ActiveEraserOnTheGround()
    {
        // 黒板消しを裏返った状態に変更
        eraserBtn.GetComponent<Image>().enabled = false;
        eraserOnGround.GetComponent<SpriteRenderer>().enabled = true;

    }
    // -----------------------
}
