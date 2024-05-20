using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonAnimation_4 : MonoBehaviour
{
    [SerializeField] Animator animator_eraserBtn;
    [SerializeField] Animator animator_door;

    /// <summary>
    /// ドアが開くアニメーション再生
    /// </summary>
    /// <param name="amount">ドアの開き具合(Half,Full)</param>
    private void OpenTheDoor(string amount)
    {
        animator_door.Play($"DoorIsOpened_{amount}");
    }
    // 黒板消しが落ちるアニメーション再生
    private void EraserFall()
    {
        animator_eraserBtn.Play("EraserFall");
    }
}
