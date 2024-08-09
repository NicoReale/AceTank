using UnityEngine;
using System.IO;

public class SaveBehaviour
{
    string path;
    string persistentPath;


    public void checkForData()
    {
        //LoadGame();
        persistentPath = Application.persistentDataPath;
        path = persistentPath + Path.AltDirectorySeparatorChar + "data.json";

        if (!Directory.Exists(persistentPath))
        {
            Directory.CreateDirectory(persistentPath);
        }
    }

    public void SaveGame(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
    }
    public SaveData LoadGame()
    {
        if (!File.Exists(path))
        {
            SaveGame(GameManager.instance.saveData);
        }
        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<SaveData>(json);
    }
}
