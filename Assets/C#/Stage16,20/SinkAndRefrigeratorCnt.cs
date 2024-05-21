using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinkAndRefrigeratorCnt : MonoBehaviour
{
    // シンクor冷蔵庫の扉をクリックした時
    // ドアを開閉させる
    public void ClickDoorBtn(Image img_OpenDoor)
    {
        img_OpenDoor.enabled = !img_OpenDoor.enabled;
    }
    // (引数で指定した)アイテムボタンのアクティブ・非アクティブを切り替える
    public void SwitchActiveAndInActive_ItemBtn(Image img_itemBtn)
    {
        img_itemBtn.enabled = !img_itemBtn.enabled;

    }
}
