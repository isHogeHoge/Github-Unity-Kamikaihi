using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Video;

public class CloudController : MonoBehaviour
{
    [SerializeField] GameObject tutorialManager;      
    [SerializeField] GameObject openingVP;           
    [SerializeField] GameObject tutorialVP;          

    private const float scrollSpeedX = 1.1f;

    private void Update()
    {
        // ポーズ中ならUpdateを抜ける
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        transform.Translate(-scrollSpeedX * Time.deltaTime, 0, 0);

    }


}
