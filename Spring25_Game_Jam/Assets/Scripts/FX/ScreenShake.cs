using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

public class ScreenShake : MonoBehaviour
{
    //PUT THIS ON THE CINEMACHINE CAMERA (NOT BRAIN)
    //shakeCam(shake intensity, shake duration)

    public static ScreenShake instance { get; private set; }

    private CinemachineCamera cam;
    private float shakeTimer;

    private void Awake()
    {
        //set singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        cam = GetComponent<CinemachineCamera>();
    }

    public void shakeCam(float intensity, float duration)
    {
        Debug.Log(1);
        CinemachineBasicMultiChannelPerlin perlin = cam.GetComponent<CinemachineBasicMultiChannelPerlin>();
        Debug.Log(1);
        perlin.AmplitudeGain = intensity;
        shakeTimer = duration;
    }

    private void Update()
    {
        if (shakeTimer > 0f)
        {
            Debug.Log("Countdown Shake Timer");
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                //timer over
                CinemachineBasicMultiChannelPerlin perlin = cam.GetComponent<CinemachineBasicMultiChannelPerlin>();

                perlin.AmplitudeGain = 0f;
            }
        }
    }
}
