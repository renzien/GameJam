using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarGene : MonoBehaviour
{
    public GameObject pillarPrefab; // Drag your pillar prefab here in the inspector
    public float spawnInterval = 2f; // Time interval between spawns
    public float moveSpeed = 5f; // Speed at which pillars move to the left
    public float staticYPosition = 0f; // Static Y position for the pillars

    private void Start()
    {
        // Start the spawning coroutine
        StartCoroutine(SpawnPillars());
    }

    private void Update()
    {
        // Move all pillars to the left
        MovePillars();
    }

    private IEnumerator SpawnPillars()
    {
        while (true)
        {
            SpawnPillar();
            yield return new WaitForSeconds(spawnInterval); // Wait for the specified interval
        }
    }

    private void SpawnPillar()
    {
        // Use the static Y position instead of a random one
        Vector3 spawnPosition = new Vector3(transform.position.x, staticYPosition, 0);
        
        // Instantiate the pillar prefab at the static position
        Instantiate(pillarPrefab, spawnPosition, Quaternion.identity);
    }

    private void MovePillars()
    {
        // Find all pillars in the scene and move them to the left
        GameObject[] pillars = GameObject.FindGameObjectsWithTag("Pillar");
        foreach (GameObject pillar in pillars)
        {
            pillar.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
    }
}
