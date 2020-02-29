using UnityEngine;
using UnityEngine.UI;

public class ARWorldMapUIElement : MonoBehaviour
{

    public string world;

    public Button worldDeleteButton;
    public Button worldSelectButton;

    private ARWorldMapController mapController;
    

    void Start()
    {
        mapController = FindObjectOfType<ARWorldMapController>();

        if (mapController == null)
        {
            Debug.LogError("No Map Controller Found!");
        }
    }

    public void LoadWorld()
    {
        ARWorldMapController.currentActiveWorld = world;
        mapController.OnLoadButton();
    }
    
    public void DeleteWorld()
    {
        ARSaveDataSystemIO.DeleteWorld(world);
    }
}
