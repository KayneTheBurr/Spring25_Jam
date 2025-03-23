using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndStatTracker : MonoBehaviour
{
    public static EndStatTracker instance;

    [SerializeField] private int enemiesKilled = 0;
    [SerializeField] private float playerHealAmount = 2f;
    public Button mainMenuButton;
    public TextMeshProUGUI killCountText;
    public Canvas endCanvas;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    private void Start()
    {
        mainMenuButton.onClick.AddListener(GoToMenu);
        endCanvas.gameObject.SetActive(false);
    }
    public void AddEnemyKilled()
    {
        enemiesKilled++;
        PlayerManager.instance.HealMeFool(playerHealAmount);
    }
    public void PlayerIsDead()
    {
        endCanvas.gameObject.SetActive(true);

        killCountText.text = $"{enemiesKilled} killed";

        ESFXManager.instance.PlayEndScreenSting();
        MusicManager.instance.PlayEndLoop();

        MenuSwapper.instance.SwapStateOnPlayerDeath();
    }
    public void GoToMenu()
    {
        SceneChangeHandler.instance.ChangeSceneByIndex(0);
    }
}
