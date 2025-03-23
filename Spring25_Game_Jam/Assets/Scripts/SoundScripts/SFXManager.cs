using UnityEngine;

public class SFXManager : MonoBehaviour
{
    // AudioSource to play the sounds. You can assign this in the Inspector.
    public AudioSource audioSource;

    // Array of hit sounds to choose from.
    public AudioClip[] hitSounds;

    // Array of hit sounds to choose from.
    public AudioClip[] drugPickUpSounds;




    // The big hit sound clip to play.
    public AudioClip bigHitWindUp;
    public AudioClip bigHitTink;
    public AudioClip bigHitSpin;

    // Array of hit sounds to choose from.
    public AudioClip[] bigHitSounds;

    public AudioClip bearHitSound;
    public AudioClip bubbleShootSound;
    public AudioClip confettiExplosionSound;

    public AudioClip footstep1;
    public AudioClip footstep2;
    public AudioClip footstep3;

    // This method is called to play a hit sound.
    public void PlayHitSound()
    {
        // Ensure we have some sounds.
        if (hitSounds.Length == 0)
        {
            Debug.LogWarning("No hit sounds assigned in SFXManager!");
            return;
        }

        // Choose a random hit sound from the array.
        int randomIndex = Random.Range(0, hitSounds.Length);
        AudioClip selectedClip = hitSounds[randomIndex];

        // Play the chosen sound using the AudioSource.
        audioSource.PlayOneShot(selectedClip);
    }

    public void PlayBigHitSound()
    {
        // Ensure we have some sounds.
        if (bigHitSounds.Length == 0)
        {
            Debug.LogWarning("No hit sounds assigned in SFXManager!");
            return;
        }

        // Choose a random hit sound from the array.
        int randomIndex = Random.Range(0, bigHitSounds.Length);
        AudioClip selectedClip = hitSounds[randomIndex];

        // Play the chosen sound using the AudioSource.
        audioSource.PlayOneShot(selectedClip);
    }

    public void PlayDrugSound()
    {
        // Ensure we have some sounds.
        if (drugPickUpSounds.Length == 0)
        {
            Debug.LogWarning("No hit sounds assigned in SFXManager!");
            return;
        }

        // Choose a random hit sound from the array.
        int randomIndex = Random.Range(0, drugPickUpSounds.Length);
        AudioClip selectedClip = drugPickUpSounds[randomIndex];

        // Play the chosen sound using the AudioSource.
        audioSource.PlayOneShot(selectedClip);
    }

    // Call this method to play the big hit sound.
    public void PlayBigHitWindUp()
    {
        if (bigHitWindUp != null)
        {
            audioSource.PlayOneShot(bigHitWindUp);
        }
        else
        {
            Debug.LogWarning("bigHitWindUp AudioClip is not assigned in SFXManager!");
        }
    }
    // Call this method to play the big hit sound.
    public void PlayBigHitTink()
    {
        if (bigHitTink != null)
        {
            audioSource.PlayOneShot(bigHitTink);
        }
        else
        {
            Debug.LogWarning("bigHitTink AudioClip is not assigned in SFXManager!");
        }
    }

    // Call this method to play the bear hit sound.
    public void PlayBearHitSound()
    {
        if (bearHitSound != null)
        {
            audioSource.PlayOneShot(bearHitSound);
        }
        else
        {
            Debug.LogWarning("bigHitTink AudioClip is not assigned in SFXManager!");
        }
    }
    // Call this method to play the bear hit sound.
    public void PlayBubbleShootSound()
    {
        if (bubbleShootSound != null)
        {
            audioSource.PlayOneShot(bubbleShootSound);
        }
        else
        {
            Debug.LogWarning("bubbleShootSound AudioClip is not assigned in SFXManager!");
        }
    }
    // Call this method to play the bear hit sound.
    public void PlayConfettiExplosionSound()
    {
        if (confettiExplosionSound != null)
        {
            audioSource.PlayOneShot(confettiExplosionSound);
        }
        else
        {
            Debug.LogWarning("confettiExplosionSound AudioClip is not assigned in SFXManager!");
        }
    }
    public void PlayFootstep1()
    {
        if (footstep1 != null)
        {
            audioSource.PlayOneShot(footstep1);
        }
        else
        {
            Debug.LogWarning("footstep1 AudioClip is not assigned in SFXManager!");
        }
    }
    public void PlayFootstep2()
    {
        if (footstep2 != null)
        {
            audioSource.PlayOneShot(footstep2);
        }
        else
        {
            Debug.LogWarning("footstep2 AudioClip is not assigned in SFXManager!");
        }
    }
    public void PlayFootstep3()
    {
        if (footstep3 != null)
        {
            audioSource.PlayOneShot(footstep3);
        }
        else
        {
            Debug.LogWarning("footstep3 AudioClip is not assigned in SFXManager!");
        }
    }

    public void PlayBigHitSpin()
    {
        if (bigHitSpin != null)
        {
            audioSource.PlayOneShot(bigHitSpin);
        }
        else
        {
            Debug.LogWarning("bigHitSpin AudioClip is not assigned in SFXManager!");
        }
    }
}
