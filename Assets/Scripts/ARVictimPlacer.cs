using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ARCursor))]
public class ARVictimPlacer : MonoBehaviour
{

    #region Public Fields
    public GameObject victimPrefab;
    public UnityEvent CreateVictim;
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
            PlaceVictim();
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

    public void PlaceVictim(){
        if (cursor.position != Vector3.zero){

            Instantiate(victimPrefab, cursor.position, Quaternion.identity);
            
            if (CreateVictim != null)
            {
                CreateVictim.Invoke();
            }
            
        }
    }

    #endregion
}
