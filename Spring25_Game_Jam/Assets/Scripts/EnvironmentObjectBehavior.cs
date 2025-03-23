using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnvironmentObjectBehavior : MonoBehaviour
{
    SpriteRenderer sp;
    public Sprite boubaSprite;
    public Sprite kikiSprite;

    public GameObject kikiMoodLight;

    void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        OnEnable();
        ChangeSprite();
    }

    public void OnEnable()
    {
        WorldGameState.worldStateChanged += ChangeSprite;
        SceneChangeHandler.onSceneChange += OnDisable;
    }

    public void OnDisable()
    {
        WorldGameState.worldStateChanged -= ChangeSprite;
        SceneChangeHandler.onSceneChange -= OnDisable;
    }

    public void ChangeSprite()
    {
        if(WorldGameState.GetWorldState() == DrugState.Bouba)
        {
            sp.sprite = boubaSprite;
            if(kikiMoodLight != null)
            {
                kikiMoodLight.SetActive(false);
            }
        }
        else if(WorldGameState.GetWorldState() == DrugState.Kikki)
        {
            sp.sprite = kikiSprite;
            if (kikiMoodLight != null)
            {
                kikiMoodLight.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Yo, hey, no world state dog.");
        }
    }
}
