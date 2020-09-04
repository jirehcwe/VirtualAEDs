using UnityEngine;
using UnityEngine.UI;

public class ARWorldMapUIElement : MonoBehaviour
{

    public string world;

    public Button worldDeleteButton;
    public Button worldSelectButton;
    public bool isExperimenting = false;

    private ARWorldMapController mapController;
    private ARWorldMapListDisplay worldMapListDisplay;
    

    void Start()
    {
        mapController = FindObjectOfType<ARWorldMapController>();

        if (mapController == null)
        {
            Debug.LogError("No Map Controller Found!");
        }

        worldMapListDisplay = FindObjectOfType<ARWorldMapListDisplay>();
    }

    public void LoadWorld()
    {
        ARWorldMapController.currentActiveWorld = world;
        mapController.OnLoadButton(isExperimenting);
    }
    
    public void DeleteWorld()
    {
        ARSaveDataSystemIO.DeleteWorld(world);
    }

    public void RefreshList()
    {
        bool isEditing = true;
        if (worldMapListDisplay)
        {
            worldMapListDisplay.DisplayAllWorldMaps(isEditing);
        } else
        {
            Debug.LogError("No world map list list display class found, can't refresh map list!");
        }
        
    }
}
