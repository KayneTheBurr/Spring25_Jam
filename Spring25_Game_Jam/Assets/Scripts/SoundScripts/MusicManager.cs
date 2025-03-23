using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    // AudioSource to play the sounds. You can assign this in the Inspector.
    public AudioSource audioSource;

    public AudioClip[] boubaSongs;
    public AudioClip endLoop;
    public AudioClip kikiLoop;
    public AudioClip mainMenu;
    public AudioClip menuKiki;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    public void PlayBoubaSongs()
    {
        if (boubaSongs.Length == 0)
        {
            Debug.LogWarning("No boubaSongs assigned in ESFXManager!");
            return;
        }

        int randomIndex = Random.Range(0, boubaSongs.Length);
        AudioClip selectedClip = boubaSongs[randomIndex];
        audioSource.PlayOneShot(selectedClip);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayEndLoop()
    {
        if (endLoop != null)
        {
            audioSource.loop = true;
            audioSource.clip = endLoop;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("endLoop AudioClip is not assigned in MusicManager!");
        }
    }

    public void PlayKikiLoop()
    {
        if (kikiLoop != null)
        {
            audioSource.loop = true;
            audioSource.clip = kikiLoop;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("kikiLoop AudioClip is not assigned in MusicManager!");
        }
    }


    public void PlayMenuKiki()
    {
        if (menuKiki != null)
        {
            audioSource.PlayOneShot(menuKiki);
        }
        else
        {
            Debug.LogWarning("menuKiki AudioClip is not assigned in ESFXManager!");
        }
    }

    public void PlayMainMenu()
    {
        if (mainMenu != null)
        {
            audioSource.PlayOneShot(mainMenu);
        }
        else
        {
            Debug.LogWarning("mainMenu AudioClip is not assigned in ESFXManager!");
        }
    }


}
