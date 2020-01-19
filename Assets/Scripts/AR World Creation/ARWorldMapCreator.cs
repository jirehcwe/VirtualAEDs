using UnityEngine;

public class ARWorldMapCreator : MonoBehaviour
{

    #region Public Fields
    #endregion

    #region Private Fields
    #endregion

    public void SetName(string mapName)
    {
        ARWorldMapController.currentActiveWorld = mapName;
    }
}
