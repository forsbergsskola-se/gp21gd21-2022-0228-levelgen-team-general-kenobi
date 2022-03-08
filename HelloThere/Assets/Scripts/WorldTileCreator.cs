using UnityEngine;
using Random = UnityEngine.Random;



/// <summary>
/// Holds one WorldTile prefab and it's weight.
/// </summary>
[System.Serializable]
public class WorldTileEntry
{
    public GameObject WorldTilePrefab;
    public int weight;
}



/// <summary>
/// Handles placement of world tiles.
/// </summary>
public class WorldTileCreator : MonoBehaviour
{
    [SerializeField] private int worldTileSizes;
    [SerializeField] private WorldTileEntry[] worldTilePool;
    
    private GameObject[] worldTileReferences;
    private Quaternion[] worldTileRotations;

    
    
    private void Start()
    {
        worldTileReferences = GetWorldTileReferences();
        worldTileRotations = GetWorldTileRotations();
    }



    /// <summary>
    /// Calculates and places all four 90 degree rotations into an array and returns it.
    /// </summary>
    /// <returns></returns>
    private Quaternion[] GetWorldTileRotations()
    {
        var result = new Quaternion[4];
        var rotation = Quaternion.identity;
        for (var i = 0; i < worldTileRotations.Length; i++)
        {
            result[i] = rotation;
            rotation *= Quaternion.Euler(0,90,0);
        }

        return result;
    }

    
    
    /// <summary>
    /// Creates an array of references to world tile prefabs with different amounts of references based on weight.
    /// </summary>
    /// <returns></returns>
    private GameObject[] GetWorldTileReferences()
    {
        
        var totalWeight = 0;
        foreach (var worldTileEntry in worldTilePool)
        {
            totalWeight += worldTileEntry.weight;
        }

        var result = new GameObject[totalWeight];

        var poolIndex = 0;
        var searchWeight = 0;
        for (var i = 0; i < totalWeight; i++)
        {
            var kenobiEntry = worldTilePool[poolIndex];
            if (searchWeight >= kenobiEntry.weight)
            {
                poolIndex++;
                searchWeight = 0;
                kenobiEntry = worldTilePool[poolIndex];
            }

            result[i] = kenobiEntry.WorldTilePrefab;
            searchWeight++;
        }

        return result;
    }



    /// <summary>
    /// Create a new random world tile at the provided world tile position.
    /// </summary>
    /// <param name="spawnPosition"></param>
    /// <returns></returns>
    public WorldTile CreateNewWorldTile(Vector2Int spawnPosition)
    {
        int randomInt = Random.Range(0, worldTileReferences.Length);
        var randomRotations = Random.Range(0, 0);
        var newWorldTile = Instantiate(worldTileReferences[randomInt], WorldTilePositionToGameWorldPosition(spawnPosition), worldTileRotations[randomRotations]);
        var newWorldTileClass = newWorldTile.GetComponent<WorldTile>();
        newWorldTileClass.WorldTilePosition = spawnPosition;

        var randomBool = Random.Range(0, 3);
        if (randomBool == 1)
        {
            var transformLocalScale = newWorldTile.transform.localScale;
            transformLocalScale.x *= -1;
            newWorldTile.transform.localScale = transformLocalScale;
        }
        if (randomBool == 2)
        {
            var transformLocalScale = newWorldTile.transform.localScale;
            transformLocalScale.z *= -1;
            newWorldTile.transform.localScale = transformLocalScale;
        }

        return newWorldTileClass;
    }



    /// <summary>
    /// Translates a world tile position to game world coordinates.
    /// </summary>
    /// <param name="worldTilePosition">position to translate</param>
    /// <returns>result of translation in vector3 format</returns>
    private Vector3 WorldTilePositionToGameWorldPosition(Vector2Int worldTilePosition)
    {
        var updatedVector2 = worldTilePosition * worldTileSizes;
        return new Vector3(updatedVector2.x, 0, updatedVector2.y);
    }
}
