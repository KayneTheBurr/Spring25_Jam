using UnityEngine;

public class MenuSwapper : MonoBehaviour
{
    public static MenuSwapper instance;

    public bool isKiKiMenu = false;

    private void Awake()
    {
        if(instance == null) instance = this;
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void SwapStateOnPlayerDeath()
    {
       isKiKiMenu = !isKiKiMenu;
    }
    public bool GetKiki()
    {
        return isKiKiMenu;
    }

}
