using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    public AudioSource audioSource; // AudioSource untuk memutar audio
    public AudioClip collisionClip; // Audio clip yang akan diputar

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Memeriksa apakah objek yang bertabrakan memiliki tag "Obstacle"
        if (other.CompareTag("Obstacle"))
        {
            // Memutar audio saat bertabrakan
            audioSource.PlayOneShot(collisionClip);

            // Menghancurkan object setelah audio selesai diputar
            Destroy(gameObject, collisionClip.length);
        }
    }
}
