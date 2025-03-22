using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }

    public List<AudioSource> SFX = new List<AudioSource>();

    public AudioSource kikiBGM;
    public AudioSource boubaBGM;

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

    private void Start()
    {
        //play kiki music
        //play bouba music
    }

    public void playSFX (AudioSource sfx, float minPitch, float maxPitch)
    {
        sfx.pitch = Random.Range(minPitch, maxPitch);
        sfx.Play();
    }

    public void playBoubaBGM()
    {
        if (kikiBGM.isPlaying)
        {
            kikiBGM.Stop();
            boubaBGM.Play();
        }
        else
        {
            boubaBGM.Play();
        }
    }

    public void playKikiBGM()
    {
        if (boubaBGM.isPlaying)
        {
            boubaBGM.Stop();
            kikiBGM.Play();
        }
        else
        {
            kikiBGM.Play();
        }
    }
}
