using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    public GameObject explosionPrefab; // Prefab ledakan

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Memeriksa apakah objek yang bertabrakan memiliki tag "Obstacle"
        if (other.CompareTag("Obstacle"))
        {
            // Instantiate prefab ledakan di posisi dan rotasi terakhir objek
            GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);

            // Tambahkan komponen DestroyAfterTime ke prefab ledakan
            explosion.AddComponent<DestroyAfterTime>().destroyTime = 0.5f;

            // Menghancurkan objek yang ditabrak
            Destroy(other.gameObject);

            // Menghancurkan objek ini setelah instansiasi ledakan
            Destroy(gameObject);
        }
    }
}
