using UnityEngine;

[RequireComponent(typeof(ARCursor))]
[RequireComponent(typeof(LineRenderer))]
public class ARWallPlacer : MonoBehaviour
{

    #region Public Fields
    public GameObject wallPrefab;
    public float defaultWallHeight = 3;
    #endregion

    #region Private Fields
    private LineRenderer lineRend;
    private ARCursor cursor;
    private Transform wallParent;
    #endregion

    void Start()
    {
        lineRend = GetComponent<LineRenderer>();
        lineRend.useWorldSpace = true;
        lineRend.positionCount = 0;

        cursor = GetComponent<ARCursor>();
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            AddPoint(lineRend, cursor.position);
        }
#endif


#if UNITY_IOS
        if (Input.touchCount > 0)
        {
            print("iPhone Test");
        }
#endif

    if (Input.GetKeyDown(KeyCode.Space)){
        BuildWalls(lineRend);
    }

    }

    void AddPoint(LineRenderer lineRenderer, Vector3 position)
    {
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, position);
    }

    public void BuildWalls(LineRenderer lineRenderer)
    {
        if (wallParent == null)
        {
            wallParent = new GameObject("Walls").transform;
        }

        for (int point = 0; point < lineRenderer.positionCount-1 ; point++)
        {
            PlaceWall(lineRenderer.GetPosition(point), lineRenderer.GetPosition(point + 1), wallParent);
        }
    }

    void PlaceWall(Vector3 start, Vector3 end, Transform parent = null)
    {
        Vector3 midpoint = (start+end)/2;
        float xDelta = start.x - end.x;
        float zDelta = start.z - end.z;
        float wallAngle = Mathf.Atan(zDelta/xDelta)*180/Mathf.PI;
        if (xDelta < 0)
        {
            if (zDelta > 0)
            {
                wallAngle = -wallAngle + 180;
            }
            else
            {
                wallAngle = 180 - wallAngle;
            }
        }
        else
        {
                wallAngle = -wallAngle;
        }
        Quaternion wallRotation = Quaternion.Euler(0, wallAngle, 0);
        Transform wallT = Instantiate(wallPrefab, midpoint, wallRotation, parent).transform;
        float length = (start-end).magnitude;
        wallT.localScale = new Vector3(length, defaultWallHeight, length);
    }
    
}
