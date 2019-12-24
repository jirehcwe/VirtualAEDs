using UnityEngine;
using System.Collections.Generic;

public class ARObjectManager : MonoBehaviour
{


    #region Public Fields
    public static List<ARObjectMetadata> objectList = new List<ARObjectMetadata>();
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

    void Update()
    {
        
    }

    void RegisterARObject(Transform objectTransform, ARObjectType objectType){
        ARObjectMetadata newObjectData = new ARObjectMetadata(objectTransform, objectType);
        objectList.Add(newObjectData);
    }

    void RemoveARObject(){
    }

    public static void LoadObjectsFromSaveData(ARWorldSaveData saveData){
        List<ARObjectMetadata> objList = saveData.ARObjectList;
        foreach(ARObjectMetadata metadata in objList)
        {
            //Instantiate them
        }
    }
}
