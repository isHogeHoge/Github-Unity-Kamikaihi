using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using System;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] string sceneName;

    // シーンを読み込む
    public async void Load()
    {
        // クリックSEが鳴り終わるまで一時停止
        await UniTask.Delay(TimeSpan.FromSeconds(0.3f),true,cancellationToken: this.GetCancellationTokenOnDestroy());
        SceneManager.LoadScene(sceneName);

        // ゲームが停止していたら、ゲームを再開させる
        if(Time.timeScale == 0.0f)
        {
            Time.timeScale = 1.0f;
        }
        
    }
}
