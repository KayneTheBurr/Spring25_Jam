using UnityEngine;
using UnityEngine.UI;

public class UIPlayerHealthBar : MonoBehaviour
{
    HealthBehavior playerHealth;

    Slider healthSlider;

    void Awake()
    {
        healthSlider = GetComponent<Slider>();

        GameObject player = FindFirstObjectByType<PlayerMovementManager>().gameObject;
        playerHealth = player.GetComponent<HealthBehavior>();
    }

    private void Start()
    {
        healthSlider.maxValue = playerHealth.maxHealthPoints;
        healthSlider.value = playerHealth.currentHealthPoints;
    }

    void Update()
    {
        healthSlider.value = playerHealth.currentHealthPoints;
    }
}
