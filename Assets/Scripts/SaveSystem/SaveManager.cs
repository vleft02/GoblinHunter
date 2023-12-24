using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System.Runtime.InteropServices.WindowsRuntime;


/// <summary>
/// Singleton that handles Saving/Loading Game Data and Holds a list of all the
/// File paths to Save Files
/// </summary>
public sealed class SaveManager
{
    private static SaveManager instance;
    private static List<string> saveGamePaths;

    private SaveManager()
    {
        saveGamePaths = new List<string>();
    }

    public static SaveManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SaveManager();
            }
            return instance;
        }
    }

    /// <summary>
    /// We Serialize the current game data to Json and store it in a file
    /// </summary>
    public static void SaveGame() 
    {
        
        if (saveGamePaths == null)
        {
            saveGamePaths = new List<string>();
        }
        Debug.Log("Saving");
        GameData data = PlayerProfile.gameData;
        data.time = DateTime.Now.ToString("dd/MM HH:mm:ss"); ;
        string path = Application.persistentDataPath + "/" + data.playerData.playerName + DateTime.Now.ToString("MM-dd-yyyy_HH-mm-ss") + ".json";

        string json = JsonConvert.SerializeObject(data, Formatting.Indented);

        File.WriteAllText(path, json);

        saveGamePaths.Add(path);
        StoreSavePaths();
    }

    /// <summary>
    ///     The save file in the given filepath is deserialized to a Game Data object 
    ///     and loaded into the PlayerProfile's current game Data 
    /// </summary>
    /// <param name="path">
    public static void LoadGame(string path)
    {
        string loadedJsonData = File.ReadAllText(path);
        var settings = new JsonSerializerSettings();
        settings.Converters.Add(new Vector3Converter());
        GameData data = JsonConvert.DeserializeObject<GameData>(loadedJsonData, settings);

        PlayerProfile.LoadGameData(data);
        SceneManager.LoadScene(data.currentArea.GetAreaName());

    }

    public static void StoreSavePaths()
    {
        if (saveGamePaths == null)
        {
            Debug.Log("No existing saves found");
            saveGamePaths = new List<string>();
            return;
        }
        else
        {
            BinaryFormatter bf = new BinaryFormatter();
            string path = Application.persistentDataPath + "/saveLocations.bin";
            FileStream stream = new FileStream(path, FileMode.Create);

            bf.Serialize(stream, saveGamePaths);
            stream.Close();
        }
    }


    
    public static List<string> GetSaveFilePaths() 
    {
        return saveGamePaths;
    }

    public static void InitializePaths(List<string> paths)
    {
        saveGamePaths = paths;
    }
}
