using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataLoad : MonoBehaviour
{
    SaveData saveData;
    string GameDatafileName;
    public PlayerManager playerManager;

    private void Start()
    {
        playerManager = PlayerManager.Instance;
    }

    public void LoadSaveData()
    {
        string filePath = Application.persistentDataPath + GameDatafileName;

        if(File.Exists(filePath))
        {
            string fromJsonData = File.ReadAllText(filePath);
            saveData = JsonUtility.FromJson<SaveData>(fromJsonData);
        }
        else
        {
            saveData = new SaveData();
        }
}
}
