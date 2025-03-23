using UnityEngine;

public class WorldStateMaterialSwap : MonoBehaviour
{
    private MeshRenderer mr;
    public Material boubaMaterial;
    public Material kikiMaterial;

    void Awake()
    {
        mr = GetComponent<MeshRenderer>();
        //OnEnable();
    }


    private void OnEnable()
    {
        WorldGameState.worldStateChanged += OnGameStateChanged;
        SceneChangeHandler.onSceneChange += OnDisable;
    }

    private void OnDisable()
    {
        WorldGameState.worldStateChanged -= OnGameStateChanged;
        SceneChangeHandler.onSceneChange -= OnDisable;
    }

    public void OnGameStateChanged()
    {
        DrugState worldState = WorldGameState.instance.GetWorldState();

        switch (worldState)
        {
            case DrugState.Bouba:
                if(mr != null)
                {
                    mr.material = boubaMaterial;
                }
                
                break;
            case DrugState.Kikki:
                if (mr != null)
                {
                    mr.material = kikiMaterial;
                }
                    
                break;
            default:
                Debug.Log("No world state, cannot change material.");
                break;
        }
    }
}
