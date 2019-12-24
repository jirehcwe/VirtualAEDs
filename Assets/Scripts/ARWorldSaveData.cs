using System.Collections.Generic;

public class ARWorldSaveData
{
    public string worldMapName;
    public List<ARObjectMetadata> ARObjectList = new List<ARObjectMetadata>();

    public ARWorldSaveData(string mapName, List<ARObjectMetadata> objList)
    {
        worldMapName = mapName;
        ARObjectList = objList;
    }

    public ARWorldSaveData()
    {
        worldMapName = null;
        ARObjectList = new List<ARObjectMetadata>();
    }
}
