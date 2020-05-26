using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ARTimelineScrubber : MonoBehaviour
{

    #region Public Fields
    #endregion

    #region Private Fields
    Slider timelineSlider;
    bool shouldPlay = false;
    #endregion

    void Awake()
    {
        timelineSlider = this.GetComponent<Slider>();
    }

    void Start()
    {
        timelineSlider.wholeNumbers = true;
        timelineSlider.value = 0;
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (shouldPlay)
        {
            timelineSlider.value = Mathf.Min(timelineSlider.value + 1, timelineSlider.maxValue);
        }
    }

    public void SetSliderNumPoints(int numDataPoints)
    {
        timelineSlider.maxValue = numDataPoints - 1; //0-indexed
        timelineSlider.minValue = 0;
    }

    public void RegisterOnValueChanged(UnityAction<float> action) 
    {
        timelineSlider.onValueChanged.AddListener(action);
    }

    public void UnregisterOnValueChanged(UnityAction<float> action) 
    {
        timelineSlider.onValueChanged.RemoveListener(action);
    }

    public void TogglePlayPause()
    {
        shouldPlay = !shouldPlay;
    }

}
