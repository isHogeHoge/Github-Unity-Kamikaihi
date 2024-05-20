using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimaCnt_12and20 : MonoBehaviour
{
    [SerializeField] Image img_takeOffAHatBtn;

    // 帽子を脱いだ後
    private void InActiveBtn_TakeOffAHat()
    {
        // 帽子をかぶるアニメーション再生ボタンを使用 & キャンディーアイテム取得可能に
        img_takeOffAHatBtn.enabled = false;

    }
    // 帽子を被った後
    private void ActiveBtn_TakeOffAHat()
    {
        // 帽子を脱ぐアニメーション再生ボタンを使用可能に
        img_takeOffAHatBtn.enabled = true;
    }
}