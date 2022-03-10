using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;


[System.Serializable]
public struct SpawnableEnemy
{
    public GameObject EnemyPrefab;
    public int SpawnWeight;
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private SpawnableEnemy[] spawnableEnemies;
    [SerializeField] private int maxEnemies;
    [HideInInspector] public List<GameObject> SpawnableTiles = new List<GameObject>();

    [SerializeField] private float progressionMultiplier;
    [SerializeField] private int randomizerBlockSize;

    private int lowRange;
    private int highRange;
    private GameObject[] enemyPrefabReferences;
    private WorldTileCreator worldTileCreator;

    public void Start()
    {
        enemyPrefabReferences = GetEnemyPrefabReferences();

        lowRange = -randomizerBlockSize;
        highRange = 0;

        worldTileCreator = FindObjectOfType<WorldTileCreator>();

        SpawnEnemies();
    }

    /// <summary>
    /// Returns an array of references to prefabs based on weight;
    /// </summary>
    /// <returns></returns>
    private GameObject[] GetEnemyPrefabReferences()
    {
        var totalWeight = 0;
        foreach (var spawnableEnemy in spawnableEnemies)
        {
            totalWeight += spawnableEnemy.SpawnWeight;
        }

        var result = new GameObject[totalWeight];

        var poolIndex = 0;
        var searchWeight = 0;
        for (var i = 0; i < totalWeight; i++)
        {
            var spawnableEnemy = spawnableEnemies[poolIndex];

            if (searchWeight >= spawnableEnemy.SpawnWeight)
            {
                poolIndex++;
                searchWeight = 0;
                spawnableEnemy = spawnableEnemies[poolIndex];
            }

            result[i] = spawnableEnemy.EnemyPrefab;
            searchWeight++;
        }

        return result;
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < maxEnemies; i++)
        {
            if (SpawnableTiles.Count == 0)
                throw new System.Exception($"No more available spawn tiles! {i} out of {maxEnemies} enemies spawned successfully.");

            var randomIndex = Random.Range(0, SpawnableTiles.Count);
            var spawnableTile = SpawnableTiles[randomIndex];
            var spawnPosition = spawnableTile.GetComponent<Renderer>().bounds.center;

            spawnPosition.y =+ 1;
            var randomEnemyIndex = Random.Range(lowRange, highRange);
            randomEnemyIndex = (int) Mathf.Clamp(randomEnemyIndex + worldTileCreator.spawnedRooms * progressionMultiplier, 0, enemyPrefabReferences.Length - 1);

            var enemy = Instantiate(enemyPrefabReferences[randomEnemyIndex], spawnPosition, Quaternion.Euler(0, Random.Range(0f, 360f), 0 ));
            enemy.GetComponent<NetworkObject>().Spawn();

            SpawnableTiles.Remove(spawnableTile);
        }
    }
}
