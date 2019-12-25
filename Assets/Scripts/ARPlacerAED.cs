using UnityEngine;

[RequireComponent(typeof(ARCursor))]
public class ARPlacerAED : MonoBehaviour
{

    #region Public Fields
    public GameObject aedPrefab;
    public static ARObjectPlacedEvent PlaceAED = new ARObjectPlacedEvent();
    #endregion

    #region Private Fields
    ARCursor cursor;
    #endregion

    void Start()
    {
        AEDPlacementSetup();
        cursor = GetComponent<ARCursor>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)){
            InstantiateAED();
        }
    }

    #region AED Placement Methods

    void AEDPlacementSetup()
    {
        if (aedPrefab == null){
            Debug.LogError("AED Prefab not found!");
            aedPrefab = GameObject.CreatePrimitive(PrimitiveType.Cube);
        }
    }

    public void InstantiateAED(){
        if (cursor.other != null)
        {
            GameObject newAED = Instantiate(aedPrefab, cursor.position, Quaternion.identity);

            // if (Vector3.Dot(cursor.other.right, this.transform.forward) > 0){
                
            //     newAED.transform.forward = newAED.transform.TransformVector(-cursor.other.InverseTransformVector(-cursor.other.forward));

            // }else
            // {
            //     newAED.transform.forward = newAED.transform.TransformVector(cursor.other.InverseTransformVector(-cursor.other.forward));
            // }
            newAED.transform.LookAt(newAED.transform.position - cursor.hitNormal);
            
            if (PlaceAED != null)
            {
                print("invoking AED event");
                PlaceAED.Invoke(newAED.transform, ARObjectType.AED);
            }
        }
        
    }

    #endregion
}
