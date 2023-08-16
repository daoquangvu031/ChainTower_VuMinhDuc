using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CoinSpawn : MonoBehaviour
{
    public GameObject coinPrefab;
    public float spawnDelay = 10.0f;
    public Collider forbiddenArea; // Trụ hoặc vùng cấm


    private bool hasSpawnedCoin = false;
    private NavMeshSurface navMeshSurface;

    private void Start()
    {
        navMeshSurface = FindObjectOfType<NavMeshSurface>();
        StartCoroutine(SpawnCoinAfterDelay());
    }

    private IEnumerator SpawnCoinAfterDelay()
    {
        yield return new WaitForSeconds(spawnDelay);

        if (!hasSpawnedCoin)
        {
            SpawnCoin();
            hasSpawnedCoin = true;
        }
    }

    private void SpawnCoin()
    {
        Vector3 randomPosition = GetRandomNavMeshPosition();

        if (forbiddenArea != null && forbiddenArea.bounds.Contains(randomPosition))
        {
            SpawnCoin();
        }
        else
        {
            Instantiate(coinPrefab, randomPosition, Quaternion.identity);
        }
    }

    private Vector3 GetRandomNavMeshPosition()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        int randomTriangleIndex = Random.Range(0, navMeshData.indices.Length - 3);
        Vector3 randomPosition = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[randomTriangleIndex]],
                                              navMeshData.vertices[navMeshData.indices[randomTriangleIndex + 1]],
                                              Random.value);
        randomPosition = Vector3.Lerp(randomPosition,
                                      navMeshData.vertices[navMeshData.indices[randomTriangleIndex + 2]],
                                      Random.value);

        return randomPosition;
    }
}
