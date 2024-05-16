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
        btn_OpenTheWindow.GetComponent<Image>().enabled = false;
    }
    // 窓を閉めるアニメーション終了時
    private void CanOpenTheWindow()
    {
        btn_OpenTheWindow.GetComponent<Image>().enabled = true;
    }
}
