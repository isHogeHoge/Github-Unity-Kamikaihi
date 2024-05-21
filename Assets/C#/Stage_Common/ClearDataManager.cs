using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;

[System.Serializable]
public class ClearData
{
    public int stageId;  // ステージID(範囲は1~31)
    public bool isClear;  // クリアフラグ
}
[System.Serializable]
public class ClearDataList
{
    public List<ClearData> dataLists;
}

public class ClearDataManager : MonoBehaviour
{
    internal ClearDataList loadDatas; //jsonファイル出力先
    private string filePath; // 保存・読み込み先のパス
    
    private void Awake()
    {
#if UNITY_EDITOR // Unityエディター上のパス
        filePath = Application.streamingAssetsPath + "/Datas_json/ClearDataList.json";
#elif UNITY_IOS // iOS上のパス
        // 2回目以降はApplication.persistentDataPathからデータを読み込む(読み書き可能)
        filePath = Application.persistentDataPath + "/ClearDataList.json";
        // 初回のみStreamingAssetsフォルダからデータを読み込む(読み込み専用)
        if (!File.Exists(filePath))
        {
            filePath = Application.dataPath + "/Raw/Datas_json/ClearDataList.json";
        }
        
#endif
    }

    // ファイルの読み込み
    internal void Load()
    {
        StreamReader rd = new StreamReader(filePath);
        string json = rd.ReadToEnd();
        rd.Close();
        // jsonファイルをSavaDataList型にし代入
        loadDatas = JsonUtility.FromJson<ClearDataList>(json);

    }

    /// <summary>
    /// クリアしたステージを上書き保存
    /// </summary>
    /// <param name="id">クリアしたステージのid.範囲は1~31</param>
    internal void ReWrite(int id)
    {
        Load();
        foreach(var i in loadDatas.dataLists)
        {
            // もしステージが未クリアであった場合、クリア済みにする
            if (i.stageId == id && i.isClear == false) 
            {
                i.isClear = true;
            }
        }
        // jsonファイルに変更した内容を、書き込む
        string json = JsonUtility.ToJson(loadDatas);
        string save_path = "";
# if UNITY_EDITOR // Unityエディター上の保存先パス
        save_path = Application.streamingAssetsPath + "/Datas_json/ClearDataList.json";
# elif UNITY_IOS // iOS上の保存先パス
        save_path = Application.persistentDataPath + "/ClearDataList.json";
# endif
        StreamWriter wr = new StreamWriter(save_path, false);
        wr.WriteLine(json);                                     
        wr.Flush();
        wr.Close();                                             
    }

    
    // (クリア済みなら)ステージ選択ボタンの画像を変更
    internal async void ChangeStagesBtnImg()
    {
        Load();
        for(var i = 0; i < loadDatas.dataLists.Count; i++)
        {
            if (loadDatas.dataLists[i].isClear)
            {
                GameObject stageBtn = GameObject.Find($"Stage{loadDatas.dataLists[i].stageId}Button");

                // クリア済み画像の読み込み
                AsyncOperationHandle<Sprite> handle = Addressables.LoadAssetAsync<Sprite>($"Assets/Image/StageSelect/StageSelectBtn/Stage{loadDatas.dataLists[i].stageId}Clear.png");
                Sprite sprite = await handle.Task;   // 画像の読み込みが終わるまでストップ
                stageBtn.GetComponent<Image>().sprite = sprite; // 画像の変更
            }
        }
    }

    //ステージ1~30クリア済みチェック
    // (最終ステージを抜いた)全ステージクリアならtrueを返す
    internal bool isClear_AllStage()
    {
        Load();
        for (var i = 0; i < loadDatas.dataLists.Count - 1; i++) // ステージ31(最終ステージ)は対象外なので-1
        {
            if (loadDatas.dataLists[i].isClear)
            {
                continue;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

}
