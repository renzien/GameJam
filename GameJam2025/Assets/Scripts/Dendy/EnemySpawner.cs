using System.Collections;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Wave[] waves; // Array wave yang akan dijalankan
    public AudioSource audioSource; // Sumber audio
    public float[] waveSpawnTimes; // Array waktu spawn untuk setiap wave

    [SerializeField]
    private float x = 11.07f; // Koordinat X yang tetap

    public float currentAudioTime; // Durasi saat ini dari audio
    public float bpm = 123f; // BPM dari lagu
    private float beatInterval;

    private int currentWaveIndex = 0;

    void Start()
    {
        beatInterval = 60f / bpm; // Hitung interval ketukan
        audioSource.Play(); // Mulai pemutaran audio
        StartCoroutine(SpawnEnemies());
    }

    void Update()
    {
        currentAudioTime = audioSource.time; // Memperbarui durasi saat ini dari audio
    }

    IEnumerator SpawnEnemies()
    {
        while (currentWaveIndex < waves.Length)
        {
            Wave currentWave = waves[currentWaveIndex];
            float spawnTime = waveSpawnTimes[currentWaveIndex]; // Ambil waktu spawn dari array

            // Tunggu sampai waktu spawn tercapai
            yield return new WaitUntil(() => audioSource.time >= spawnTime);

            Debug.Log($"Wave {currentWaveIndex + 1} started");

            StartCoroutine(SpawnWave(currentWave));

            // Tunggu sampai semua object di-spawn
            yield return new WaitUntil(() => AllEnemiesSpawned(currentWave));

            Debug.Log($"Wave {currentWaveIndex + 1} completed");

            currentWaveIndex++;
        }

        Debug.Log("All waves completed");
    }

    IEnumerator SpawnWave(Wave wave)
    {
        for (int i = 0; i < wave.enemies.Length; i++)
        {
            GameObject enemy = wave.enemies[i];
            float spawnY = wave.spawnYPositions[i]; // Ambil posisi y dari array spawnYPositions
            float spawnDelay = wave.spawnDelays[i]; // Ambil spawn delay dari array spawnDelays
            Vector2 spawnPosition = new Vector2(x, spawnY);
            yield return new WaitForSeconds(spawnDelay); // Tunggu sesuai delay sebelum spawn musuh
            Instantiate(enemy, spawnPosition, Quaternion.identity);
        }
    }

    private bool AllEnemiesSpawned(Wave wave)
    {
        // Memeriksa apakah semua enemy telah di-spawn
        return FindObjectsOfType<GameObject>().Count(go => wave.enemies.Contains(go)) == 0;
    }
}
