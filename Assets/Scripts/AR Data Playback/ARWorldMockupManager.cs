using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        timelineScrubber.SetSliderNumPoints(dataPoints.Count);
        ScrubToCurrentDataPoint(0);
        print("Floor height: " + ARObjectManager.Instance.GetFloorHeight());
        floorPlane.position = new Vector3(floorPlane.position.x, ARObjectManager.Instance.GetFloorHeight(), floorPlane.position.z);
    }

    public void ScrubToCurrentDataPoint(float datapointIndex)
    {
        ARDataPoint point = dataPoints[(int)datapointIndex];
        headsetInstance.transform.position = point.position;
        headsetInstance.transform.rotation = point.gaze;
    }
}
