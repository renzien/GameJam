using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBG : MonoBehaviour
{
    public GameObject[] backgrounds; // Array untuk menyimpan prefab background
    public int numberOfBackgrounds = 5; // Jumlah background yang ingin di-generate
    public Vector2 spawnAreaSize = new Vector2(10, 10); // Ukuran area spawn
    public Transform pivot; // Transform pivot untuk spawn background baru
    public float spawnTime = 2f; // Waktu antara spawn background

    private void Start()
    {
        StartCoroutine(GenerateBackgrounds());
    }

    private IEnumerator GenerateBackgrounds()
    {
        for (int i = 0; i < numberOfBackgrounds; i++)
        {
            GenerateBackgroundAtPivot();
            yield return new WaitForSeconds(spawnTime); // Tunggu selama spawnTime sebelum spawn berikutnya
        }
    }

    public void GenerateBackgroundAtPivot()
    {
        // Pilih background secara acak dari array
        GameObject randomBackground = backgrounds[Random.Range(0, backgrounds.Length)];

        // Tentukan posisi spawn di pivot
        Vector3 spawnPosition = pivot.position;

        // Instantiate background
        GameObject backgroundInstance = Instantiate(randomBackground, spawnPosition, Quaternion.identity);

        // Tambahkan komponen BackgroundMover secara otomatis
        BackgroundMover mover = backgroundInstance.AddComponent<BackgroundMover>();
        mover.moveSpeed = 2f; // Atur kecepatan gerakan (bisa disesuaikan)
        mover.destroyXPosition = -10f; // Atur posisi X untuk menghancurkan (bisa disesuaikan)
    }
}
