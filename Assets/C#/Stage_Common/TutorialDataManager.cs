using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;

[System.Serializable]
public class TutorialData
{
    public bool playedTutorial;  // チュートリアル再生フラグ
}


public class TutorialDataManager : MonoBehaviour
{
    private TutorialData loadData; // jsonファイル出力先
    private string filePath; // 保存・読み込み先のパス

    private void Awake()
    {
#if UNITY_EDITOR // Unityエディター上のパス
        filePath = Application.streamingAssetsPath + "/Datas_json/TutorialData.json";
#elif UNITY_IOS // iOS上のパス
        // 2回目以降はApplication.persistentDataPathからデータを読み込む(読み書き可能)
        filePath = Application.persistentDataPath + "/TutorialData.json";
        // 初回のみStreamingAssetsフォルダからデータを読み込む(読み込み専用)
        if (!File.Exists(filePath))
        {
            filePath = Application.dataPath + "/Raw/Datas_json/TutorialData.json";
        }
#endif
    }

    // jsonファイルの読み込み
    internal void Load()
    {
        StreamReader rd = new StreamReader(filePath);           
        string json = rd.ReadToEnd();                       
        rd.Close();
        // jsonファイルをTutorialData型にし代入
        loadData = JsonUtility.FromJson<TutorialData>(json);    

    }

    // チュートリアル動画再生済みチェック
    // チュートリアル動画再生済みでtrue
    internal bool IsPlayedTutorialVideo()
    {
        return loadData.playedTutorial;
    }

    // チュートリアル再生済みにし、jsonファイルに書き込む
    internal void ReWrite()
    {
        Load();
        loadData.playedTutorial = true;
        string json = JsonUtility.ToJson(loadData);
        string save_path = "";
# if UNITY_EDITOR // Unityエディター上の保存先パス
        save_path = Application.streamingAssetsPath + "/Datas_json/TutorialData.json";
# elif UNITY_IOS // iOS上の保存先パス
        save_path = Application.persistentDataPath + "/TutorialData.json";
# endif
        StreamWriter wr = new StreamWriter(save_path, false);    
        wr.WriteLine(json);                                     
        wr.Flush();
        wr.Close();

    }
}