using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageManager_14 : MonoBehaviour
{
    [SerializeField] GameObject objs_changeColor; // 色を変更するオブジェクト(Player,Car,CrossWalk,Clock)
    [SerializeField] GameObject player;

    // ---------- Button ------------
    // 時計(ゲーム停止)
    public void ClickClockBtn_Stop()
    {
        // 時間停止(timeScaleを0に)
        Mathf.Approximately(Time.timeScale, 0f);
        
        for(var i = 0; i < objs_changeColor.transform.childCount; i++)
        {
            // objs_changedColor(Player,Car,CrossWalk,Clock)のカラーを濃い灰色に変更
            GameObject obj = objs_changeColor.transform.GetChild(i).gameObject;
            obj.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.2f, 0.2f, 1f);

            // PlayerとCarのアニメーションスピードを0にする
            if(obj.name == "Player" || obj.name == "Car")
            {
                obj.GetComponent<Animator>().SetFloat("Speed", 0f);
            }
        }

    }
    // 時計(ゲーム再開)
    public void ClickClockBtn_Restart()
    {
        // 時間停止解除(timeScaleを1に)
        Mathf.Approximately(Time.timeScale, 1f);

        // playerの"PlayreStop"アニメーションを再生
        //player.GetComponent<Animator>().Play("PlayerStop");
        
        for (var i = 0; i < objs_changeColor.transform.childCount; i++)
        {
            // objs_changedColor(Player,Car,CrossWalk,Clock)のカラーを元に戻す
            GameObject obj = objs_changeColor.transform.GetChild(i).gameObject;
            obj.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);

            // PlayerとCarのアニメーションスピードを元に戻す
            if (obj.name == "Player" || obj.name == "Car")
            {
                obj.GetComponent<Animator>().SetFloat("Speed", 1f);
            }
        }
        
    }
    // ------------------------------------
}
