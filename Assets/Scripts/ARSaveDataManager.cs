using UnityEngine;
using System.IO;

public class ARSaveDataManager : MonoBehaviour
{
    const string FIXED_SAVEDATA_FILENAME = "worldmaplist.json";

    public static ARWorldList GetAllWorlds()
    {
        ARWorldList worldList = null;
        string savedatapath = Path.Combine(Application.persistentDataPath, FIXED_SAVEDATA_FILENAME);
        if (File.Exists(savedatapath))
        {
            worldList = JsonUtility.FromJson<ARWorldList>(File.ReadAllText(savedatapath));
        } else
        {
            return new ARWorldList();
        }

        return worldList;
    }

    public static void SetAllWorlds(ARWorldList worldList)
    {   
        string worldListPath = Path.Combine(Application.persistentDataPath, FIXED_SAVEDATA_FILENAME);
        File.WriteAllText(worldListPath, JsonUtility.ToJson(worldList, true));
    }

    public static ARWorldSaveData GetWorld(string worldName)
    {
        ARWorldSaveData savedata = null;
        string savedatapath = Path.Combine(Application.persistentDataPath, worldName + ".json");
        if (File.Exists(savedatapath))
        {
            savedata = JsonUtility.FromJson<ARWorldSaveData>(File.ReadAllText(savedatapath));
        }
        else
        {
            Debug.LogError("No AR World Save Data found!");
        }
        
        return savedata;
    }



    public static bool SaveWorld(ARWorldSaveData savedata)
    {
        ARWorldList newWorldList = GetAllWorlds();
        string newWorldName = savedata.worldMapName;

        if (newWorldName == FIXED_SAVEDATA_FILENAME)
        {
            Debug.LogError("File name error, please rename file!");
            return false;
        }

        if (newWorldList.worldNames.Contains(newWorldName))
        {
            Debug.Log("Saving over pre-existing file!");
        }

        //Checking for the existence of the world map before saving to the index of world maps
        if (File.Exists(Path.Combine(Application.persistentDataPath, savedata.worldMapName + ".worldmap"))){
            newWorldList.worldNames.Add(newWorldName);
            string worldListPath = Path.Combine(Application.persistentDataPath, FIXED_SAVEDATA_FILENAME);
            string savedataPath = Path.Combine(Application.persistentDataPath, newWorldName + ".json");
            File.WriteAllText(worldListPath, JsonUtility.ToJson(newWorldList, true));
            File.WriteAllText(savedataPath, JsonUtility.ToJson(savedata, true));
            return true;
        } else {
            Debug.LogError("No corresponding .worldmap found.");
            return false;
        }

        
    }

    public static bool DeleteWorld(string mapToRemove)
    {
        string worldMapPath = Path.Combine(Application.persistentDataPath, mapToRemove + ".worldmap");
        ARWorldList newWorldList = GetAllWorlds();
        if (newWorldList.worldNames.Contains(mapToRemove))
        {
            if (File.Exists(worldMapPath))
            {
                File.Delete(worldMapPath);
                newWorldList.worldNames.Remove(mapToRemove);

                return true;
            }else
            {
                Debug.LogError(".worldmap file does not exist.");
                return false;
            }
        }else
        {
            Debug.LogError("World List does not contain map to remove. Are you sure this is the right name?");
            return false;
        }
    }


}
