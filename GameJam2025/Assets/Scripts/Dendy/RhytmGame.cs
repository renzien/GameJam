using System.Collections;
using UnityEngine;

public class RhythmGame : MonoBehaviour
{
    public AudioSource audioSource; // Audio source kamu
    public GameObject[] waveAndEnemyPrefabs; // Array prefab wave dan enemy
    private float[] spawnTimes; // Array waktu spawn

    public float spawnPosX = 11.07f; // Posisi x yang tetap
    public float spawnPosY1 = 2.1f; // Posisi y pertama
    public float spawnPosY2 = -1.95f; // Posisi y kedua

    void Start()
    {
        // Contoh waktu spawn di detik ke-1, 2, dan 3
        spawnTimes = new float[] { 1f, 2f, 3f };
        StartCoroutine(SpawnWavesAndEnemies());
    }

    IEnumerator SpawnWavesAndEnemies()
    {
        for (int i = 0; i < spawnTimes.Length; i++)
        {
            yield return new WaitForSeconds(spawnTimes[i]);
            SpawnWaveOrEnemy(i);
        }
    }

    void SpawnWaveOrEnemy(int index)
    {
        if (index < waveAndEnemyPrefabs.Length)
        {
            float spawnPosY = (index % 2 == 0) ? spawnPosY1 : spawnPosY2; // Alternasi posisi y
            Vector2 spawnPosition = new Vector2(spawnPosX, spawnPosY);
            Instantiate(waveAndEnemyPrefabs[index], spawnPosition, Quaternion.identity);
        }
    }
}
