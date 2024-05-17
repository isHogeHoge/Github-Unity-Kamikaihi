using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelClickOutOfScreen : MonoBehaviour
{
    private Vector3 leftBottom;                       
    private Vector3 rightTop;　　　　　　　　　　　　　　 

    void Start()
    {
        // ゲーム画面左下&右上座標を取得(スクリーン座標)
        float cameraWidth = Camera.main.rect.width;
        float cameraHeight = Camera.main.rect.height;
        Vector3 cameraPos = Camera.main.WorldToScreenPoint(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0f));
        leftBottom = new Vector3(cameraPos.x - (Screen.width * cameraWidth * 0.5f), cameraPos.y - (Screen.height * cameraHeight * 0.5f), 0f);
        rightTop = new Vector3(cameraPos.x + (Screen.width * cameraWidth * 0.5f), cameraPos.y + (Screen.height * cameraHeight * 0.5f), 0f);
    }

    
    // 画面をタップした時、ゲーム画面内ならtrue,範囲外ならfalseを返す
    internal bool isWithinTheGameScreen()
    {
        Vector3 mousePos = Input.mousePosition;
        if ((leftBottom.x <= mousePos.x && mousePos.x <= rightTop.x) &&
            (leftBottom.y <= mousePos.y && mousePos.y <= rightTop.y))
        {
            return true;
        }
        return false;
    }
}
