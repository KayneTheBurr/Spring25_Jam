using UnityEngine;

public class BoubaSpriteSwitch : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(!MenuSwapper.instance.GetKiki());

        if (isActiveAndEnabled) MusicManager.instance.PlayMainMenu();
    }
}
