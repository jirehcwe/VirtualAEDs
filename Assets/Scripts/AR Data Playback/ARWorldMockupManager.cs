using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR;

public class ARWorldMockupManager : MonoBehaviour
{

    #region Public Fields
    public ARTimelineScrubber timelineScrubber;
    public Transform floorPlane;
    #endregion

    #region Private Fields
    string worldPath;
    List<ARDataPoint> dataPoints;
    GameObject headsetPrefab;
    GameObject headsetInstance;
    ARVictim victimInstance;
    #endregion

    private void OnDisable()
    {
        timelineScrubber.UnregisterOnValueChanged(ScrubToCurrentDataPoint);
    }

    void Start()
    {
        timelineScrubber.RegisterOnValueChanged(ScrubToCurrentDataPoint);
        headsetPrefab = Resources.Load<GameObject>("Headset Root");
        headsetInstance = Instantiate(headsetPrefab);
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     Debug.Log("OH YEAAAAH");
        //     RecreateWorldFromJson("Assets/World Data/Test.json");
        // } else{
        //     Debug.Log("PRESS SPACE");
        // }
    }

    public void RecreateWorldFromJson(string path)
    {
        ARObjectManager.Instance.GenerateARObjectsFromPath(path);
    }

    public void SetCurrentPlaybackFrame(int frame)
    {

    }

    public void LoadWorldTimelineFromPath(string path)
    {
        dataPoints = ARSaveDataSystemIO.GetDataPointsByPath(path);
        timelineScrubber.SetupSlider(GetEventList(dataPoints), dataPoints.Count);
        ScrubToCurrentDataPoint(0);
        print("Floor height: " + ARObjectManager.Instance.GetFloorHeight());
        floorPlane.position = new Vector3(floorPlane.position.x, ARObjectManager.Instance.GetFloorHeight(), floorPlane.position.z);
        victimInstance = ARObjectManager.objReferencelist.Find( obj => obj.GetComponent<ARVictim>() != null).GetComponent<ARVictim>();
    }

    public void ScrubToCurrentDataPoint(float datapointIndex)
    {
        ARDataPoint point = dataPoints[(int)datapointIndex];
        switch (point.arEventType)
        {
            case ARDataPoint.AREventType.NullEvent:
                headsetInstance.transform.position = point.position;
                headsetInstance.transform.rotation = point.gaze;
                break;
            case ARDataPoint.AREventType.CardiacArrestEvent:
                victimInstance.TriggerCardiacArrest();
                break;
            case ARDataPoint.AREventType.AEDPickupEvent:
                break;
            case ARDataPoint.AREventType.ReachVictimWithAEDEvent:
                break;
                    
        }
    }

    private List<(int, ARDataPoint.AREventType)> GetEventList(List<ARDataPoint> dataPoints)
    {
        return dataPoints.Select((point, index) => (index, point.arEventType))
                         .Where( tuple => tuple.arEventType != ARDataPoint.AREventType.NullEvent)
                         .ToList();
        
    }
}
