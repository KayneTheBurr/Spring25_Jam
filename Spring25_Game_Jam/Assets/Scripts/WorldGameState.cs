using System;
using UnityEngine;

public class WorldGameState : MonoBehaviour
{
    public static event Action worldStateChanged;
    private static DrugState worldState = DrugState.Bouba;

    public DrugState GetWorldState()
    {
        return worldState;
    }

    public void ChangeWorldState(DrugState drugState)
    {
        worldState = drugState;

        worldStateChanged?.Invoke();
    }
}
