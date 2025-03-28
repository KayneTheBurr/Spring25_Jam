using UnityEngine;

public class EmbersAndBubbles : MonoBehaviour
{
    public ParticleSystem embers;
    public ParticleSystem bubbles;

    void Update()
    {
        //kiki
        if (WorldGameState.instance.GetWorldState() == DrugState.Kikki)
        {
            if (bubbles.isPlaying)
            {
                bubbles.Stop();
                
                if (!embers.isPlaying)
                {
                    embers.Play();
                }
            }
        }
        //bouba
        else if (WorldGameState.instance.GetWorldState() == DrugState.Bouba)
        {
            if (embers.isPlaying)
            {
                embers.Stop();

                if (!bubbles.isPlaying)
                {
                    bubbles.Play();
                }
            }
        }
    }
}
