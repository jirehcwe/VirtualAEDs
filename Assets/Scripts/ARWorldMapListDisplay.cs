using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class ARWorldMapListDisplay : MonoBehaviour
{
    public Transform parentList;
    public GameObject worldSelectCanvas;
    public GameObject worldCreateCanvas;
    public GameObject worldEditCanvasStep1;
    public GameObject worldEditCanvasStep2;

    public GameObject worldButtonPrefab;

    public void DisplayAllWorldMaps()
    {
        foreach (Transform child in parentList)
        {
            Destroy(child.gameObject);
        }

        ARWorldList worldList = ARSaveDataManager.GetAllWorlds();
        if (worldList.worldNames.Count <= 0)
        {
            print("No worlds found");
            return;
        }

        foreach (string worldName in worldList.worldNames)
        {
            GameObject worldSelectButton = Instantiate(worldButtonPrefab, parentList);
            ARWorldMapUIElement worldMapUIElement = worldSelectButton.GetComponent<ARWorldMapUIElement>();
            worldMapUIElement.world = worldName;
            worldMapUIElement.worldSelectButton.GetComponent<TextMeshProUGUI>().text = worldName;
            CanvasEnableForWorldSelection(worldMapUIElement.worldDeleteButton);
        }
    }

    void CanvasEnableForWorldSelection(Button button)
    {
        button.onClick.AddListener(() => worldSelectCanvas.gameObject.SetActive(false));
        button.onClick.AddListener(() => worldCreateCanvas.gameObject.SetActive(true));
        button.onClick.AddListener(() => worldEditCanvasStep1.gameObject.SetActive(false));
        button.onClick.AddListener(() => worldEditCanvasStep2.gameObject.SetActive(true));
    }
}
