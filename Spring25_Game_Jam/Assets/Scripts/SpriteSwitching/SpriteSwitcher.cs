using UnityEngine;

public class SpriteSwitcher : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite spriteBouba;
    public Sprite spriteKiki;

    private void Start()
    {
        // Subscribe to the world state changed event (thanks Eb)
        WorldGameState.worldStateChanged += UpdateSprite;

        // Update the sprite at the start to match the current world state
        UpdateSprite();
    }

    private void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks (0-0)
        WorldGameState.worldStateChanged -= UpdateSprite;
    }

    // Called whenever the world state changes
    private void UpdateSprite()
    {
        // Check current global world state
        var currentState = WorldGameState.GetWorldState();

        // Set the sprite depending on the state
        spriteRenderer.sprite = (currentState == DrugState.Bouba) ? spriteBouba : spriteKiki;
    }
}