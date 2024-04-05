using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinkAndRefrigeratorCnt : MonoBehaviour
{
    // シンクor冷蔵庫の扉をクリックした時
    // ドアを開閉させる
    public void ClickDoorBtn(GameObject img_OpenDoor)
    {
        img_OpenDoor.GetComponent<Image>().enabled = !img_OpenDoor.GetComponent<Image>().enabled;
    }
    // (引数で指定した)アイテムボタンのアクティブ・非アクティブを切り替える
    public void SwitchActiveAndInActive_ItemBtn(GameObject itemBtn)
    {
        itemBtn.GetComponent<Image>().enabled = !itemBtn.GetComponent<Image>().enabled;

    }
}
