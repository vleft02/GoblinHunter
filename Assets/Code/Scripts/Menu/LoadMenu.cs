using System;
using System.IO;
using TMPro;
using UnityEngine;
using Newtonsoft.Json;

public class LoadMenu : MonoBehaviour
{
    [SerializeField]GameObject SaveSlotPrefab;
    public static Action RefreshSaveList;
    private void OnEnable()
    {
        RefreshSaveList += ShowSaveList;
    }

    private void OnDisable()
    {
        RefreshSaveList -= ShowSaveList;
    }


    /// <summary>
    /// We deserialize the Load files and show some data to identify the save file
    /// </summary>
    public void ShowSaveList()
    {
        if (SaveManager.GetSaveFilePaths() != null)
        {
            GameObject loadList = GameObject.Find("LoadList");
          
            
            
            foreach (Transform child in loadList.transform)
            {
                Destroy(child.gameObject);
            }

            for (int i = SaveManager.GetSaveFilePaths().Count-1; i >= 0; i--) 
            {
                string path = SaveManager.GetSaveFilePaths()[i];
                if (File.Exists(path))
                {
                    

                    string loadedJsonData = File.ReadAllText(path);

                    var settings = new JsonSerializerSettings();
                    settings.Converters.Add(new Vector3Converter());
                    GameData data = JsonConvert.DeserializeObject<GameData>(loadedJsonData, settings);


                    GameObject saveSlot = Instantiate(SaveSlotPrefab);
                    TextMeshProUGUI[] textFields = saveSlot.GetComponentsInChildren<TextMeshProUGUI>();
                    textFields[0].text = data.playerData.playerName;
                    textFields[2].text = data.time;
                    textFields[4].text = data.currentArea.GetAreaName();
                    textFields[6].text = data.playerData.kills.ToString();
                    textFields[8].text = path;
                    saveSlot.transform.SetParent(loadList.transform);
                    saveSlot.transform.localScale = Vector3.one;
                }
            }
        }
        else 
        {
            Debug.Log("List of save paths was null");
        }
    }



}

/// <summary>
/// Due to NewtonSoft Json serializer not being able to serialize/deserialize vector3 Data 
/// We set custom behavior
/// </summary>
public class Vector3Converter : JsonConverter<Vector3>
{
    public override void WriteJson(JsonWriter writer, Vector3 value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("x");
        writer.WriteValue(value.x);
        writer.WritePropertyName("y");
        writer.WriteValue(value.y);
        writer.WritePropertyName("z");
        writer.WriteValue(value.z);
        writer.WriteEndObject();
    }



    public override Vector3 ReadJson(JsonReader reader, Type objectType, Vector3 existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.StartObject)
        {
            reader.Read(); 
            float x = float.Parse(reader.Value.ToString());

            reader.Read();
            reader.Read(); 
            float y = float.Parse(reader.Value.ToString());

            reader.Read(); 
            reader.Read();
            float z = float.Parse(reader.Value.ToString());

            reader.Read();
            return new Vector3(x, y, z);
        }
        else if (reader.TokenType == JsonToken.StartArray)
        {
            float[] values = serializer.Deserialize<float[]>(reader);
            if (values.Length >= 3)
            {
                return new Vector3(values[0], values[1], values[2]);
            }
        }

        throw new JsonSerializationException("Unexpected token type for Vector3.");
    }

    public override bool CanRead => true;
    public override bool CanWrite => true;
}
