using UnityEngine;
using System.Collections.Generic;

public class ARObjectManager : MonoBehaviour
{


    #region Public Fields
    public static List<ARObjectMetadata> objectDataList = new List<ARObjectMetadata>();
    public static List<GameObject> objReferencelist = new List<GameObject>();
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
        objectDataList.Add(newObjectData);
        objReferencelist.Add(objectTransform.gameObject);
    }

    void RemoveARObject(){
    }

}
