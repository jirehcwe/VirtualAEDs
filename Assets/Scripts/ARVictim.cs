using UnityEngine;

public class ARVictim : MonoBehaviour
{

    #region Public Fields
    public Animator victimAnimator;
    public static ARVictim Instance
    {
        get { return instance;}
    }
    #endregion

    #region Private Fields
    private static ARVictim instance;
    #endregion

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }

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
