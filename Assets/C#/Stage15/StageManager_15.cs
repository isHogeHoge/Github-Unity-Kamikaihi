using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine.UI;
public class StageManager_15 : MonoBehaviour
{
    [SerializeField] Image img_door_Closed;
    [SerializeField] Image img_door_Open;
    [SerializeField] Button doorBtn;
    [SerializeField] Image img_goButton;

    private void Start()
    {
        // ステージ初期位置から左に1ページ分だけ移動できるように設定
        this.GetComponent<StageScrollCnt>().maxCountL = -1;
    }

    // ---------- Button ------------
    // 扉
    public async void ClickDoorBtn()
    {
        doorBtn.enabled = false;

        // 扉の開閉
        OpenTheDoor();
        await UniTask.Delay(TimeSpan.FromSeconds(1f),cancellationToken: this.GetCancellationTokenOnDestroy());
        CloseTheDoor();

        doorBtn.enabled = true;
    }
    // -------------------------------

    // ゲーム操作を禁止に
    public void CantGameControl()
    {
        img_goButton.enabled = false;
        this.GetComponent<StageManager>().CantGameControl();
    }
    // 家のドアを開ける
    internal void OpenTheDoor()
    {
        img_door_Closed.enabled = false;
        img_door_Open.enabled = true;
    }
    // 家のドアを閉める
    private void CloseTheDoor()
    {
        img_door_Closed.enabled = true;
        img_door_Open.enabled = false;
    }


}
