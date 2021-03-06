﻿using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ARCursor : MonoBehaviour
{
    #region Public Fields
    public GameObject cursorPrefab;
    public float maxCursorRange = 20;
    public bool hasPickedUpAED
    {
        get; private set;
    }
    public Vector3 position;
    public Transform other;
    public Vector3 hitNormal;
    #endregion

    #region Private Fields
    Transform cursorInstanceTransform;
    bool isRecording = false;
    #endregion

    public void OnDisable()
    {
        ARDataCollectionManager.StartDataRecording.RemoveListener(StartRecordCursor);
        ARDataCollectionManager.StopDataRecording.RemoveListener(EndRecordCursor);
    }
    

    void Start()
    {
        SetupCursorObject();
        ARDataCollectionManager.StartDataRecording.AddListener(StartRecordCursor);
        ARDataCollectionManager.StopDataRecording.AddListener(EndRecordCursor);
    }

    public void PickUpAED()
    {
        hasPickedUpAED = true;
    }

    public void UseUpAED()
    {
        hasPickedUpAED = false;
    }

    void Update()
    {
        UpdateCursorPosition();
    }

    private void FixedUpdate()
    {
        if (isRecording)
        {
            //TODO: add event types
            ARDataCollectionManager.RecordDataPoint(transform.position, transform.rotation, Time.fixedTime, ARDataPoint.AREventType.NullEvent);
        }
    }

    void UpdateCursorPosition(){
        RaycastHit hit;
        Ray cameraRay = new Ray(this.transform.position, this.transform.forward);
        Debug.DrawRay(cameraRay.origin, cameraRay.direction, Color.green);
        if (Physics.Raycast(cameraRay, out hit, maxCursorRange))
        {
            hitNormal = hit.normal;
            position = hit.point;
            if (cursorInstanceTransform == null)
            {
                SetupCursorObject(hit.point.x, hit.point.y, hit.point.z);
            } else
            {
                cursorInstanceTransform.position = hit.point;
            }

            //Assigning hit object's transform to other
            other = hit.transform;
            
        } else
        {
            if (cursorInstanceTransform != null)
            {
                Destroy(cursorInstanceTransform.gameObject);
            }

            //Dereferencing other transform
            other = null;
            position = Vector3.zero;
        }
    }

    void SetupCursorObject(float x = 0, float y = 0, float z = 0){
        if (cursorPrefab == null)
        {
            cursorInstanceTransform = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
            Destroy(cursorInstanceTransform.GetComponent<Collider>());
        } else
        {
            cursorInstanceTransform = Instantiate(cursorPrefab, new Vector3(x, y, z), Quaternion.identity).transform;
        }
        cursorInstanceTransform.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    void StartRecordCursor()
    {
        print("recording from cursor");
        isRecording = true;
    }

    void EndRecordCursor()
    {
        print("end recording from cursor");
        isRecording = false;
    }


}
