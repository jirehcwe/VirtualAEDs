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

        ARWorldList worldList = ARSaveDataManager.GetWorldList();
        if (worldList.worldNames.Count <= 0)
        {
            print("No worlds found");
            GameObject worldSelectButton = Instantiate(worldButtonPrefab, parentList);
            ARWorldMapUIElement worldMapUIElement = worldSelectButton.GetComponent<ARWorldMapUIElement>();
            worldMapUIElement.worldSelectButton.GetComponentInChildren<TextMeshProUGUI>().text = "NO WORLDS FOUND";
             worldMapUIElement.worldSelectButton.interactable = false;
            Destroy(worldMapUIElement.worldDeleteButton.gameObject);
            return;
        }

        foreach (string worldName in worldList.worldNames)
        {
            GameObject worldSelectButton = Instantiate(worldButtonPrefab, parentList);
            ARWorldMapUIElement worldMapUIElement = worldSelectButton.GetComponent<ARWorldMapUIElement>();
            worldMapUIElement.world = worldName;
            worldMapUIElement.worldSelectButton.GetComponentInChildren<TextMeshProUGUI>().text = worldName;
            CanvasEnableForWorldSelection(worldMapUIElement.worldSelectButton);
        }
    }

    void CanvasEnableForWorldSelection(Button button)
    {
        button.onClick.AddListener(() => worldCreateCanvas.SetActive(true));
        button.onClick.AddListener(() => worldEditCanvasStep1.SetActive(false));
        button.onClick.AddListener(() => worldEditCanvasStep2.SetActive(true));
        button.onClick.AddListener(() => worldSelectCanvas.SetActive(false));
        
        button.onClick.AddListener(() => print("enabling other canvases"));
    }
}
