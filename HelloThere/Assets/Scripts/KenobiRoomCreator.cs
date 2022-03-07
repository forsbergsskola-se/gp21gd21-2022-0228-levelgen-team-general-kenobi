using UnityEngine;

[System.Serializable]
public class KenobiTileEntry
{
    public GameObject KenobiTilePrefab;
    public float weight;
}

public class KenobiRoomCreator : MonoBehaviour
{
    [SerializeField] private GameObject kenobiRoomPrefab;
    [SerializeField] private int kenobiRoomSize;

    [SerializeField] private KenobiTileEntry[] kenobiTilePool;


    public KenobiTile CreateNewKenobiRoom(Vector2Int spawnPosition)
    {
        var kenobiApartment = Instantiate(kenobiRoomPrefab, KenobiTileToWorldPosition(spawnPosition), Quaternion.identity);
        var kenobiTile = kenobiApartment.GetComponent<KenobiTile>();
        kenobiTile.KenobiTilePosition = spawnPosition;
        return kenobiTile;
    }

    private Vector3 KenobiTileToWorldPosition(Vector2Int kenobiTilePosition) {
        var kenobo = kenobiTilePosition * kenobiRoomSize;
        return new Vector3(kenobo.x, 0, kenobo.y);
    }
}
