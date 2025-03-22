using System;
using UnityEngine;

public class WorldGameState : MonoBehaviour
{
    public static event Action worldStateChanged;
    private static DrugState worldState = DrugState.Bouba;

    public static DrugState GetWorldState()
    {
        return worldState;
    }

    public static void ChangeWorldState(DrugState drugState)
    {
        worldState = drugState;

        worldStateChanged?.Invoke();
    }
}
