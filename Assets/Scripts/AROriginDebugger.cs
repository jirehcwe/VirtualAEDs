using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class AROriginDebugger : MonoBehaviour
{

    #region Public Fields
    public TextMeshProUGUI debugText;
    public ARSessionOrigin origin;
    #endregion

    #region Private Fields
    #endregion

    void Start()
    {
        
    }

    void Update()
    {
        debugText.text = origin.transform.position.ToString();
    }
}
