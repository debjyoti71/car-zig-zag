using System.Collections;
using UnityEngine;

public class PlatformSpwaner : MonoBehaviour
{

    public GameObject platform; // Prefab for the platform to spawn

    public Transform lastPlatform; // Reference to the last spawned platform

    Vector3 lastpose; // Position of the last spawned platform
    Vector3 newpose; // Position for the new platform

    public bool stop;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastpose = lastPlatform.position; // Initialize lastpose with the position of the last platform
        StartCoroutine(SpawnPlatform()); 

    }

    void genaratePos()
    {
        newpose = lastpose; // Start with the last position
        int rand = Random.Range(0, 2);//0 1 
        if (rand > 0)
        {
            newpose.x += 2f;
        }
        else
        {
            newpose.z += 2f;
        }
        Instantiate(platform, newpose, Quaternion.identity); // Instantiate a new platform at the new position
    }
    IEnumerator SpawnPlatform()
    {
        while (!stop)
        {
            genaratePos(); 
            Instantiate(platform, newpose, Quaternion.identity); 
            lastpose = newpose; 
            yield return new WaitForSeconds(0.2f); 
        }
    }
}
