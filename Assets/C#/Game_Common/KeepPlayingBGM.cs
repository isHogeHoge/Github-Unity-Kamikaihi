using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepPlayingBGM : MonoBehaviour
{
    private static bool isLoad = false;// 自身がすでにロードされているかを判定するフラグ
    private void Awake()
    {
        // すでにロードされていたら、自身を破棄して終了
        if (isLoad) 
        { 
            Destroy(gameObject);
            return;
        }
        // ロードされていなかったら、シーンを跨いでBGMを鳴らし続ける
        isLoad = true; 
        DontDestroyOnLoad(gameObject);
    }
}
