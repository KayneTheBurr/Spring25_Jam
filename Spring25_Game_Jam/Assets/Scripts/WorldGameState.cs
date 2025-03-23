using System;
using System.Collections;
using UnityEngine;

public class WorldGameState : MonoBehaviour
{
    public static event Action worldStateChanged;
    private static DrugState worldState = DrugState.Bouba;

    [SerializeField] private float secondsInBouba = 10f;
    public static float boubaTimer;

    [SerializeField] private float extraBoubaTimeRound1 = 30f;

    private void Start()
    {
        secondsInBouba += extraBoubaTimeRound1;

        ChangeWorldState(DrugState.Bouba);
    }

    private void Update()
    {
        if (worldState == DrugState.Bouba)
        {
            if (boubaTimer < secondsInBouba)
            {
                boubaTimer += Time.deltaTime;

                if (boubaTimer >= secondsInBouba)
                {
                    ChangeWorldState(DrugState.Kikki);
                }
            }
        }
    }

    public static DrugState GetWorldState()
    {
        return worldState;
    }

    public static void ChangeWorldState(DrugState drugState)
    {
        worldState = drugState;
        if (worldState == DrugState.Bouba) { boubaTimer = 0f; }

        worldStateChanged?.Invoke();
    }
}
