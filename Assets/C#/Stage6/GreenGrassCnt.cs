using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GreenGrassCnt : MonoBehaviour
{
    [SerializeField] GameObject GreenGrassBtn;

    // GreenGrassが揺れた後
    // 再びクリック可能に
    private void ActiveGreenGrassBtn()
    {
        GreenGrassBtn.GetComponent<Image>().enabled = true;
    }
    // 停止状態に
    private void StopShakingGreenGrass()
    {
        this.GetComponent<Animator>().SetBool("isShaking", false);
    }
}
