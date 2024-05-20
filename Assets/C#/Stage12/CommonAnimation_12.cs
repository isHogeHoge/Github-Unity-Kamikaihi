using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public class CommonAnimation_12 : MonoBehaviour
{
    [SerializeField] Button cushion1Btn; 
    [SerializeField] Button cushion2Btn; 
    [SerializeField] Button fakeBtn;     

    /// <summary>
    /// 自身を再びクリックできるようにする
    /// </summary>
    /// <param name="name">クリック可能にするオブジェクト名</param>
    private void ActiveBtn(string name)
    {
        switch (name)
        {
            case "Cushion1":
                cushion1Btn.enabled = true;
                break;
            case "Cushion2":
                cushion2Btn.enabled = true;
                break;
            case "Fake":
                fakeBtn.enabled = true;
                break;
            default:
                Debug.Log("無効な文字列です");
                break;
        }
    }
}
