using UnityEngine;

public class WorldLightBehavior : MonoBehaviour
{
    private Light _light;
    public float boubaLightIntensity = 1f;
    public float kikiLightIntensity = 0.1f;
    
    void Awake()
    {
        _light = GetComponent<Light>();

        OnEnable();
    }

    public void OnEnable()
    {
        WorldGameState.worldStateChanged += OnGameStateChange;
    }

    public void OnDisable()
    {
        WorldGameState.worldStateChanged -= OnGameStateChange;
    }

    public void OnGameStateChange()
    {
        DrugState worldState = WorldGameState.GetWorldState();

        switch (worldState)
        {
            case DrugState.Bouba:
                _light.intensity = boubaLightIntensity;
                break;
            case DrugState.Kikki:
                _light.intensity = kikiLightIntensity;
                break;
            default:
                Debug.Log("No world state, not changing world light.");
                break;
        }
    }
}
