using UnityEngine;
using UnityEngine.UI;

public class UpdateTextFieldFromSlider : MonoBehaviour
{

    #region Public Fields
    public Slider slider;
    public Text text;
    #endregion

    public void UpdateTextfield()
    {
        text.text = slider.value + " seconds";
    }
}
