using UnityEngine;

public class FireState : MonoBehaviour
{
    public GameObject back;
    public GameObject front;


    void Update()
    {
        //kiki (on)
        if (WorldGameState.instance.GetWorldState() == DrugState.Kikki)
        {
            back.SetActive(true);
            front.SetActive(true);
        }
        //bouba (off)
        else if (WorldGameState.instance.GetWorldState() == DrugState.Bouba)
        {
            back.SetActive(false);
            front.SetActive(false);
        }
    }
}
