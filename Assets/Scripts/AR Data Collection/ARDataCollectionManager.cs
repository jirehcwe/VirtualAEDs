using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class ARDataCollectionManager : MonoBehaviour
{

    #region Public Fields
    public static ARDataCollectionManager Instance;
    public static bool IsRecording = false;
    public static UnityEvent StartDataRecording = new UnityEvent();
    public static UnityEvent StopDataRecording = new UnityEvent();
    public static string WorldName = null;

    public bool isDiscreteSaveOperation = true;
    #endregion

    #region Private Fields
    static List<ARDataPoint> dataPoints;
    #endregion

    void Start()
    {
        if (Instance != null || Instance != this)
        {
            Destroy(this);
        }else
        {
            Instance = this;
        }

        IsRecording = false;
    }

    void Update()
    {
        
    }

    public static void RecordDataPoint(Vector3 position, Quaternion gaze, float timeStamp)
    {
        if (string.IsNullOrEmpty(ARWorldMapController.currentActiveWorld))
        {
            Debug.LogError("World name is null or missing!");
            return;
        }else
        {
            WorldName = ARWorldMapController.currentActiveWorld;
        }

        ARDataPoint point = new ARDataPoint(position, gaze, timeStamp);
        dataPoints.Add(point);


        if (Instance.isDiscreteSaveOperation)
        {
            PushDataPointToFile(WorldName, point);
            dataPoints.Clear();
        } else if (dataPoints.Count > 1/Time.fixedDeltaTime)
        {
            Debug.LogError("NOT DONE YET");
            ARDataPoint[] copy = new ARDataPoint[dataPoints.Count];
            dataPoints.CopyTo(copy);
            PushDataListToFile(WorldName, copy);
            dataPoints.Clear();
        }

    }
    
    public void StartRecording()
    {
        StartDataRecording.Invoke();
    }

    public void StopRecording()
    {
        StopDataRecording.Invoke();
    }

    public static void PushDataListToFile(string worldName, ARDataPoint[] datapoints)
    {

    }

    public static void PushDataPointToFile(string worldName, ARDataPoint dataPoint)
    {
        ARSaveDataManager.SaveDataPoint(worldName, dataPoint);
    }
    
}
