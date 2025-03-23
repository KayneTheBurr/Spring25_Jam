using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnvironmentObjectBehavior : MonoBehaviour
{
    SpriteRenderer sp;
    Animator anim;
    public Sprite boubaSprite;
    public Sprite kikiSprite;

    public GameObject kikiMoodLight;

    void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
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

            if(anim != null)
            {
                anim.SetBool("isBouba", true);
            }

            if(kikiMoodLight != null)
            {
                kikiMoodLight.SetActive(false);
            }
        }
        else if(WorldGameState.GetWorldState() == DrugState.Kikki)
        {
            sp.sprite = kikiSprite;

            if (anim != null)
            {
                anim.SetBool("isBouba", false);
            }

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
