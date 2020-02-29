using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARKit;

[deprecated]
public class ARWorldMockupManager : MonoBehaviour
{

    #region Public Fields
    public ARSession m_ARSession;
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
            RecreateWorld("Assets/World Data/Test.json");
        } else{
            Debug.Log("PRESS IT");
        }
    }

    public void RecreateWorld(string worldPath)
    {
        string[] paths;
        if (!String.IsNullOrEmpty(worldPath))
        {
            paths = worldPath.Split('.');
        }     
        else
        {
            Debug.LogError("No current world name found.");
            return;
        }

        StartCoroutine(Load(paths[0]));
                
    }

    IEnumerator Load(string path)
    {
        var sessionSubsystem = (ARKitSessionSubsystem)m_ARSession.subsystem;
        if (sessionSubsystem == null)
        {
            Debug.Log("No session subsystem available. Could not load.");
            yield break;
        }

        string worldmapPath = path + ".worldmap";
        var file = File.Open(worldmapPath, FileMode.Open);
        if (file == null)
        {
            Debug.Log(string.Format("File {0} does not exist.", worldmapPath));
            yield break;
        }

        Debug.Log(string.Format("Reading {0}...", worldmapPath));

        int bytesPerFrame = 1024 * 10;
        var bytesRemaining = file.Length;
        var binaryReader = new BinaryReader(file);
        var allBytes = new List<byte>();
        while (bytesRemaining > 0)
        {
            var bytes = binaryReader.ReadBytes(bytesPerFrame);
            allBytes.AddRange(bytes);
            bytesRemaining -= bytesPerFrame;
            yield return null;
        }

        var data = new NativeArray<byte>(allBytes.Count, Allocator.Temp);
        data.CopyFrom(allBytes.ToArray());

        Debug.Log(string.Format("Deserializing to ARWorldMap...", worldmapPath));
        ARWorldMap worldMap;
        if (ARWorldMap.TryDeserialize(data, out worldMap))
        data.Dispose();
        

        if (worldMap.valid)
        {
            Debug.Log("Deserialized successfully.");
        }
        else
        {
            Debug.LogError("Data is not a valid ARWorldMap.");
            yield break;
        }

        Debug.Log("Apply ARWorldMap to current session.");
        sessionSubsystem.ApplyWorldMap(worldMap);
        // OnResetButton();
        ARObjectManager.Instance.GenerateARObjectsFromPath(path + ".json");
        
    }

}
