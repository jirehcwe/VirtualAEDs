using System.Collections.Generic;

[System.Serializable]
public struct ARWorldSaveData
{
    public string worldMapName;
    public ARObjectMetadata[] ARObjectList;
    public int sessionsRecorded;
    public int victimCardiacArrestWaitTime;

    public ARWorldSaveData(string mapName, ARObjectMetadata[] objArray, int heartAttackTime)
    {
        worldMapName = mapName;
        ARObjectList = (ARObjectMetadata[])objArray.Clone();
        sessionsRecorded = 0;
        victimCardiacArrestWaitTime = heartAttackTime;
    }

    public ARWorldSaveData(string mapName, ARObjectMetadata[] objArray)
    {
        worldMapName = mapName;
        ARObjectList = (ARObjectMetadata[])objArray.Clone();
        sessionsRecorded = 0;
        victimCardiacArrestWaitTime = 5;
    }
}
