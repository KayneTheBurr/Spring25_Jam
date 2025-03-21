using UnityEngine;
using Unity.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingManager : MonoBehaviour
{
    public static PostProcessingManager instance { get; private set; }

    public List<VolumeProfile> profiles = new List<VolumeProfile>();

    //  bouba   [0]
    //  kiki    [1]

    private Volume globalVolume;

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
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        globalVolume = GetComponent<Volume>();
    }

    public void kikiToBouba()
    {
        //kiki variables
        DepthOfField dof;
        profiles[1].TryGet<DepthOfField>(out dof);

        //bouba variables
    }

    public void boubaToKiki()
    {
        //bouba variables
        DepthOfField dof;
        profiles[0].TryGet<DepthOfField>(out dof);

        //kiki variables
    }
}
