using UnityEngine;

public class KenobiRoomCreator : MonoBehaviour
{
    [SerializeField] private GameObject kenobiRoomPrefab;
    [SerializeField] private int kenobiRoomSize;



    public KenobiTile CreateNewKenobiRoom(Vector2Int spawnPosition)
    {
        var kenobiApartment = Instantiate(kenobiRoomPrefab, KenobiTileToWorldPosition(spawnPosition), Quaternion.identity);
        return kenobiApartment.GetComponent<KenobiTile>();
    }

    private Vector3 KenobiTileToWorldPosition(Vector2Int kenobiTilePosition) {
        var kenobo = kenobiTilePosition * kenobiRoomSize;
        return new Vector3(kenobo.x, -5, kenobo.y);
    }
}
