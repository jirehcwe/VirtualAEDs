using System.Collections.Generic;

[System.Serializable]
public struct ARWorldSaveData
{
    public string worldMapName;
    public ARObjectMetadata[] ARObjectList;

    public ARWorldSaveData(string mapName, ARObjectMetadata[] objArray)
    {
        worldMapName = mapName;
        ARObjectList = (ARObjectMetadata[])objArray.Clone();
    }
}
