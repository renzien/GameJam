using UnityEngine;

public class WallDestroyer : MonoBehaviour
{
    public AudioSource audioSource; // AudioSource untuk memutar audio
    public AudioClip destructionClip; // Audio clip yang akan diputar saat bertabrakan dengan obstacle

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Memeriksa apakah objek yang bertabrakan memiliki tag "Obstacle"
        if (other.CompareTag("Obstacle"))
        {
            // Memutar audio saat bertabrakan
            audioSource.PlayOneShot(destructionClip);

            // Menghancurkan objek yang bertabrakan
            Destroy(other.gameObject);
        }
    }
}
