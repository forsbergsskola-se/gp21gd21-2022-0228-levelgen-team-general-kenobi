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

            //Decoration goes under ground if not set
            spawnPosition.y = 0.0f;

            var vectorOffset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));

            var enemy = Instantiate(decorationPrefab, spawnPosition + vectorOffset, Quaternion.Euler(0, Random.Range(0f, 360f), 0 ));
            enemy.GetComponent<NetworkObject>().Spawn();

            SpawnableTiles.Remove(spawnableTile);
        }
    }
}
