using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class WorldGameState : MonoBehaviour
{
    public static WorldGameState instance;

    public static event Action worldStateChanged;
    private static DrugState worldState = DrugState.Bouba;

    [SerializeField] private float secondsInBouba = 10f;
    public static float boubaTimer;

    [SerializeField] private float extraBoubaTimeRound1 = 30f;
    private bool warningPlayed = false;
    private bool firstKikiTime = false;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    private void Start()
    {
        secondsInBouba += extraBoubaTimeRound1;

        ChangeWorldState(DrugState.Bouba);
    }

    private void Update()
    {
        float changeSoundTime = secondsInBouba - 5f;

        if (worldState == DrugState.Bouba)
        {
            if (boubaTimer < secondsInBouba)
            {
                boubaTimer += Time.deltaTime;

                if (boubaTimer >= changeSoundTime && warningPlayed == false)
                {
                    warningPlayed = true;
                    ESFXManager.instance.PlayKikiWarnLaugh();
                }

                if (boubaTimer >= secondsInBouba)
                {
                    if(firstKikiTime)
                    {
                        firstKikiTime = true;
                        ESFXManager.instance.PlayDarkCircusAnnouncement();
                    }
                    ChangeWorldState(DrugState.Kikki);
                }
            }
        }
    }
    public static DrugState GetWorldState()
    {
        return worldState;
    }
    public void ChangeWorldState(DrugState drugState)
    {
        worldState = drugState;
        if (worldState == DrugState.Bouba) //if goes to bouba
        {
            ESFXManager.instance.PlayKikiToBoubaSound();
            MusicManager.instance.PlayBoubaSongs();
            warningPlayed = false;
            boubaTimer = 0f; 
        }
        else //if goes to kiki 
        {
            ESFXManager.instance.PlayBoubaToKikiSound();
            MusicManager.instance.PlayKikiLoop();
            warningPlayed = false;
        }

        worldStateChanged?.Invoke();
    }
}
