using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class ARSaveDataManager : MonoBehaviour
{
    const string FIXED_SAVEDATA_FILENAME = "worldmaplist.json";

    public static ARWorldList GetWorldList()
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

    public static void SetWorldList(ARWorldList worldList)
    {   
        string worldListPath = Path.Combine(Application.persistentDataPath, FIXED_SAVEDATA_FILENAME);
        File.WriteAllText(worldListPath, JsonUtility.ToJson(worldList));
    }

    public static ARWorldSaveData GetWorldByName(string worldName)
    {
        ARWorldSaveData savedata;
        string savedatapath = Path.Combine(Application.persistentDataPath, worldName + ".json");
        if (File.Exists(savedatapath))
        {
            savedata = JsonUtility.FromJson<ARWorldSaveData>(File.ReadAllText(savedatapath));
            return savedata;
        }
        else
        {
            Debug.LogError("No AR World Save Data found!");
            List<ARObjectMetadata> list = new List<ARObjectMetadata>();
            savedata = new ARWorldSaveData(worldName, list.ToArray());
            return savedata;
        }
    }

    public static ARWorldSaveData GetWorldByPath(string path)
    {
        ARWorldSaveData savedata;
        if (File.Exists(path))
        {
            savedata = JsonUtility.FromJson<ARWorldSaveData>(File.ReadAllText(path));
            return savedata;
        }
        else
        {
            Debug.LogError("No AR World Save Data found!");
            List<ARObjectMetadata> list = new List<ARObjectMetadata>();
            savedata = new ARWorldSaveData("", list.ToArray());
            return savedata;
        }
    }

    public static bool SaveWorld(ARWorldSaveData savedata)
    {
        ARWorldList newWorldList = GetWorldList();
        string newWorldName = savedata.worldMapName;
        bool containsWorld = false;

        if (newWorldName == FIXED_SAVEDATA_FILENAME || newWorldName.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
        {
            Debug.LogError("File name error, please rename file!");
            return false;
        }

        if (newWorldList.worldNames.Contains(newWorldName))
        {
            Debug.Log("Saving over pre-existing file!");
            containsWorld = true;
        }

        //Checking for the existence of the world map before saving to the index of world maps
        if (File.Exists(Path.Combine(Application.persistentDataPath, savedata.worldMapName + ".worldmap"))){
            if (containsWorld == false)
            {
                newWorldList.worldNames.Add(newWorldName);
                SetWorldList(newWorldList);
            }

            string savedataPath = Path.Combine(Application.persistentDataPath, newWorldName + ".json");
            foreach(ARObjectMetadata data in savedata.ARObjectList)
            {
                print(data.objectType);
            }
            File.WriteAllText(savedataPath, JsonUtility.ToJson(savedata));
            print("saving world save data to : " + savedataPath);
            return true;
        } else {
            Debug.LogError("No corresponding .worldmap found.");
            return false;
        }

        
    }

    public static bool DeleteWorld(string mapToRemove)
    {
        string worldMapPath = Path.Combine(Application.persistentDataPath, mapToRemove + ".worldmap");
        string worldSaveDatapath = Path.Combine(Application.persistentDataPath, mapToRemove + ".json");
        ARWorldList newWorldList = GetWorldList();
        if (newWorldList.worldNames.Contains(mapToRemove))
        {
            if (File.Exists(worldMapPath))
            {
                File.Delete(worldMapPath);
                print(mapToRemove + ".worldmap" + " was deleted.");
                if (File.Exists(worldSaveDatapath))
                {
                    File.Delete(worldSaveDatapath);
                    print(mapToRemove + ".json" + " was deleted.");
                }else
                {
                    Debug.LogError("No world save data json found, critical error.");
                }
                newWorldList.worldNames.Remove(mapToRemove);
                SetWorldList(newWorldList);
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

    public void PrintAllWorlds()
    {
        ARWorldList list = GetWorldList();
        foreach(string world in list.worldNames)
        {
            print(world);
        }

        print("done printing all worlds in master list");
    }

    public void DeleteAllWorlds()
    {
        ARWorldList list = new ARWorldList();
        SetWorldList(list);
        print("done erasing all worlds");
    }

    public void PrintAllWorldMaps()
    {
        DirectoryInfo info = new DirectoryInfo(Application.persistentDataPath);
        foreach(FileInfo file in info.EnumerateFiles())
        {
            if (file.Extension.Contains("json") && file.Name.Contains("worldmaplist") == false)
            {
                print("----objects in " + file.Name + " ----");
                char[] splitter = {'.'};
                print("world name: " + file.Name.Split(splitter)[0]);
                ARWorldSaveData worldData = GetWorldByName(file.Name.Split(splitter)[0]);
                if (worldData.ARObjectList != null)
                {
                    foreach(ARObjectMetadata obj in worldData.ARObjectList)
                    {
                        print(obj.objectType + " " + obj.position);
                    }
                }
                print("----end of world data for " + file.Name + " ----");
            }
        }
        foreach(string s in Directory.GetFiles(Application.persistentDataPath))
        {
            print(s);
        }

        print("done printing world maps");
    }

    public void DeleteAllWorldMaps()
    {
        foreach(string s in Directory.GetFiles(Application.persistentDataPath))
        {
            File.Delete(s);
        }

        print("done erasing all contents of persistent data path.");
    }
}
