using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class ARWorldMapListDisplay : MonoBehaviour
{
    public Transform worldEditButtonList;
    public GameObject worldSelectCanvas;
    public GameObject worldCreateCanvas;
    public GameObject worldEditCanvasStep1;
    public GameObject worldEditCanvasStep2;

    public Transform experimentButtonList;
    public GameObject experimentTestingCanvasStep1;
    public GameObject experimentTestingCanvasStep2;


    public GameObject worldButtonPrefab;

    public void DisplayAllWorldMaps(bool isEditing)
    {
        foreach (Transform child in worldEditButtonList)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in experimentButtonList)
        {
            Destroy(child.gameObject);
        }

        ARWorldList worldList = ARSaveDataManager.GetWorldList();
        if (worldList.worldNames.Count <= 0)
        {
            print("No worlds found");
            GameObject worldSelectButton = Instantiate(worldButtonPrefab, isEditing? worldEditButtonList : experimentButtonList);
            ARWorldMapUIElement worldMapUIElement = worldSelectButton.GetComponent<ARWorldMapUIElement>();
            worldMapUIElement.worldSelectButton.GetComponentInChildren<TextMeshProUGUI>().text = "NO WORLDS FOUND";
            worldMapUIElement.worldSelectButton.interactable = false;
            Destroy(worldMapUIElement.worldDeleteButton.gameObject);
            return;
        }

        foreach (string worldName in worldList.worldNames)
        {
            GameObject worldSelectButton = Instantiate(worldButtonPrefab, isEditing? worldEditButtonList : experimentButtonList);
            ARWorldMapUIElement worldMapUIElement = worldSelectButton.GetComponent<ARWorldMapUIElement>();
            worldMapUIElement.world = worldName;
            worldMapUIElement.worldSelectButton.GetComponentInChildren<TextMeshProUGUI>().text = worldName;
            if (isEditing)
            {
                CanvasEnableForWorldEditing(worldMapUIElement.worldSelectButton);
            } else
            {
                CanvasEnableForTesting(worldMapUIElement.worldSelectButton);
            }
        }
    }

    void CanvasEnableForWorldEditing(Button button)
    {
        button.onClick.AddListener(() => worldCreateCanvas.SetActive(true));
        button.onClick.AddListener(() => worldEditCanvasStep1.SetActive(false));
        button.onClick.AddListener(() => worldEditCanvasStep2.SetActive(true));
        button.onClick.AddListener(() => worldSelectCanvas.SetActive(false));
        
        button.onClick.AddListener(() => print("enabling other canvases for editing"));
    }

    void CanvasEnableForTesting(Button button)
    {
        button.onClick.AddListener(() => experimentTestingCanvasStep2.SetActive(true));
        button.onClick.AddListener(() => experimentTestingCanvasStep1.SetActive(false));

        button.onClick.AddListener(() => print("enabling other canvases for running experiments"));
    }
}
