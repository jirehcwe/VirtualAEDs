using UnityEngine;

[SerializeField]
public struct ARDataPoint
{
    public Vector3 position;
    public Quaternion gaze;
    public float timeStamp;
}
