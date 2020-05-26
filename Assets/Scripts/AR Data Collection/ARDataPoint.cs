using System;
using UnityEngine;

[Serializable]
public struct ARDataPoint
{
    public ARDataPoint(Vector3 pos, Quaternion look, float time)
    {
        position = pos;
        gaze = look;
        timeStamp = time;
    }

    public void PrintSelf()
    {
        Debug.Log("Timestamp: " + timeStamp + " | Position: " + position + " | Rot: " + gaze);
    }
    public Vector3 position;
    public Quaternion gaze;
    public float timeStamp;
}
