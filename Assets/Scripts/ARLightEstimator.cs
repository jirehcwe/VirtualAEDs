using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARLightEstimator : MonoBehaviour
{
    public List<Light> lightList;

    [SerializeField]
    [Tooltip("The ARCameraManager which will produce frame events containing light estimation information.")]
    ARCameraManager m_CameraManager;

    /// <summary>
    /// Get or set the <c>ARCameraManager</c>.
    /// </summary>
    public ARCameraManager cameraManager
    {
        get { return m_CameraManager; }
        set
        {
            if (m_CameraManager == value)
                return;

            if (m_CameraManager != null)
                m_CameraManager.frameReceived -= FrameChanged;

            m_CameraManager = value;

            if (m_CameraManager != null & enabled)
                m_CameraManager.frameReceived += FrameChanged;
        }
    }

    public float? brightness { get; private set; }

    public float? colorTemperature { get; private set; }

    public Color? colorCorrection { get; private set; }

    void Awake ()
    {
        if (lightList == null)
        {
            Debug.LogError("No Light object found!");
        }

        lightList = this.transform.GetComponentsInChildren<Light>().OfType<Light>().ToList();
    }

    void OnEnable()
    {
        if (m_CameraManager != null)
            m_CameraManager.frameReceived += FrameChanged;
    }

    void OnDisable()
    {
        if (m_CameraManager != null)
            m_CameraManager.frameReceived -= FrameChanged;
    }

    void FrameChanged(ARCameraFrameEventArgs args)
    {
        foreach (Light light in lightList)
        {
            if (args.lightEstimation.averageBrightness.HasValue)
            {
                brightness = args.lightEstimation.averageBrightness.Value;
                light.intensity = brightness.Value;
            }

            if (args.lightEstimation.averageColorTemperature.HasValue)
            {
                colorTemperature = args.lightEstimation.averageColorTemperature.Value;
                light.colorTemperature = colorTemperature.Value;
            }
            
            if (args.lightEstimation.colorCorrection.HasValue)
            {
                colorCorrection = args.lightEstimation.colorCorrection.Value;
                light.color = colorCorrection.Value;
            }
        }
    }
}