using TMPro;
using UnityEngine;

public class EndStatTracker : MonoBehaviour
{
    public static EndStatTracker instance;

    [SerializeField] private int enemiesKilled = 0;
    [SerializeField] private float playerHealAmount = 2f;
    public TextMeshProUGUI killCountText;
    public Canvas endCanvas;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    private void Start()
    {
        endCanvas.gameObject.SetActive(false);
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
