using DataInfo;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance = null;

    private string dataPath;
    private const string GAME_DATA_PATH = "/gameData.dat";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Initialize();
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void Initialize()
    {
        dataPath = Application.persistentDataPath + GAME_DATA_PATH;
    }

    //데이터 저장 및 파일을 생성하는 함수.
    public void Save(GameData gameData)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(dataPath);

        bf.Serialize(file, gameData);
        file.Close();

        //GameData data = new GameData();
    }

    //파일에 저장된 데이터를 리턴해주는 함수.
    public GameData Load()
    {
        if(File.Exists(dataPath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(dataPath, FileMode.Open);

            GameData data = (GameData)bf.Deserialize(file);
            file.Close();

            return data;
        }
        else
        {
            GameData data = new GameData();

            return data;
        }
    }
}
