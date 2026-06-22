using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public Transform player;

    public GameObject[] platformPrefabs;
    public float[] platformLengths;

    public float spawnAheadDistance = 60f;
    public float destroyBehindDistance = 30f;

    public int startingPlatforms = 3;

    private float nextSpawnZ = 0f;
    private int nextPlatformIndex = 0;

    private Queue<GameObject> activePlatforms = new Queue<GameObject>();
    private Queue<float> activePlatformLengths = new Queue<float>();

    void Start()
    {
        for (int i = 0; i < startingPlatforms; i++)
        {
            SpawnPlatform();
        }
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        if (nextSpawnZ < player.position.z + spawnAheadDistance)
        {
            SpawnPlatform();
        }

        RemoveOldPlatforms();
    }

    void SpawnPlatform()
    {
        int currentIndex = nextPlatformIndex;

        GameObject newPlatform = Instantiate
        (
            platformPrefabs[currentIndex],
            new Vector3(0f, 0f, nextSpawnZ),
            Quaternion.identity
        );

        activePlatforms.Enqueue(newPlatform);
        activePlatformLengths.Enqueue(platformLengths[currentIndex]);

        nextSpawnZ += platformLengths[currentIndex];

        nextPlatformIndex++;

        if (nextPlatformIndex >= platformPrefabs.Length)
        {
            nextPlatformIndex = 0;
        }
    }

    void RemoveOldPlatforms()
    {
        if (activePlatforms.Count == 0)
        {
            return;
        }

        GameObject oldestPlatform = activePlatforms.Peek();
        float oldestPlatformLength = activePlatformLengths.Peek();

        if (oldestPlatform.transform.position.z + oldestPlatformLength < player.position.z - destroyBehindDistance)
        {
            activePlatforms.Dequeue();
            activePlatformLengths.Dequeue();

            Destroy(oldestPlatform);
        }
    }
}