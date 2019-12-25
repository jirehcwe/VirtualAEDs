using UnityEngine;

public struct ARObjectMetadata
{
    public Quaternion rotation;
    public Vector3 position;
    public Vector3 scale;
    public ARObjectType objectType;

    public ARObjectMetadata(Transform t, ARObjectType objType)
    {
        rotation = new Quaternion(t.rotation.x, t.rotation.y, t.rotation.z, t.rotation.w);
        position = new Vector3(t.position.x, t.position.y, t.position.z);
        scale = new Vector3(t.lossyScale.x, t.lossyScale.y, t.lossyScale.z);
        objectType = objType;
    }
}
