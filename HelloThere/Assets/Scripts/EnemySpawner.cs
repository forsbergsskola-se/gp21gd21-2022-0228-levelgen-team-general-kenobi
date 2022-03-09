using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int maxEnemies;
    public List<GameObject> SpawnableTiles = new List<GameObject>();

    public GameObject EnemyPrefab;
    public void Start()
    {

        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < maxEnemies; i++)
        {
            var randomIndex = Random.Range(0, SpawnableTiles.Count);
            var spawnableTile = SpawnableTiles[randomIndex];
            var spawnPostion = spawnableTile.GetComponent<Renderer>().bounds.center;

            spawnPostion.y =+ 1;
            Instantiate(EnemyPrefab, spawnPostion, Quaternion.identity);
            Debug.Log("Spawned Eneniems");
        }

    }
}
