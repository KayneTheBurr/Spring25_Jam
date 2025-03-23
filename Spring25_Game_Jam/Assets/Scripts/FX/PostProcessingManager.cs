using UnityEngine;
using Unity.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;
using UnityEngine.UI;

public class PostProcessingManager : MonoBehaviour
{
    public static PostProcessingManager instance { get; private set; }

    public List<VolumeProfile> profiles = new List<VolumeProfile>();

    //  bouba   [0]
    //  kiki    [1]

    public Volume globalVolume;
    public GameObject flash;

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
        StartCoroutine(screenFlash(Color.white, 0.1f));
        globalVolume.profile = profiles[0];
    }

    public void boubaToKiki()
    {
        StartCoroutine(screenFlash(Color.red, 0.1f));
        globalVolume.profile = profiles[1];
    }

    public IEnumerator screenFlash(Color flashColor, float flashDuration)
    {
        flash.GetComponent<Image>().color = flashColor;
        flash.SetActive(true);
        yield return new WaitForSeconds(flashDuration);
        flash.SetActive(false);
    }
}
