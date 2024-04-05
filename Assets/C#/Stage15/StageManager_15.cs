using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine.UI;
public class StageManager_15 : MonoBehaviour
{
    [SerializeField] GameObject door_Closed;
    [SerializeField] GameObject door_Open;
    [SerializeField] GameObject doorBtn;
    [SerializeField] GameObject goButton;

    private void Start()
    {
        // ステージ初期位置から左に1ページ分だけ移動できるように設定
        this.gameObject.GetComponent<StageScrollCnt>().maxCountL = -1;
    }

    // ---------- Button ------------
    // 扉
    public async void ClickDoorBtn()
    {
        doorBtn.GetComponent<Button>().enabled = false;

        // 扉の開閉
        OpenTheDoor();
        await UniTask.Delay(TimeSpan.FromSeconds(1f),cancellationToken: this.GetCancellationTokenOnDestroy());
        CloseTheDoor();

        doorBtn.GetComponent<Button>().enabled = true;
    }
    // -------------------------------

    // ゲーム操作を禁止に
    public void CantGameControl()
    {
        goButton.GetComponent<Image>().enabled = false;
        this.GetComponent<StageManager>().CantGameControl();
    }
    // 家のドアを開ける
    internal void OpenTheDoor()
    {
        door_Closed.GetComponent<Image>().enabled = false;
        door_Open.GetComponent<Image>().enabled = true;
    }
    // 家のドアを閉める
    private void CloseTheDoor()
    {
        door_Closed.GetComponent<Image>().enabled = true;
        door_Open.GetComponent<Image>().enabled = false;
    }


}
