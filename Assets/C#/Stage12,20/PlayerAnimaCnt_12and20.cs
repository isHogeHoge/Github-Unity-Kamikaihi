using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimaCnt_12and20 : MonoBehaviour
{
    [SerializeField] GameObject btn_TakeOffAHat;

    // 帽子を脱いだ後
    private void InActiveBtn_TakeOffAHat()
    {
        // 帽子をかぶるアニメーション再生ボタンを使用 & キャンディーアイテム取得可能に
        btn_TakeOffAHat.GetComponent<Image>().enabled = false;

    }
    // 帽子を被った後
    private void ActiveBtn_TakeOffAHat()
    {
        // 帽子を脱ぐアニメーション再生ボタンを使用可能に
        btn_TakeOffAHat.GetComponent<Image>().enabled = true;
    }
}