using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelClickOutOfScreen : MonoBehaviour
{
    private Vector3 leftBottom;                       
    private Vector3 rightTop;　　　　　　　　　　　　　　 

    void Start()
    {
        // ゲーム画面左下&右上座標を取得
        float cameraWidth = Camera.main.rect.width;
        float cameraHeight = Camera.main.rect.height;
        leftBottom = Camera.main.ScreenToWorldPoint(Vector3.zero);
        rightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * cameraWidth, Screen.height * cameraHeight, 0f));
        Debug.Log(leftBottom);
        Debug.Log(rightTop);
    }

    
    // 画面をタップした時、ゲーム画面内ならtrue,範囲外ならfalseを返す
    internal bool isWithinTheGameScreen()
    {
        Vector3 mousePos_screen = Input.mousePosition;
        Vector3 mousePos_world = Camera.main.ScreenToWorldPoint(new Vector3(mousePos_screen.x, mousePos_screen.y, 10f));
        Debug.Log(mousePos_world);
        if ((leftBottom.x <= mousePos_world.x && mousePos_world.x <= rightTop.x) &&
            (leftBottom.y <= mousePos_world.y && mousePos_world.y <= rightTop.y))
        {
            Debug.Log("範囲内");
            return true;
        }
        Debug.Log("範囲外");
        return false;
    }
}
