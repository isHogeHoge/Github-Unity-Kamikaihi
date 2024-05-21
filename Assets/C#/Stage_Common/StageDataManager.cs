using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

[System.Serializable]
public class ReleasedCountData
{
    public int releasedCount;  // 解放済みステージ数
}

[System.Serializable]
public class StageData
{
    public int stageId;     // ステージid
    public bool isReleased;  // ステージ解放フラグ
}
[System.Serializable]
public class StageDataList
{
    public List<StageData> dataLists;
}


public class StageDataManager : MonoBehaviour
{
    private ClearDataManager cdm;
    // jsonファイルの出力先
    private StageDataList loadDatas;   // stageId,isReleased
    private ReleasedCountData loadData;    // releasedCount

    private string filePath_isReleased;   // 保存・読み込み先のパス(StageDataList)
    private string filePath_releasedCount;  // 保存・読み込み先のパス(ReleasedCountData)
    private string json;       // json形式に変換用変数
    private int count_ActiveStage = 0;

    private void Awake()
    {
# if UNITY_EDITOR // Unityエディター上のパス
        filePath_isReleased = Application.streamingAssetsPath + "/Datas_json/StageDataList.json";
        filePath_releasedCount = Application.streamingAssetsPath + "/Datas_json/ReleasedCountData.json";
# elif UNITY_IOS // iOS上のパス
        // 2回目以降はApplication.persistentDataPathからデータを読み込む(読み書き可能)
        filePath_isReleased = Application.persistentDataPath + "/StageDataList.json";
        filePath_releasedCount = Application.persistentDataPath + "/ReleasedCountData.json";
        // 初回のみStreamingAssetsフォルダからデータを読み込む(読み込み専用)
        if (!File.Exists(filePath_isReleased) || !File.Exists(filePath_releasedCount))
        {
            filePath_isReleased = Application.dataPath + "/Raw/Datas_json/StageDataList.json";
            filePath_releasedCount = Application.dataPath + "/Raw/Datas_json/ReleasedCountData.json";
        }
#endif
    }
    private void Start()
    {
        cdm = this.GetComponent<ClearDataManager>();
    }

    // jsonファイル読み込み
    internal void Load(string path)
    {
        if (!File.Exists(path))
        {
            Debug.Log("ファイルが見つかりませんでした");
            return;
        }

        StreamReader rd = new StreamReader(path); 
        json = rd.ReadToEnd();                       
        rd.Close();

        if (path == filePath_isReleased)
        {
            loadDatas = JsonUtility.FromJson<StageDataList>(json);    // jsonファイルをStageDataList型にし代入
        }
        else if (path == filePath_releasedCount)
        {
            loadData = JsonUtility.FromJson<ReleasedCountData>(json);    // jsonファイルをReleasedCountData型にし代入
        }
        else
        {
            Debug.Log("そのファイルはロードできません");
        }
        
    }
    /// <summary>
    /// 上書き保存(StageDataList)
    /// </summary>
    /// <param name="fileName">保存先のファイルパス</param>
    /// <param name="datas">上書き保存するデータ</param>
    private void ReWrite(StageDataList datas)
    {
        json = JsonUtility.ToJson(datas);
        string save_path = "";
# if UNITY_EDITOR // Unityエディター上の保存先パス
        save_path = Application.streamingAssetsPath + "/Datas_json/StageDataList.json";
# elif UNITY_IOS // iOS上の保存先パス
        save_path = Application.persistentDataPath + "/StageDataList.json";
# endif
        StreamWriter wr = new StreamWriter(save_path, false);   
        wr.WriteLine(json);                                   
        wr.Flush();
        wr.Close();
    }
    /// <summary>
    /// 上書き保存(ReleasedCountData)
    /// </summary>
    /// <param name="fileName">保存先のファイルパス</param>
    /// <param name="data">上書き保存するデータ</param>
    private void ReWrite(ReleasedCountData data)
    {
        json = JsonUtility.ToJson(data);
        string save_path = "";
# if UNITY_EDITOR // Unityエディター上の保存先パス
        save_path = Application.streamingAssetsPath + "/Datas_json/ReleasedCountData.json";
# elif UNITY_IOS // iOS上の保存先パス
        save_path = Application.persistentDataPath + "/ReleasedCountData.json";
# endif
        StreamWriter wr = new StreamWriter(save_path, false);   
        wr.WriteLine(json);                                   
        wr.Flush();
        wr.Close();                                         
    }



    // +++ ステージ選択シーン読み込み時に呼び出されるメソッド +++
    // 新しいステージを解放する
    internal void ReleaseStage()
    {
        Load(filePath_isReleased);
        Load(filePath_releasedCount);

        // ステージ31(ファイナルステージ)は、全ステージクリアで解放
        // 解放済みステージ数(releasedCount)ではなく、各ステージのクリア状況(ClearDataList)を参照
        if (cdm.isClear_AllStage())
        {
            loadDatas.dataLists[30].isReleased = true;
        }
        // ステージ1~30は、解放済みステージ数(releasedCount)をそのまま反映
        for (var i = 1; i <= loadData.releasedCount; i++)
        {
            if (i <= 30) // ステージ1~30まで
            {
                loadDatas.dataLists[i - 1].isReleased = true;
            }
            
        }
        ReWrite(loadDatas);
    }
    // アクティブにできるステージがあるならtrueを返す
    // 解放済みステージ数が全てアクティブならfalseを返す
    internal bool canActiveStage()
    {
        if (count_ActiveStage == loadData.releasedCount)
        {
            return false;
        }
        return true;
    }
    // ステージの解放状況に応じて、遷移ボタンをアクティブor非アクティブにする
    internal void ActiveOrInActiveStagesBtn()
    {
        Load(filePath_isReleased);
        Load(filePath_releasedCount);
        count_ActiveStage = 0;
        foreach (var i in loadDatas.dataLists)
        {
            // ステージnに遷移するボタンを取得 
            GameObject obj = GameObject.Find($"Stage{i.stageId}Button");               
            Button stageBtn = obj.GetComponent<Button>();
            //  ステージnが解放済みなら、ステージnに遷移するボタンを使用可能に
            if (i.isReleased)
            {
                stageBtn.interactable = true;                              
                stageBtn.transition = Selectable.Transition.Animation;
                count_ActiveStage++;
            }
            //  ステージnが解放済みでないなら、ステージnに遷移するボタンを使用不可 & カラーを灰色に
            else
            {
                
                stageBtn.interactable = false;　　　　　　　　　　　　　　　　　　　　　
                stageBtn.transition = Selectable.Transition.ColorTint;            
                ColorBlock colorBlock = new ColorBlock();                   
                colorBlock.disabledColor = new Color(0.8f, 0.8f, 0.8f);     
                colorBlock.colorMultiplier = 1.0f;                         
                stageBtn.colors = colorBlock;                                    
            }
        }        
    }
    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++

    // +++ ステージクリア時に呼び出されるメソッド +++
    // 解放済みステージ数を+1する
    internal void PlusReleasedCount()
    {
        Load(filePath_releasedCount);
        loadData.releasedCount++;
        ReWrite(loadData);
    }    
    // +++++++++++++++++++++++++++++++++++++++++
}
