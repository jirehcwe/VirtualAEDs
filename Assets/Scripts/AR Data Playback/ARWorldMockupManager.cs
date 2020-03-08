using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARKit;

public class ARWorldMockupManager : MonoBehaviour
{

    #region Public Fields
    #endregion

    #region Private Fields
    string worldPath;
    #endregion

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("OH YEAAAAH");
            RecreateWorldFromJson("Assets/World Data/Test.json");
        } else{
            Debug.Log("PRESS IT");
        }
    }

    public void RecreateWorldFromJson(string path)
    {
        ARObjectManager.Instance.GenerateARObjectsFromPath(path);
    }

    public void SetCurrentPlaybackFrame(int frame)
    {

    }

}
