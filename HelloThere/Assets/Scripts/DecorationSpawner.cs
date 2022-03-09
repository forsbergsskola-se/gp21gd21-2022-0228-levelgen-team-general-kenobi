using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

public class DecorationSpawner : MonoBehaviour
{
    [SerializeField] private GameObject decorationPrefab;
    [SerializeField] private int maxDecorations;
    [HideInInspector] public List<GameObject> SpawnableTiles = new List<GameObject>();

    public void Start()
    {
        SpawnDecoration();
    }

    void SpawnDecoration()
    {
        for (int i = 0; i < maxDecorations; i++)
        {
            if (SpawnableTiles.Count == 0)
                throw new System.Exception($"No more available spawn tiles! {i} out of {maxDecorations} decorations spawned successfully.");

            var randomIndex = Random.Range(0, SpawnableTiles.Count);
            var spawnableTile = SpawnableTiles[randomIndex];
            var spawnPosition = spawnableTile.GetComponent<Renderer>().bounds.center;

            spawnPosition.y =+ 1;
            var enemy = Instantiate(decorationPrefab, spawnPosition, Quaternion.identity);
            enemy.GetComponent<NetworkObject>().Spawn();

            SpawnableTiles.Remove(spawnableTile);
        }
    }
}
