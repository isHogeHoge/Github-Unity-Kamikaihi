using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySE : MonoBehaviour
{
    private AudioSource se;

    private void Start()
    {
        se = this.GetComponent<AudioSource>();
    }
    // SEの再生
    public void PlayAudioClip(AudioClip audioClip)
    {
        se.PlayOneShot(audioClip);
    }
}
