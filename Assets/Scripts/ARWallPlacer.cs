using UnityEngine;

[RequireComponent(typeof(ARCursor))]
[RequireComponent(typeof(LineRenderer))]
public class ARWallPlacer : MonoBehaviour
{

    #region Public Fields

    public GameObject wallPrefab;

    public float defaultWallHeight = 3;

    public static ARObjectPlacedEvent PlaceWall;
    #endregion

    #region Private Fields
    LineRenderer lineRend;
    ARCursor cursor;
    Transform wallParent;
    #endregion

    void Start()
    {
        WallPlacementSetup();
        cursor = GetComponent<ARCursor>();
    }

    void Update()
    {

    #if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            AddPoint();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            BuildWalls();
        }
    #endif

    }

    #region Wall Placement Methods

    void WallPlacementSetup(){
        lineRend = GetComponent<LineRenderer>();
        lineRend.useWorldSpace = true;
        lineRend.positionCount = 0;
        lineRend.startWidth = 0.1f;
        lineRend.endWidth = 0.1f;
    }

    public void AddPoint()
    {
        lineRend.positionCount++;
        if (cursor.position != Vector3.zero)
        {
            lineRend.SetPosition(lineRend.positionCount - 1, cursor.position);
        } else {
            // no cursor position found, default is zero. do not add.
        }
    }

    public void BuildWalls()
    {
        if (wallParent == null)
        {
            wallParent = new GameObject("Walls").transform;
        }

        for (int point = 0; point < lineRend.positionCount-1 ; point++)
        {
            InstantiateWall(lineRend.GetPosition(point), lineRend.GetPosition(point + 1), wallParent);
        }
    }

    void InstantiateWall(Vector3 start, Vector3 end, Transform parent = null)
    {
        // could have used transform.forward etc tbh...
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


        if (PlaceWall != null)
        {
            PlaceWall.Invoke(wallT, ARObjectType.Wall);
        }
    }
    #endregion


}
