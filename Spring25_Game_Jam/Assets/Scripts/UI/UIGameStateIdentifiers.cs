using UnityEngine;
using UnityEngine.UI;

public class UIGameStateIdentifiers : MonoBehaviour
{
    public Image gameStateIcon;

    private void Awake()
    {
        OnEnable();
    }

    public void OnEnable()
    {
        WorldGameState.worldStateChanged += OnWorldStateChange;
    }

    public void OnDisable()
    {
        WorldGameState.worldStateChanged -= OnWorldStateChange;
    }

    public void OnWorldStateChange()
    {
        DrugState worldState = WorldGameState.GetWorldState();

        switch (worldState)
        {
            case DrugState.Bouba:
                SetBoubaUI();
                break;
            case DrugState.Kikki:
                SetKikkiUI();
                break;
            default:
                Debug.Log("Unknown world state set, cannot change UI");
                break;
        }
    }

    private void SetBoubaUI()
    {
        gameStateIcon.color = Color.green;
    }

    private void SetKikkiUI()
    {
        gameStateIcon.color = Color.red;
    }
}
