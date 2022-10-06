using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Linq;

public class SaveSystem: MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    public static SaveSystem instance { get; private set; }

    private GameData gameData;
    private List<IDataSave> dataObjects;
    private FileDataHandler dataHandler;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
            dataObjects = FindAllDataSaveObjects();
            LoadGame();
        }
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void NewGame()
    {
        gameData = new GameData();
        gameData.NewGameData();
    }


    public void SaveGame()
    {
        foreach (IDataSave dataObj in dataObjects)
        {
            dataObj.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
    }

    public void LoadGame()
    {
        gameData = dataHandler.Load();

        if (gameData == null)
        {
            NewGame();
        }

        foreach (IDataSave dataObj in dataObjects)
        {
            dataObj.LoadData(gameData);
        }
        gameData.NullDefaultItems();
    }

    private List<IDataSave> FindAllDataSaveObjects()
    {
        IEnumerable<IDataSave> dataSaveObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataSave>();
        return new List<IDataSave>(dataSaveObjects);
    }
}
