using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{

    private string path = "";
    private string filename = "SaveData.json";

    // Sets the path to the json file
    // If in editor, path is Assets/SaveData.json
    // If in build, path is C:/Users/[user]/AppData/LocalLow/[company]/[game]/SaveData.json
    private void Awake() {
        SetPath();
    }

    private void SetPath() {
        if(Application.isEditor)  {
            path = Application.dataPath + Path.AltDirectorySeparatorChar + filename;
        } else {
            path = Application.persistentDataPath + Path.AltDirectorySeparatorChar + filename;
        }
    }

    // Saves player data to json file at path
    public void Save(PlayerData playerData)
    {
        if(path == "") SetPath();

        string json = JsonUtility.ToJson(playerData, true);
        using StreamWriter writer = new StreamWriter(path);
        writer.Write(json);
    }

    // Load data from the json file and return as key value pairs
    public PlayerData LoadData()
    {
        if(path == "") SetPath();
        
        try {
            using StreamReader reader = new StreamReader(path);
            string json = reader.ReadToEnd();

            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            return data;
        } catch(System.IO.FileNotFoundException) {
            Debug.Log("No save file found, creating new one");
            PlayerData data = new PlayerData(0, 0, 3, 0);
            Save(data);
            return data;
        }

    }

}
