using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject platform;            // Platform prefab to spawn
    public Transform lastPlatform;         // Initial reference platform

    public bool stop;                      // Flag to control spawning
    public float spawnDelay = 0.2f;        // Delay between spawns

    private Vector3 lastPos;               // Last platform position
    private Vector3 newPos;                // New platform position

    public Dictionary<int, Vector3> platformLoc = new Dictionary<int, Vector3>();
    private int platformIndex = 0;

    public static PlatformSpawner instance;

    void Awake()
    {
        instance = this;
    }


    void Start()
    {
        // Save initial platform position
        lastPos = lastPlatform.position;
        platformLoc[platformIndex++] = lastPos;

        // Start spawning
        StartCoroutine(SpawnPlatformLoop());
    }

    // Coroutine for continuous spawning
    IEnumerator SpawnPlatformLoop()
    {
        while (!stop)
        {
            GenerateNewPosition();
            Instantiate(platform, newPos, Quaternion.identity);
            platformLoc[platformIndex++] = newPos;

            lastPos = newPos;
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    // Determine next spawn position (left or forward)
    void GenerateNewPosition()
    {
        newPos = lastPos;
        int rand = Random.Range(0, 2); // 0 or 1

        if (rand == 0)
            newPos.z += 2f;   // forward
        else
            newPos.x += 2f;   // right
    }
}
