using System;
using UnityEngine;
using Random = UnityEngine.Random;



[System.Serializable]
public class KenobiTileEntry
{
    public GameObject KenobiTilePrefab;
    public int weight;
}



public class KenobiRoomCreator : MonoBehaviour
{
    [SerializeField] private GameObject kenobiRoomPrefab;
    [SerializeField] private int kenobiRoomSize;
    [SerializeField] private KenobiTileEntry[] kenobiTilePool;



    private GameObject[] KenobiTileReferences;
    private Quaternion[] KenobiRoations = new Quaternion[4];

    private void Start()
    {
        var totalWeight = 0;
        foreach (var kenobiTileEntry in kenobiTilePool)
        {
            totalWeight += kenobiTileEntry.weight;
        }

        KenobiTileReferences = new GameObject[totalWeight];

        var poolIndex = 0;
        var searchWeight = 0;
        for (var i = 0; i < totalWeight; i++)
        {
            var kenobiEntry = kenobiTilePool[poolIndex];
            if (searchWeight >= kenobiEntry.weight)
            {
                poolIndex++;
                searchWeight = 0;
                kenobiEntry = kenobiTilePool[poolIndex];
            }

            KenobiTileReferences[i] = kenobiEntry.KenobiTilePrefab;
            searchWeight++;
        }

        var rotation = Quaternion.identity;
        for (int i = 0; i < KenobiRoations.Length; i++) {
            KenobiRoations[i] = rotation;
            rotation *= Quaternion.Euler(0,90,0);
        }
    }



    public KenobiTile CreateNewKenobiRoom(Vector2Int spawnPosition)
    {
        int randomInt = Random.Range(0, KenobiTileReferences.Length);
        var randomRotations = Random.Range(0, 4);
        var kenobiApartment = Instantiate(KenobiTileReferences[randomInt], KenobiTileToWorldPosition(spawnPosition), KenobiRoations[randomRotations]);
        var kenobiTile = kenobiApartment.GetComponent<KenobiTile>();
        kenobiTile.KenobiTilePosition = spawnPosition;
        return kenobiTile;
    }



    private Vector3 KenobiTileToWorldPosition(Vector2Int kenobiTilePosition)
    {
        var kenobo = kenobiTilePosition * kenobiRoomSize;
        return new Vector3(kenobo.x, 0, kenobo.y);
    }
}
