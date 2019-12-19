using UnityEngine;
using UnityEngine.UI;

public class CreatedVictimEventButtonRegistrar : MonoBehaviour
{

    #region Public Fields
    public Button cardiacToggleButton;
    public Button placeVictimButton;
    #endregion

    #region Private Fields
    #endregion
    void OnDisable()
    {
        ARVictimPlacer.PlaceVictim.RemoveListener(this.RegisterCardiacToButton);
    }

    void Start()
    {
        if (ARVictimPlacer.PlaceVictim == null){
            Debug.Log("AR Victim Placer not found.");
        }
        cardiacToggleButton.interactable = false;
        ARVictimPlacer.PlaceVictim.AddListener(this.RegisterCardiacToButton);
    }

    void RegisterCardiacToButton(Transform transform, ARObjectType objectType)
    {
        placeVictimButton.interactable = false;
        cardiacToggleButton.interactable = true;
        cardiacToggleButton.onClick.AddListener(ARVictim.Instance.ToggleCardiacArrest);
    }
}
