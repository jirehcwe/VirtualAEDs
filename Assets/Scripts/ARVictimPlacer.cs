using UnityEngine;

public class ARVictimPlacer : MonoBehaviour
{

    #region Public Fields
    public GameObject victimPrefab;
    #endregion

    #region Private Fields
    ARCursor cursor;
    #endregion

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    #region Victim Placement Methods

    void VictimPlacementSetup()
    {
        if (victimPrefab == null){
            Debug.LogError("Victim Prefab not found!");
            victimPrefab = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        }
    }

    #endregion
}
