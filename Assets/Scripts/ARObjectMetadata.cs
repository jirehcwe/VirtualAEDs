using UnityEngine;

[System.Serializable]
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

    public ARObjectMetadata Copy()
    {
        ARObjectMetadata clone = new ARObjectMetadata();
        clone.objectType = this.objectType;
        clone.position = new Vector3(this.position.x, this.position.y, this.position.z);
        clone.rotation = new Quaternion(this.rotation.x, this.rotation.y, this.rotation.z, this.rotation.w);
        clone.scale = new Vector3(this.scale.x, this.scale.y, this.scale.z);
        return clone;
    }
}
