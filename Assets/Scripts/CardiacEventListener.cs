using UnityEngine;
using UnityEngine.UI;

public class CardiacEventListener : MonoBehaviour
{

    #region Public Fields
    public Button cardiacToggleButton;
    public Button placeVictimButton;
    #endregion

    #region Private Fields
    ARVictimPlacer victimPlacer;
    #endregion

    void Start()
    {
        victimPlacer = FindObjectOfType<ARVictimPlacer>();
        if (victimPlacer == null){
            Debug.Log("huh");
        }
        cardiacToggleButton.interactable = false;
        victimPlacer.CreateVictim.AddListener(this.RegisterCardiacToButton);
    }

    void RegisterCardiacToButton()
    {
        placeVictimButton.interactable = false;
        cardiacToggleButton.interactable = true;
        cardiacToggleButton.onClick.AddListener(ARVictim.Instance.ToggleCardiacArrest);
    }
}
