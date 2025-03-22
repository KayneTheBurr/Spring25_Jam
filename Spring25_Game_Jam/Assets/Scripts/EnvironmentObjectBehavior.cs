using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnvironmentObjectBehavior : MonoBehaviour
{
    SpriteRenderer sp;
    public Sprite boubaSprite;
    public Sprite kikiSprite;

    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        OnEnable();
    }

    public void OnEnable()
    {
        WorldGameState.worldStateChanged += ChangeSprite;
    }

    public void OnDisable()
    {
        WorldGameState.worldStateChanged -= ChangeSprite;
    }

    public void ChangeSprite()
    {
        if(WorldGameState.GetWorldState() == DrugState.Bouba)
        {
            sp.sprite = boubaSprite;
        }
        else if(WorldGameState.GetWorldState() == DrugState.Kikki)
        {
            sp.sprite = kikiSprite;
        }
        else
        {
            Debug.Log("Yo, hey, no world state dog.");
        }
    }
}
