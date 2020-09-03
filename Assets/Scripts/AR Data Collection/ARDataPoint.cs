using System;
using UnityEngine;

[Serializable]
public struct ARDataPoint
{
    public Vector3 position;
    public Quaternion gaze;
    public float timeStamp;
    public AREventType arEventType;

    public ARDataPoint(Vector3 pos, Quaternion look, float time, AREventType newEvent)
    {
        position = pos;
        gaze = look;
        timeStamp = time;
        arEventType = newEvent;
    }

    public void PrintSelf()
    {
        Debug.Log("Timestamp: " + timeStamp + " | Position: " + position + " | Rot: " + gaze + 
                  " | Event: " + Enum.GetName(typeof(AREventType), arEventType));
    }

    public enum AREventType
    {
        NullEvent = 0,
        CardiacArrestEvent = 1,
        AEDPickupEvent = 2,
        ReachVictimWithAEDEvent = 3,
    };

}
