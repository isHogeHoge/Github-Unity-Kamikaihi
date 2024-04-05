using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Test : MonoBehaviour
{
    private ClearDataManager cm;
    private TutorialDataManager tm;
    private StageDataManager sm;

    // Start is called before the first frame update
    void Start()
    {

        //ClearDataManagerのテスト
        /*
        cm = this.GetComponent<ClearDataManager>();
        cm.Save1(Application.dataPath + "/Datas_json/ClearDataList.json");
        */

        /*
        cm = this.GetComponent<ClearDataManager>();
        //cm.Save(1, true);
        //cm.Save(2, true);
        //cm.Save(3, true);
        //cm.Save(4, true);

        */

        // TutorialDataManagerのテスト
        /*
        tm = this.GetComponent<TutorialDataManager>();
        tm.Load(Application.dataPath + "/Datas_json/test2.json");
        Debug.Log(tm.loadData.tutorialFlag);
        tm.ReWrite(Application.dataPath + "/Datas_json/test2.json");
        Debug.Log(tm.loadData.tutorialFlag);
        */

        // StageDataManagerのテスト
        /*
        sm = this.GetComponent<StageDataManager>();
        sm.Save();
        sm.PlusClearCount();
        sm.ReleaseStage();
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
