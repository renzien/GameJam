using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{
    public GameObject[] furniturePrefabs; // Array untuk menyimpan prefab furniture
    public float spawnInterval = 2f; // Interval waktu antara spawn
    public float spawnYPosition = 0f; // Posisi Y statis untuk furniture
    public float spawnXPosition = 0f; // Posisi X statis untuk spawn furniture
    public float moveSpeed = 5f; // Kecepatan gerakan furniture ke kiri

    private void Start()
    {
        // Mulai coroutine untuk spawn furniture
        StartCoroutine(SpawnFurniture());
    }

    private void Update()
    {
        // Pindahkan semua furniture ke kiri
        MoveFurniture();
    }

    private IEnumerator SpawnFurniture()
    {
        // Spawn furniture pada interval yang ditentukan
        while (true)
        {
            SpawnStaticFurniture();
            yield return new WaitForSeconds(spawnInterval); // Tunggu selama interval yang ditentukan
        }
    }

    private void SpawnStaticFurniture()
    {
        // Pilih indeks acak dari array furniturePrefabs
        int randomIndex = Random.Range(0, furniturePrefabs.Length);
        
        // Tentukan posisi spawn statis
        Vector3 spawnPosition = new Vector3(spawnXPosition, spawnYPosition, 0);
        
        // Instantiate prefab furniture yang dipilih di posisi spawn
        GameObject furniture = Instantiate(furniturePrefabs[randomIndex], spawnPosition, Quaternion.identity);
        
        // Set tag "Furniture" pada objek yang dihasilkan
        furniture.tag = "Furniture";
    }

    private void MoveFurniture()
    {
        // Temukan semua furniture dalam scene dan pindahkan mereka ke kiri
        GameObject[] furnitureObjects = GameObject.FindGameObjectsWithTag("Furniture");
        foreach (GameObject furniture in furnitureObjects)
        {
            furniture.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
    }
}
