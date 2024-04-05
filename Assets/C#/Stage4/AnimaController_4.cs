using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimaController_4 : MonoBehaviour
{
    [SerializeField] GameObject eraserBtn;
    [SerializeField] GameObject door;

    /// <summary>
    /// ドアが開くアニメーション再生
    /// </summary>
    /// <param name="amount">ドアの開き具合(Half,Full)</param>
    private void OpenTheDoor(string amount)
    {
        door.GetComponent<Animator>().Play($"DoorIsOpened_{amount}");
    }
    // 黒板消しが落ちるアニメーション再生
    private void EraserFall()
    {
        eraserBtn.GetComponent<Animator>().Play("EraserFall");
    }
}
