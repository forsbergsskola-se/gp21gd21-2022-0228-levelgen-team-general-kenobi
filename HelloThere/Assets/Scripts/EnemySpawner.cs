using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int maxEnemies;
    public List<GameObject> SpawnableTiles = new List<GameObject>();

    [SerializeField] private GameObject EnemyPrefab;
    public void Start()
    {

        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < maxEnemies; i++)
        {
            if (SpawnableTiles.Count == 0)
                throw new System.Exception($"No more available spawn tiles! {i} out of {maxEnemies} spawned successfully.");
            
            var randomIndex = Random.Range(0, SpawnableTiles.Count);
            var spawnableTile = SpawnableTiles[randomIndex];
            var spawnPosition = spawnableTile.GetComponent<Renderer>().bounds.center;

            spawnPosition.y =+ 1;
            var enemy = Instantiate(EnemyPrefab, spawnPosition, Quaternion.identity);
            enemy.GetComponent<NetworkObject>().Spawn();

            SpawnableTiles.Remove(spawnableTile);
        }
    }
}
