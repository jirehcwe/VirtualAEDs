using UnityEngine;

public class ARWorldMapCreator : MonoBehaviour
{

    #region Public Fields
    public ARWorldMapController mapController;
    #endregion

    #region Private Fields
    #endregion

    public void SetName(string mapName)
    {
        mapController.currentActiveMap = mapName;
    }
}
