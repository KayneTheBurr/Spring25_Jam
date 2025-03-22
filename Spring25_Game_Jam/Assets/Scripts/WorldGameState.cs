using System;
using System.Collections;
using UnityEngine;

public class WorldGameState : MonoBehaviour
{
    public static event Action worldStateChanged;
    private static DrugState worldState = DrugState.Bouba;

    public static float secondsInBouba = 10f;
    public static float boubaTimer;

    private void Start()
    {
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
