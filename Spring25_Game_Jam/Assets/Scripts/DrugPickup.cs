using UnityEngine;

public class DrugPickup : MonoBehaviour
{
    public string drugName;
    public DrugState drugState;
    //public WeaponType weaponType;
    public LayerMask playerLayer;
    public AudioClip pickupSound;

    private AudioSource audioSource;
    private PlayerStatsManager statsManager;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource= gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            statsManager = other.GetComponent<PlayerStatsManager>();
            if (statsManager != null)
            {
                // For kiki and bouba drug state and switch weapon
                //statsManager.SwitchDrugState(drugState);
                //statsManager.SwitchWeapon(weaponType);

                if (pickupSound != null)
                {
                    audioSource.PlayOneShot(pickupSound);  // play sound after pickup the object
                }

                Destroy(gameObject);
            }
        }
    }
}
