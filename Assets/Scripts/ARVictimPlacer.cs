using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ARCursor))]
public class ARVictimPlacer : MonoBehaviour
{

    #region Public Fields
    public GameObject victimPrefab;
    public static ARObjectPlacedEvent PlaceVictim = new ARObjectPlacedEvent();
    #endregion

    #region Private Fields
    ARCursor cursor;
    #endregion

    void Start()
    {
        cursor = this.GetComponent<ARCursor>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            InstantiateVictim();
        }
    }

    #region Victim Placement Methods

    void VictimPlacementSetup()
    {
        if (victimPrefab == null){
            Debug.LogError("Victim Prefab not found!");
            victimPrefab = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        }
    }

    public void InstantiateVictim(){
        if (cursor.position != Vector3.zero){

            GameObject victim = Instantiate(victimPrefab, cursor.position, Quaternion.identity);
            
            if (PlaceVictim != null)
            {
                print("invoking victim event");
                PlaceVictim.Invoke(victim.transform, ARObjectType.Victim);
            }
            
        }
    }

    #endregion
}
