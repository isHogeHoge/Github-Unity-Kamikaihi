using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public class AnimaController_12 : MonoBehaviour
{
    [SerializeField] GameObject cushion1Btn; 
    [SerializeField] GameObject cushion2Btn; 
    [SerializeField] GameObject fakeBtn;     

    /// <summary>
    /// 自身を再びクリックできるようにする
    /// </summary>
    /// <param name="name">クリック可能にするオブジェクト名</param>
    private void ActiveBtn(string name)
    {
        switch (name)
        {
            case "Cushion1":
                cushion1Btn.GetComponent<Button>().enabled = true;
                break;
            case "Cushion2":
                cushion2Btn.GetComponent<Button>().enabled = true;
                break;
            case "Fake":
                fakeBtn.GetComponent<Button>().enabled = true;
                break;
            default:
                Debug.Log("無効な文字列です");
                break;
        }
    }
}
