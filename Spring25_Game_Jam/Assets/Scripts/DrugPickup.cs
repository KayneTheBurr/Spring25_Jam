using UnityEngine;

public class DrugPickup : MonoBehaviour
{
    public string drugName;
    //public WeaponType weaponType;
    public LayerMask playerLayer;
    public AudioClip pickupSound;

    private AudioSource audioSource;
    private PlayerStatsManager statsManager;

    void Awake()
    {
        //OnEnable();
    }

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
                if (pickupSound != null)
                {
                    audioSource.PlayOneShot(pickupSound);  // play sound after pickup the object
                }

                // For kiki and bouba drug state and switch weapon
                //statsManager.SwitchWeapon(weaponType);

                WorldGameState.instance.ChangeWorldState(DrugState.Bouba);
            }
        }
    }

    #region Listeners

    private void OnEnable()
    {
        WorldGameState.worldStateChanged += OnWorldStateChanged;
    }

    private void OnDisable()
    {
        WorldGameState.worldStateChanged -= OnWorldStateChanged;
    }

    public void OnWorldStateChanged()
    {
        if(WorldGameState.instance.GetWorldState() == DrugState.Bouba)
        {
            OnDisable();
            Destroy(gameObject);
        }
    }

    #endregion
}
