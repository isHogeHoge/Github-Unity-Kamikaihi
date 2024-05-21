using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class WindowAnimaCnt : MonoBehaviour
{
    [SerializeField] Image img_OpenTheWindowBtn;

    // 窓を開けるアニメーション終了時
    private void CanCloseTheWindow()
    {
        img_OpenTheWindowBtn.enabled = false;
    }
    // 窓を閉めるアニメーション終了時
    private void CanOpenTheWindow()
    {
        img_OpenTheWindowBtn.enabled = true;
    }
}
