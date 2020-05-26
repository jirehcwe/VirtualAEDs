using UnityEngine;
using System.Collections.Generic;

public class ARObjectManager : MonoBehaviour
{
    #region Public Fields
    public static ARObjectManager Instance;
    public static List<ARObjectMetadata> objectDataList = new List<ARObjectMetadata>();
    public static List<GameObject> objReferencelist = new List<GameObject>();
    public GameObject aedPrefab;
    public GameObject wallPrefab;
    public GameObject victimPrefab;
    #endregion

    #region Private Fields
    
    #endregion

    void OnEnable()
    {
        ARVictimPlacer.PlaceVictim.AddListener(RegisterARObject);
        ARPlacerAED.PlaceAED.AddListener(RegisterARObject);
        ARWallPlacer.PlaceWall.AddListener(RegisterARObject);
    }

    void OnDisable()
    {
        ARVictimPlacer.PlaceVictim.RemoveListener(RegisterARObject);
        ARPlacerAED.PlaceAED.RemoveListener(RegisterARObject);
        ARWallPlacer.PlaceWall.RemoveListener(RegisterARObject);
    }

    void Start()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(this);
        }else if (Instance == null)
        {
            Instance = this;
        }

        aedPrefab = Resources.Load<GameObject>("AED");
        wallPrefab = Resources.Load<GameObject>("Wall");
        victimPrefab = Resources.Load<GameObject>("Victim_root");
        if (!aedPrefab || !wallPrefab || !victimPrefab)
        {
            Debug.LogError("Prefabs not found in Resources folder!");
        }
    }

    public static void RegisterARObject(Transform objectTransform, ARObjectType objectType){
        ARObjectMetadata newObjectData = new ARObjectMetadata(objectTransform, objectType);
        objectDataList.Add(newObjectData);
        objReferencelist.Add(objectTransform.gameObject);
    }

    void RemoveARObject(){
    }
    
    /// <summary>
    /// This uses the ARSaveDataManager to access the device's persisent data path. Not to be used in editor.
    /// 
    /// </summary>
    /// <param name="worldName">Simple string name to search for AR World Object list.</param>
    public void GenerateARObjectsFromDeviceMemory(string worldName)
    {

        ARWorldSaveData worldSaveData = ARSaveDataSystemIO.GetWorldByName(worldName);
        print("generating world: "  + worldName);
        GenerateARObjects(worldSaveData.ARObjectList);
    }

    public void GenerateARObjectsFromPath(string path)
    {
        ARWorldSaveData worldSaveData = ARSaveDataSystemIO.GetWorldByPath(path);
        print("generating world at "  + path);
        GenerateARObjects(worldSaveData.ARObjectList);
    }

    void GenerateARObjects(ARObjectMetadata[] objectMetadataList)
    {
        
        ResetObjects();

        if (objectMetadataList == null)
        {
            Debug.Log("No list found.");
            return;
        }
        
        if (objectMetadataList.Length == 0)
        {
            Debug.Log("No objects found.");
            return;
        }

        foreach (ARObjectMetadata metadata in objectMetadataList)
        {
            switch (metadata.objectType)
            {
                case ARObjectType.Wall:
                    Transform wall = Instantiate(wallPrefab, metadata.position, metadata.rotation).transform;
                    wall.localScale = metadata.scale;
                    ARObjectManager.RegisterARObject(wall, ARObjectType.Wall);
                    break;
                case ARObjectType.AED:
                    Transform aed = Instantiate(aedPrefab, metadata.position, metadata.rotation).transform;
                    aed.localScale = metadata.scale;
                    ARObjectManager.RegisterARObject(aed, ARObjectType.AED);
                    break;
                case ARObjectType.Victim:
                    Transform victim = Instantiate(victimPrefab, metadata.position, metadata.rotation).transform;
                    victim.localScale = metadata.scale;
                    ARObjectManager.RegisterARObject(victim, ARObjectType.Victim);
                    break;
            }
        }
    }

    public void ResetObjects()
    {
        foreach (GameObject go in objReferencelist)
        {
            Destroy(go);
        }
        objReferencelist.Clear();
        objectDataList.Clear();
    
    }

    public float GetFloorHeight()
    {
        float lowestHeight = -1; // # 0 is where the device starts (usually not floor height, but hand height)

        if (objectDataList.Count < 1)
        {
            Debug.LogError("No objects found, object data list of ObjectManager is empty.");
            return lowestHeight;
        }


        //Get first wall height
        foreach(ARObjectMetadata metadata in objectDataList)
        {
            if (metadata.objectType == ARObjectType.Wall)
            {
                lowestHeight = metadata.position.y;
                break;
            }
        }

        //Find lowest wall height
        foreach(ARObjectMetadata metadata in objectDataList)
        {
            if (metadata.objectType == ARObjectType.Wall)
            {
                if (metadata.position.y < lowestHeight)
                {
                    lowestHeight = metadata.position.y;
                }
            }
            
        }

        return lowestHeight;
    }
}
