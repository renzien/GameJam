using UnityEngine;

public class ObstacleSound : MonoBehaviour
{
    public AudioSource audioSource; // AudioSource untuk memutar audio
    public AudioClip[] collisionClips; // Array audio clip yang akan diputar
    private int currentClipIndex = 0; // Indeks saat ini dalam array

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Memeriksa apakah objek yang bertabrakan memiliki tag yang sesuai
        if (other.CompareTag("Player"))
        {
            // Memutar audio clip saat bertabrakan
            if (collisionClips.Length > 0)
            {
                audioSource.PlayOneShot(collisionClips[currentClipIndex]);

                // Update indeks untuk audio clip berikutnya
                currentClipIndex = (currentClipIndex + 1) % collisionClips.Length;
            }
        }
    }
}
