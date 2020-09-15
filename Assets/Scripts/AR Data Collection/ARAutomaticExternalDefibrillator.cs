using UnityEngine;
using System.Collections;

public class ARAutomaticExternalDefibrillator : MonoBehaviour
{

    #region Public Fields
    public float REQUIRED_PICKUP_TIME_SECONDS = 3;
    #endregion

    #region Private Fields
    private float timeInProximity;
    #endregion

    private void Update()
    {
        float scale = 0.3f + 0.7f*((REQUIRED_PICKUP_TIME_SECONDS - timeInProximity)/REQUIRED_PICKUP_TIME_SECONDS);
        transform.localScale = new Vector3(scale, scale, scale);
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.name);
        if (other.gameObject.tag == "Player")
        {
            if (timeInProximity < REQUIRED_PICKUP_TIME_SECONDS)
            {
                timeInProximity += Time.fixedDeltaTime;
            } else {
                ARDataCollectionManager.RecordDataPoint(this.transform.position, this.transform.rotation, Time.fixedTime, ARDataPoint.AREventType.AEDPickupEvent);
                timeInProximity = 0;
                Debug.Log("AED picked up");
                other.GetComponent<ARCursor>().PickUpAED();
                this.gameObject.SetActive(false);
            }
            
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        Debug.Log(other.name);
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Left AED point");
            timeInProximity = 0;       
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log(other.name);

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Entered AED point");
        }
    }

}
