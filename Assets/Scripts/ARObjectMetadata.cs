using UnityEngine;

public class ARObjectMetadata : ScriptableObject
{
    public Transform transformData;
    public ARObjectType objectType;

    public ARObjectMetadata(Transform t, ARObjectType objType)
    {
        transformData = t;
        objectType = objType;
    }
}
