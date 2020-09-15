using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ARVictim : MonoBehaviour
{

    #region Public Fields
    public Animator victimAnimator;

    #endregion

    #region Private Fields
    private static ARVictim instance;
    private MeshCollider meshCollider;
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
        
        for (int i = 0; i < combinedInstances.Length; i++)
        {
            Mesh bakedMesh = new Mesh();
            smrArray[i].BakeMesh(bakedMesh);

            combinedInstances[i].mesh = bakedMesh;
            combinedInstances[i].transform = smrArray[i].transform.localToWorldMatrix;
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
        yield return null;
    }
}
