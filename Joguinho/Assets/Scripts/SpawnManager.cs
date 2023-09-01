using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] private GameObject enemyPrefabs;

    void Start()
    {
        InvokeRepeating("SpawnEnemies", 2, 2);
    }

  
    void SpawnEnemies()
    {
        int index = Random.Range(0, spawnPoints.Length);
        Instantiate(enemyPrefabs, spawnPoints[index].position, Quaternion.identity);
    }
}
