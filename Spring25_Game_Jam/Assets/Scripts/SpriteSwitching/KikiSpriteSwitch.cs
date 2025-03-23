using UnityEngine;

public class KikiSpriteSwitch : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(MenuSwapper.instance.GetKiki());

        if (isActiveAndEnabled) MusicManager.instance.PlayMenuKiki();
    }
}
