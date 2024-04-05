using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] GameObject audioPlayerSE;         
    [SerializeField] Sprite soundSpr;         // 音あり時の画像
    [SerializeField] Sprite notSoundSpr;　    // 消音時の画像

    private Image image;
    private GameObject audioPlayerBGM;                
    private AudioSource se;                           
    private AudioSource bgm;                          
    private static bool isSound = true;      // サウンドフラグ

    private void Start()
    {
        image = this.GetComponent<Image>();
        // Inspectorから取得できない(DontDestoryオブジェクト)ため、Findメソッドで取得
        audioPlayerBGM = GameObject.Find("AudioPlayerBGM");

        bgm = audioPlayerBGM.GetComponent<AudioSource>();
        se = audioPlayerSE.GetComponent<AudioSource>();

    }

    private void Update()
    {
        // 音あり時の処理
        if (isSound)
        {
            se.volume = 1;
            bgm.volume = 0.3f;
            image.sprite = soundSpr;
        }
        // 消音時の処理
        else
        {
            se.volume = 0;
            bgm.volume = 0;
            image.sprite = notSoundSpr;
            
        }
    }

    // SoundButtonを押した時、サウンドフラグを切り替える
    public void ClickSoundButton()
    {
        isSound = !isSound;
        
    }
}
