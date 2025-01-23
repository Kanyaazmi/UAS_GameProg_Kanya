using UnityEngine;

public class AnimalSoundTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip sheepSound; 
    [SerializeField] private AudioClip cowSound; 
    [SerializeField] private AudioClip chickenSound; 
    [SerializeField] private AudioClip pigSound; 
    private AudioSource audioSource;
    private bool isSoundPlaying = false;

    private void Start()
    {
        // Setup AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isSoundPlaying) 
        {
            isSoundPlaying = true; 

            // Stop any sound that may already be playing
            audioSource.Stop();

            // Check if the collision is with a specific animal
            if (CompareTag("Sheep"))
            {
                PlayAnimalSound(sheepSound);
            }
            else if (CompareTag("Cow"))
            {
                PlayAnimalSound(cowSound);
            }
            else if (CompareTag("Chicken"))
            {
                PlayAnimalSound(chickenSound);
            }
            else if (CompareTag("Pig"))
            {
                PlayAnimalSound(pigSound);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Check if collider is the player
        {
            // Mark that sound is no longer playing after player exits
            isSoundPlaying = false;
        }
    }



    private void PlayAnimalSound(AudioClip sound)
    {
        if (sound != null)
        {
            audioSource.PlayOneShot(sound); // Play sound once
        }
        
    }
}
