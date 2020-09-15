using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

public class ARTimelineScrubber : MonoBehaviour
{

    #region Public Fields
        public Color cardiacArrestEventColor;
        public Color pickupAEDEventColor;
        public Color reachVictimWithAEDEventColor;
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
        

        foreach(var arEvent in eventList)
        {   
            if (arEvent.eventType != ARDataPoint.AREventType.NullEvent)
            {
                float relativeXpos = xStartPos + (xEndPos-xStartPos)*((float)arEvent.index/(float)numDataPoints);
                CreateEventButton(arEvent.index, arEvent.eventType, relativeXpos, yPos);
            }
        }
        SetSliderNumPoints(numDataPoints);
    }

    public void SetSliderNumPoints(int numDataPoints)
    {
        timelineSlider.maxValue = numDataPoints - 1;
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

    private void CreateEventButton(int eventIndex, ARDataPoint.AREventType eventType, float relativeXpos, float relativeYpos)
    {
        GameObject eventButton = Instantiate(eventButtonPrefab, this.transform);
        RectTransform buttonRect = eventButton.GetComponent<RectTransform>();
        buttonRect.anchoredPosition = new Vector2(relativeXpos, relativeYpos);
        eventButton.GetComponent<Button>().onClick.AddListener(delegate
                                                                        {
                                                                            timelineSlider.value = eventIndex;
                                                                        }
                                                                );
        Image eventButtonImage = eventButton.GetComponent<Image>();    

        switch(eventType)
        {
            case ARDataPoint.AREventType.CardiacArrestEvent:
                eventButtonImage.color = cardiacArrestEventColor;
                break;
            case ARDataPoint.AREventType.AEDPickupEvent:
                eventButtonImage.color = pickupAEDEventColor;
                break;
            case ARDataPoint.AREventType.ReachVictimWithAEDEvent:
                eventButtonImage.color = reachVictimWithAEDEventColor;
                break;
                    
        }
    }

}
