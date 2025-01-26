using UnityEngine;

public class EnvironmentSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject environmentPrefab;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Respawn"))
        {
            SpawnEnvironment();
        }
    }

    private void SpawnEnvironment()
    {
        Instantiate(environmentPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
