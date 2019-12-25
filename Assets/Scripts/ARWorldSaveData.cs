using System.Collections.Generic;

public struct ARWorldSaveData
{
    public string worldMapName;
    public List<ARObjectMetadata> ARObjectList;

    public ARWorldSaveData(string mapName, List<ARObjectMetadata> objList)
    {
        worldMapName = mapName;
        ARObjectList = new List<ARObjectMetadata>(objList);
    }
}
