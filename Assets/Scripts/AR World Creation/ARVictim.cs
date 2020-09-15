using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ARVictim : MonoBehaviour
{

    #region Public Fields
    public Animator victimAnimator;
    public float REQUIRED_PROXIMITY_TIME_SECONDS = 3;
    #endregion

    #region Private Fields
    private static ARVictim instance;
    private MeshCollider meshCollider;
    private float timeInProximity;
    
    #endregion

    void Start()
    {
        victimAnimator = FindObjectOfType<Animator>();
        if (victimAnimator == null)
        {
            Debug.LogError("No Animator found!");
        }

        meshCollider = GetComponent<MeshCollider>();
        if (meshCollider == null)
        {
            Debug.LogError("No mesh collider component found!");
        }
    }

    public void TriggerCardiacArrest(){
        victimAnimator.SetBool("doCardiacArrest", true);
        StartCoroutine(SetCardiacPoseMesh());
    }

    public void ToggleCardiacArrest(){
        victimAnimator.SetBool("doCardiacArrest", !victimAnimator.GetBool("doCardiacArrest"));
    }
    
    private Mesh CombineMeshes()
    {   
        SkinnedMeshRenderer[] smrArray = this.transform.GetChild(0).GetComponentsInChildren<SkinnedMeshRenderer>();

        CombineInstance[] combinedInstances = new CombineInstance[smrArray.Length];
        Matrix4x4 parentMatrix = transform.worldToLocalMatrix;
        
        for (int i = 0; i < combinedInstances.Length; i++)
        {
            Mesh bakedMesh = new Mesh();
            smrArray[i].BakeMesh(bakedMesh);

            combinedInstances[i].mesh = bakedMesh;
            combinedInstances[i].transform = parentMatrix * smrArray[i].transform.localToWorldMatrix;
        }

        Mesh combinedMesh = new Mesh();
        combinedMesh.name = "Combined Mesh";
        combinedMesh.CombineMeshes(combinedInstances, true);
        return combinedMesh;
    }

    private IEnumerator SetCardiacPoseMesh()
    {
        yield return new WaitForSeconds(3);
        meshCollider.sharedMesh = null;
        meshCollider.sharedMesh = CombineMeshes();
        print(meshCollider.bounds);
        yield return null;
    }

    private void OnTriggerStay(Collider other)
    {
        print(other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            if (other.GetComponent<ARCursor>().hasPickedUpAED && victimAnimator.GetBool("doCardiacArrest")){
                if (timeInProximity < REQUIRED_PROXIMITY_TIME_SECONDS)
                {
                    timeInProximity += Time.fixedDeltaTime;
                } else {
                    ARDataCollectionManager.RecordDataPoint(this.transform.position, this.transform.rotation, Time.fixedTime, ARDataPoint.AREventType.ReachVictimWithAEDEvent);
                    timeInProximity = 0;
                    ToggleCardiacArrest();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.tag == "Player")
        {
            timeInProximity = 0;       
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Approached victim");
        }
    }
}
