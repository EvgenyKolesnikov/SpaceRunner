using UnityEngine;
using Unity.Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }
    private CinemachineBasicMultiChannelPerlin noise;
    private float shakeTimer;
    private float startingIntensity;
    private float shakeTimerTotal;

    private void Awake()
    {
        Instance = this;
        var vcam = GetComponent<CinemachineVirtualCameraBase>();
        if (vcam != null)
            noise = vcam.GetCinemachineComponent(CinemachineCore.Stage.Noise) as CinemachineBasicMultiChannelPerlin;
        if (noise == null)
            noise = GetComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        if (noise == null)
            return;

        noise.AmplitudeGain = intensity;
        startingIntensity = intensity;
        shakeTimer = time;
        shakeTimerTotal = time;
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (noise != null)
                noise.AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, shakeTimer / shakeTimerTotal);
        }
    }
}
