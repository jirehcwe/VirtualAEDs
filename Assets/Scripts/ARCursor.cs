using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ARCursor : MonoBehaviour
{
    #region Public Fields
    public GameObject cursorPrefab;
    public float maxCursorRange = 20;
    #endregion

    #region Private Fields
    Transform cursorInstanceTransform;
    Camera mainCamera;
    #endregion

    void Start()
    {
        mainCamera = Camera.main;
        SetupCursorObject();
    }

    void Update()
    {
        UpdateCursorPosition();
    }

    void UpdateCursorPosition(){
        RaycastHit hit;
        Ray cameraRay = new Ray(this.transform.position, this.transform.forward);
        Debug.DrawRay(cameraRay.origin, cameraRay.direction, Color.green);
        if (Physics.Raycast(cameraRay, out hit, maxCursorRange))
        {
            if (cursorInstanceTransform == null)
            {
                SetupCursorObject(hit.point.x, hit.point.y, hit.point.z);
            } else
            {
                cursorInstanceTransform.position = hit.point;
            }
            
        } else
        {
            if (cursorInstanceTransform != null)
            {
                Destroy(cursorInstanceTransform.gameObject);
            }

        }
    }

    void SetupCursorObject(float x = 0, float y = 0, float z = 0){
        if (cursorPrefab == null)
        {
            cursorInstanceTransform = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
            Destroy(cursorInstanceTransform.GetComponent<Collider>());
        } else
        {
            cursorInstanceTransform = Instantiate(cursorPrefab, new Vector3(x, y, z), Quaternion.identity).transform;
        }
        cursorInstanceTransform.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }
}
