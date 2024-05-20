using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeReduction : MonoBehaviour
{
    [SerializeField] Vector2 targetSize; // 縮小サイズ
    [SerializeField] float speed;        // 縮小スピード
    private RectTransform rect;

    void Start()
    {
        rect = this.GetComponent<RectTransform>();
        // 縮小サイズを現在の2分の1に
        targetSize = new Vector2(this.rect.sizeDelta.x / 2, this.rect.sizeDelta.y / 2);
    }

    void Update()
    {
        // Beeのサイズ縮小
        this.rect.sizeDelta = Vector3.MoveTowards(this.rect.sizeDelta, targetSize, speed * Time.deltaTime);
        
    }
}
