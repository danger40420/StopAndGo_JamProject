using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PointSpawner : MonoBehaviour
{
    public List<GameObject> prefabList;
    public Camera targetCamera;
    public Transform playerTransform;
    public float spawnRadius = 5f;
    
    public void SpawnRandomPrefabAroundPlayer()
    {
        if (prefabList == null || prefabList.Count == 0 || playerTransform == null)
            return;
        
        int randomIndex = Random.Range(0, prefabList.Count);
        GameObject selectedPrefab = prefabList[randomIndex];
        
        Vector2 randomPoint2D = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPosition = playerTransform.position + new Vector3(randomPoint2D.x, 0f, randomPoint2D.y);
        
        Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
    }
}

