using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Vector3 direction;

    void Update()
    {
        // ポーズ中はUpdateを抜ける
        if (Mathf.Approximately(Time.timeScale, 0f))
        {
            return;
        }

        this.transform.position += speed * direction * Time.deltaTime;
    }
}
