using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PrefabAnimaCnt : MonoBehaviour
{
    // フェードアウト終了時、自身を破棄する
    private void InActiveThisObj()
    {
        Destroy(this.gameObject);
    }
}
