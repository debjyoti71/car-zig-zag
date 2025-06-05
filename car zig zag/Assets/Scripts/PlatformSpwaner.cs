using System.Collections;
using UnityEngine;

public class PlatformSpwaner : MonoBehaviour
{
    public GameObject platform;           // Prefab for the platform to spawn
    public Transform lastPlatform;        // Reference to the last spawned platform

    Vector3 lastpose;                     // Position of the last spawned platform
    Vector3 newpose;                      // Position for the new platform

    public bool stop;                     // Flag to control spawning

    void Start()
    {
        lastpose = lastPlatform.position; // Initialize lastpose with the position of the last platform
        StartCoroutine(SpawnPlatform());
    }

    // Generate new position based on last position
    void genaratePos()
    {
        newpose = lastpose;
        int rand = Random.Range(0, 2); // Generates 0 or 1
        if (rand > 0)
        {
            newpose.x += 2f;
        }
        else
        {
            newpose.z += 2f;
        }
    }

    // Coroutine that continuously spawns platforms
    IEnumerator SpawnPlatform()
    {
        while (!stop)
        {
            genaratePos(); // Calculate new position
            Instantiate(platform, newpose, Quaternion.identity); // Spawn the platform
            lastpose = newpose; // Update last position
            yield return new WaitForSeconds(0.2f); // Wait for 0.2 seconds before spawning next
        }
    }
}
