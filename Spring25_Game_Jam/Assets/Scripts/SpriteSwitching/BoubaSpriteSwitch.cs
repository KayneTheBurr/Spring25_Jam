using UnityEngine;

public class BoubaSpriteSwitch : MonoBehaviour
{
    private void OnEnable()
    {
        WorldGameState.worldStateChanged += UpdateVisibility;
        UpdateVisibility(); // Sync on start
    }

    private void OnDisable()
    {
        WorldGameState.worldStateChanged -= UpdateVisibility;
    }

    private void UpdateVisibility()
    {
        // Enable only if the current world state is Bouba
        gameObject.SetActive(WorldGameState.GetWorldState() == DrugState.Bouba);
    }
}
