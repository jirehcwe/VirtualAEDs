using UnityEngine;

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
    }

    public void ToggleCardiacArrest(){
        victimAnimator.SetBool("doCardiacArrest", !victimAnimator.GetBool("doCardiacArrest"));
    }
    
    private Mesh CombineMeshes()
    {   
        foreach (SkinnedMeshRenderer skinnedMeshRenderer in this.transform.GetChild(0).GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            //TODO: get mesh and combine.
        }

        return null;
    }
}
