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
    static List<ARDataPoint> dataPoints = new List<ARDataPoint>();
    #endregion

    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }else
        {
            Instance = this;
        }

        IsRecording = false;
    }

    public static void RecordDataPoint(Vector3 position, Quaternion gaze, float timeStamp, ARDataPoint.AREventType newEvent)
    {
        if (IsRecording)
        {
            if (string.IsNullOrEmpty(ARWorldMapController.currentActiveWorld))
            {
                Debug.LogError("World name is null or missing!");
                return;
            } else {
                WorldName = ARWorldMapController.currentActiveWorld;
            }

            ARDataPoint point = new ARDataPoint(position, gaze, timeStamp, newEvent);
        
            if (Instance.isDiscreteSaveOperation)
            {
                PushDataPointToFile(WorldName, point);
            } else if (dataPoints.Count > 1/Time.fixedDeltaTime)
            {
                Debug.LogError("NOT DONE YET");
                ARDataPoint[] copy = new ARDataPoint[dataPoints.Count];
                dataPoints.CopyTo(copy);
                PushDataListToFile(WorldName, copy);
                dataPoints.Clear();
            } else {
                dataPoints.Add(point);
            }
        }
        

    }
    
    public void StartRecording()
    {
        print("invoking " + StartDataRecording.GetPersistentEventCount() + " start events");
        IsRecording = true;
        StartDataRecording.Invoke();
    }

    public void StopRecording()
    {
        print("invoking " + StopDataRecording.GetPersistentEventCount() + " stop events");
        IsRecording = false;
        StopDataRecording.Invoke();
    }

    public static void PushDataListToFile(string worldName, ARDataPoint[] datapoints)
    {

    }

    public static void PushDataPointToFile(string worldName, ARDataPoint dataPoint)
    {
        ARSaveDataSystemIO.SaveDataPoint(worldName, dataPoint);
    }
    
}
