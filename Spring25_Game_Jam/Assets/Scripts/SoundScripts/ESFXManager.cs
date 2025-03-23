using UnityEngine;

public class ESFXManager : MonoBehaviour
{
    public static ESFXManager instance;

    // AudioSource to play the sounds. You can assign this in the Inspector.
    public AudioSource audioSource;

    // Arrays of sounds.
    public AudioClip[] successSounds;
    public AudioClip[] boubaToKiki;
    public AudioClip kikiToBouba;
    public AudioClip kikiWarnLaugh;
    public AudioClip endScreenSting;

    // Single AudioClip for darkCircusAnnouncement.
    public AudioClip darkCircusAnnouncement;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void PlaySuccessSound()
    {
        if (successSounds.Length == 0)
        {
            Debug.LogWarning("No successSounds assigned in ESFXManager!");
            return;
        }

        int randomIndex = Random.Range(0, successSounds.Length);
        AudioClip selectedClip = successSounds[randomIndex];
        audioSource.PlayOneShot(selectedClip);
    }

    public void PlayBoubaToKikiSound()
    {
        if (boubaToKiki.Length == 0)
        {
            Debug.LogWarning("No boubaToKiki sounds assigned in ESFXManager!");
            return;
        }

        int randomIndex = Random.Range(0, boubaToKiki.Length);
        AudioClip selectedClip = boubaToKiki[randomIndex];
        audioSource.PlayOneShot(selectedClip);
    }

    public void PlayKikiToBoubaSound()
    {
        if (kikiToBouba != null)
        {
            audioSource.PlayOneShot(kikiToBouba);
        }
        else
        {
            Debug.LogWarning("kikiToBouba AudioClip is not assigned in ESFXManager!");
        }
    }

    public void PlayDarkCircusAnnouncement()
    {
        if (darkCircusAnnouncement != null)
        {
            audioSource.PlayOneShot(darkCircusAnnouncement);
        }
        else
        {
            Debug.LogWarning("darkCircusAnnouncement AudioClip is not assigned in ESFXManager!");
        }
    }

    public void PlayKikiWarnLaugh()
    {
        if (kikiWarnLaugh != null)
        {
            audioSource.PlayOneShot(kikiWarnLaugh);
        }
        else
        {
            Debug.LogWarning("kikiWarnLaugh AudioClip is not assigned in ESFXManager!");
        }
    }

    public void PlayEndScreenSting()
    {
        if (endScreenSting != null)
        {
            audioSource.PlayOneShot(endScreenSting);
        }
        else
        {
            Debug.LogWarning("endScreenSting AudioClip is not assigned in ESFXManager!");
        }
    }
}
