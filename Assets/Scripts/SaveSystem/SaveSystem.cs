using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public static class SaveSystem
{
    public static PlayerData currentSave;
    public static List<string> savePaths;
    public static bool newArea = false;



    public static int counter = 0;
    /// <summary>
    /// Used To Test the Saving System
    /// </summary>
    public static void Save(PlayerLogic player, Transform transform)
    {
        if (savePaths == null)
        {
            savePaths = new List<string>();
        }

        Debug.Log("Saving");

        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + player.playerName + DateTime.Now.ToString("MM-dd-yyyy HH-mm") + counter + ".bin";
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData(player, transform, WeaponManager.weapons, WeaponManager._currentWeapon);
        counter++;
        bf.Serialize(stream, data);
        stream.Close();
        savePaths.Add(path);
        StoreSavePaths();
        currentSave = data;
    }
    public static void NewGameSave(string playerName)
    {
        if (savePaths == null)
        {
            Debug.Log("No existing saves found");
            savePaths = new List<string>();
        }
        Debug.Log("Saving");
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + playerName + DateTime.Now.ToString("MM-dd-yyyy HH-mm") + counter + ".bin";
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData(playerName);
        counter++;
        bf.Serialize(stream, data);
        stream.Close();
        savePaths.Add(path);
        StoreSavePaths();
        currentSave = data;
    }
    /// <summary>
    /// We Don't actually store a save File but simply Change the current Save data
    /// so that the player is spawned in the next area with the appropriate characteristics
    /// </summary>
    public static void AreaChangeSave(PlayerLogic player)
    {
        newArea = true;
        PlayerData data = new PlayerData(player, WeaponManager.weapons, WeaponManager._currentWeapon);
        currentSave = data;
    }
    public static void StoreSavePaths() 
    {
        if (savePaths == null)
        {
            Debug.Log("No existing saves found");
            savePaths = new List<string>();
            return;
        }
        else
        {
            BinaryFormatter bf = new BinaryFormatter();
            string path = Application.persistentDataPath + "/saveLocations.bin";
            FileStream stream = new FileStream(path, FileMode.Create);

            bf.Serialize(stream, savePaths);
            stream.Close();
        }

    }
    public static PlayerData Load()
    {
        return null;
    }

     public static void Initialize(List<string> savePathsList)
    {
        savePaths = savePathsList; 
    }


    public static void Load(string path)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);
        PlayerData data = bf.Deserialize(stream) as PlayerData;
        
        stream.Close();

        SceneManager.LoadScene(data.location);

        currentSave = data;
        
    }

}
