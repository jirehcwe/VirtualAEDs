using UnityEngine;

public class ARVictim : MonoBehaviour
{

    #region Public Fields
    public Animator victimAnimator;

    #endregion

    #region Private Fields
    private static ARVictim instance;
    #endregion

    void Start()
    {
        victimAnimator = FindObjectOfType<Animator>();
        if (victimAnimator == null)
        {
            Debug.LogError("No Animator found!");
        }
    }

    public void TriggerCardiacArrest(){
        victimAnimator.SetBool("doCardiacArrest", true);
    }

    public void ToggleCardiacArrest(){
        victimAnimator.SetBool("doCardiacArrest", !victimAnimator.GetBool("doCardiacArrest"));
    }
    
}
