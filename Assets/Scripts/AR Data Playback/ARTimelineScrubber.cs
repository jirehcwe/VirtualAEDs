using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

public class ARTimelineScrubber : MonoBehaviour
{

    #region Public Fields
    #endregion

    #region Private Fields
    Slider timelineSlider;
    GameObject eventButtonPrefab;
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

        eventButtonPrefab = Resources.Load<GameObject>("Event Button");
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

    public void SetupSlider(List<(int index, ARDataPoint.AREventType eventType)> eventList, int numDataPoints)
    {
        RectTransform rectTrans = this.GetComponent<RectTransform>();
        float xStartPos = eventButtonPrefab.GetComponent<RectTransform>().rect.width/2;
        float xEndPos = rectTrans.rect.width - xStartPos;
        float yPos = rectTrans.rect.height * 1.5f;
        // Do markers.
        foreach(var arEvent in eventList)
        {   
            switch(arEvent.eventType)
            {
                case ARDataPoint.AREventType.NullEvent:
                    break;
                case ARDataPoint.AREventType.CardiacArrestEvent:
                    GameObject eventButton = Instantiate(eventButtonPrefab, this.transform);
                    RectTransform buttonRect = eventButton.GetComponent<RectTransform>();
                    float relativeXpos = xStartPos + (xEndPos-xStartPos)*((float)arEvent.index/(float)numDataPoints);
                    buttonRect.anchoredPosition = new Vector2(relativeXpos, yPos);
                    eventButton.GetComponent<Button>().onClick.AddListener(delegate
                                                                                    {
                                                                                        timelineSlider.value = arEvent.index;
                                                                                    }
                                                                            );
                    break;
                case ARDataPoint.AREventType.AEDPickupEvent:
                    break;
                case ARDataPoint.AREventType.ReachVictimWithAEDEvent:
                    break;
            }
        }
        SetSliderNumPoints(numDataPoints);
        
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
