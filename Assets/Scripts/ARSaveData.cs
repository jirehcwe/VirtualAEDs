using System.Collections.Generic;

public class ARSaveData
{
    public string worldMapName;
    public List<ARObjectMetadata> ARObjectList = new List<ARObjectMetadata>();

    public ARSaveData(string mapName, List<ARObjectMetadata> objList)
    {
        worldMapName = mapName;
        ARObjectList = objList;
    }
}
