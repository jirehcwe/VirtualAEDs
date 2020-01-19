using UnityEngine;

[SerializeField]
public struct ARDataPoint
{
    public ARDataPoint(Vector3 pos, Quaternion look, float time)
    {
        position = pos;
        gaze = look;
        timeStamp = time;
    }
    public Vector3 position;
    public Quaternion gaze;
    public float timeStamp;
}
