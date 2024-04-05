using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapAnimaCnt : MonoBehaviour
{
    [SerializeField] GameObject hotBath;
    [SerializeField] GameObject signBoard;

    // +++++ Needle'sTrap +++++
    // 針が引っ込むアニメーション終了時
    private void ActiveHotBathTrap()
    {
        // お湯風呂トラップに切り替わる
        hotBath.GetComponent<Animator>().Play("HotBathActive");
        signBoard.GetComponent<SpriteRenderer>().enabled = true;
    }
    // ++++++++++++++++++++++++
}
