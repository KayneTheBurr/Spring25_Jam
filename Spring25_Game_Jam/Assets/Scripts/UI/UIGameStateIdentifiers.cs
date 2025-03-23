using UnityEngine;
using UnityEngine.UI;

public class UIGameStateIdentifiers : MonoBehaviour
{
    private Image icon;
    public Sprite boubaSprite;
    public Sprite kikiSprite;

    private void Awake()
    {
        icon = GetComponent<Image>();
        OnEnable();
    }

    public void OnEnable()
    {
        WorldGameState.worldStateChanged += OnWorldStateChange;
        SceneChangeHandler.onSceneChange += OnDisable;
    }

    public void OnDisable()
    {
        WorldGameState.worldStateChanged -= OnWorldStateChange;
        SceneChangeHandler.onSceneChange -= OnDisable;
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
        icon.sprite = boubaSprite;
    }

    private void SetKikkiUI()
    {
        icon.sprite = kikiSprite;
    }
}
