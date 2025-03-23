using UnityEngine;

public class EndStatTracker : MonoBehaviour
{
    public static EndStatTracker instance;

    [SerializeField] private int enemiesKilled = 0;
    [SerializeField] private float playerHealAmount = 2f;


    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void AddEnemyKilled()
    {
        enemiesKilled++;
        PlayerManager.instance.HealMeFool(playerHealAmount);
    }
    public void PlayerIsDead()
    {
        //end the game here!
        //turn on end canvas
        ESFXManager.instance.PlayEndScreenSting();
        MusicManager.instance.PlayEndLoop();
    }

}
