using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class WindowAnimaCnt : MonoBehaviour
{
    [SerializeField] GameObject btn_OpenTheWindow;

    // 窓を開けるアニメーション終了時
    private void CanCloseTheWindow()
    {
        // 窓を閉めることが出来るようにする
        btn_OpenTheWindow.GetComponent<Image>().enabled = false;
    }
    // 窓を閉めるアニメーション終了時
    private void CanOpenTheWindow()
    {
        // 再び窓を開けることが出来るようにする
        btn_OpenTheWindow.GetComponent<Image>().enabled = true;
    }
}
